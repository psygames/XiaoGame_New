using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedStone.Data;
using Message;


namespace RedStone.SOS
{
    public class SOS_Logic
    {
        BattleServerProxy battleProxy { get { return ProxyManager.instance.GetProxy<BattleServerProxy>(); } }
        RoomProxy roomProxy { get { return ProxyManager.instance.GetProxy<RoomProxy>(); } }
        UserProxy userProxy { get { return ProxyManager.instance.GetProxy<UserProxy>(); } }
        private int m_roomID;
        RoomData room { get { return roomProxy.GetRoom(m_roomID); } }
        Random rand = new Random();

        //最长时间一小时，超时房间自动解散
        private const float ROOM_MAX_TIME = 60 * 60;

        private State m_state = State.WaitJoin;
        private List<Player> m_players = new List<Player>();

        public void Init(int roomID)
        {
            m_roomID = roomID;
            roomRemainTime = ROOM_MAX_TIME;

            InitPlayers();

            // Register Msg
            RegisterMsg<CBJoinBattleRequest>(OnJoinBattle);
            RegisterMsg<CBReady>(OnReady);
        }

        void InitPlayers()
        {
            m_players.Clear();
            int incrID = 1;
            foreach (var user in room.users)
            {
                var player = new Player();
                player.Init(user, incrID);
                m_players.Add(player);
                incrID++;
            }
        }

        public Player GetPlayer(int playerID)
        {
            return m_players.FirstOrDefault(a => a.id == playerID);
        }

        public Card GetCard(int cardID)
        {
            return m_cardMgr.allCards.FirstOrDefault(a => a.id == cardID);
        }


        public void Update()
        {
            CheckAllJoined();
            CheckAllReady();
            CheckEnd();
        }

        #region Check State

        private float roomRemainTime = 0;
        private void CheckEnd()
        {
            roomRemainTime -= Time.deltaTime;
            if (roomRemainTime <= 0)
                m_state = State.End;

            //TODO: End Conditions
        }

        public void CheckAllJoined()
        {
            if (m_state != State.WaitJoin || m_state == State.WaitReady)
                return;

            if (m_players.All(a => a.state == Player.State.Joined))
            {
                m_state = State.WaitReady;
                RoomSync();
            }
        }

        public void CheckAllReady()
        {
            if (m_state != State.WaitReady || m_state == State.Started)
                return;

            if (m_players.All(a => a.state == Player.State.Ready))
            {
                m_state = State.Started;
                RoomSync();
                GameBegin();
            }
        }
        #endregion

        void OnJoinBattle(Player player, CBJoinBattleRequest msg)
        {
            if (m_state != State.WaitJoin)
                return;

            player.user.SetState(UserState.Battle);
            player.SetState(Player.State.Joined);

            var rep = new CBJoinBattleReply();
            rep.Info = new BattleRoomInfo();
            rep.Info.Id = m_roomID;
            rep.Info.Name = room.name;

            foreach (var d in m_players)
            {
                BattlePlayerInfo info = new BattlePlayerInfo();
                info.Id = d.id;
                info.IsSelf = d == player;
                info.Level = d.user.level;
                info.Name = d.user.name;
                info.Gold = d.gold;
                info.Seat = d.seat;
                info.Joined = d.state == Player.State.Joined;
                rep.PlayerInfos.Add(info);
            }

            SendToAll(rep);

            Debug.Log(player.user.name + " joined game");
        }

        void OnReady(Player player, CBReady msg)
        {
            if (m_state != State.WaitReady)
                return;

            player.SetState(Player.State.Ready);

            CBReadySync sync = new CBReadySync();
            sync.FromID = player.id;
            SendToAll(sync);

            Debug.Log(player.user.name + " is ready");
        }

        void OnPlayCard(Player player, CBPlayCard msg)
        {
            if (m_whosTurn != player)
            {
                Debug.LogError("{0} 出牌，但是不是他的回个，已禁止！", player.name);
                return;
            }

            var card = GetCard(msg.CardID);
            player.RemoveCard(card);
            var target = GetPlayer(msg.TargetID);


            Debug.Log("{0} 出牌 {1} 指向 {2}", player.name, card.name, target.name);

            CBPlayCardSync sync = new CBPlayCardSync();
            sync.FromID = player.id;
            sync.TargetID = target.id;
            sync.CardID = card.id;
            SendToAll(sync);

            //TODO:中间技能过程

            TurnNextAndSendCard();
        }








        private Player m_whosTurn = null;
        public void TurnNext()
        {
            int nextSeat = m_whosTurn.seat % m_players.Count + 1;
            m_whosTurn = m_players.First(a => a.seat == nextSeat);
            RoomSync();
        }


        public void TurnNextAndSendCard()
        {
            TurnNext();
            SendCardToTurned();
        }

        public void RoomSync()
        {
            CBRoomSync sync = new CBRoomSync();
            sync.State = (int)m_state;
            if (m_whosTurn != null)
                sync.WhoseTurn = m_whosTurn.id;
            SendToAll(sync);
        }

        // SEND CARDS
        CardMgr m_cardMgr = new CardMgr();
        public void GameBegin()
        {
            m_whosTurn = m_players.First(a => a.seat == 1);
            m_cardMgr.Reset();
            GameBegin_SyncCards();
        }

        public void GameBegin_SyncCards()
        {
            CBCardInfoSync sync = new CBCardInfoSync();
            foreach (var card in m_cardMgr.allCards)
            {
                CardInfo info = new CardInfo();
                info.Id = card.id;
                info.TableID = card.tableID;
                sync.Cards.Add(info);
            }

            SendToAll(sync);
        }

        // 首次发牌
        public void GameBegin_SendCards()
        {
            int n = m_players.Count - 1;
            SendCardToTurned(); //给第一个玩家发牌
            while (n-- > 0) //给其他玩家发牌
            {
                TurnNextAndSendCard();
            }
            TurnNextAndSendCard();//给第一个玩家发第二张牌
        }

        public void SendCard(int targetID, int cardID)
        {
            CBSendCardSync msg = new CBSendCardSync();
            msg.CardID = cardID;
            msg.TargetID = targetID;
        }


        public void SendCardToTurned()
        {
            Card card = m_cardMgr.TakeCard();
            m_whosTurn.AddCard(card);
            SendCard(m_whosTurn.id, card.id);
        }




        public enum State
        {
            WaitJoin,
            WaitReady,
            Started,
            End,
        }

        public void SendToAll<T>(T msg)
        {
            foreach (var p in m_players)
            {
                battleProxy.SendMessage(p.user.sessionID, msg);
            }
        }

        public void SendTo<T>(int playerId, T msg)
        {
            var p = m_players.First(a => a.id == playerId);
            battleProxy.SendMessage(p.user.sessionID, msg);
        }


        private void RegisterMsg<T>(Action<Player, T> action)
        {
            foreach (var user in room.users)
            {
                battleProxy.RegisterUserMsg<T>(user.token, (msg) =>
                {
                    action.Invoke(m_players.First(a => a.user.token == user.token), msg);
                });
            }
        }
    }
}
