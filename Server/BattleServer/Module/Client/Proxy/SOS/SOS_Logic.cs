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
        const float WHOS_TURN_TIME = 35f;
        const float END_KEEP_TIME = 20f;


        private float m_whosTurnCounter = 0;
        private State m_state = State.WaitJoin;
        private List<Player> m_players = new List<Player>();

        public void Init(int roomID)
        {
            m_roomID = roomID;
            roomRemainTime = ROOM_MAX_TIME;
            m_endKeepCD = END_KEEP_TIME;

            InitPlayers();

            // Register Msg
            RegisterMsg<CBJoinBattleRequest>(OnJoinBattle);
            RegisterMsg<CBReady>(OnReady);
            RegisterMsg<CBPlayCard>(OnPlayCard);
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
            HandlePoolMsg();

            CheckAllJoined();
            CheckAllReady();
            CheckEnd();

            EndKeepTime();
            UpdateTurnTime();
            UpdateAI();
        }

        #region Check State

        private float m_endKeepCD = 0;
        private void EndKeepTime()
        {
            if (m_state != State.End)
                return;

            m_endKeepCD -= Time.deltaTime;
        }

        private float roomRemainTime = 0;
        private void CheckEnd()
        {
            if (m_state != State.Started)
                return;

            roomRemainTime -= Time.deltaTime;

            if (roomRemainTime <= 0)
            {
                CalculateResult();
            }
            else if (m_cardMgr.leftCards.Count <= 0
                 && m_players.All(a => a.state == Player.State.Out || a.handCards.Count <= 1))
            {
                CalculateResult();
            }
            else if (m_players.Count(a => a.state != Player.State.Out) <= 1)
            {
                CalculateResult();
            }
        }

        public void CheckAllJoined()
        {
            if (m_state != State.WaitJoin)
                return;

            if (m_players.All(a => a.state == Player.State.Joined))
            {
                m_state = State.WaitReady;
                RoomSync();
            }
        }

        public void CheckAllReady()
        {
            if (m_state != State.WaitReady)
                return;

            if (m_players.All(a => a.state == Player.State.Ready))
            {
                m_state = State.Started;
                RoomSync();
                GameBegin();
            }
        }

        public void CalculateResult()
        {
            Player winner = null;
            foreach (var p in m_players)
            {
                if (p.state != Player.State.Out && p.handCards.Count > 0
                    && (winner == null || p.handCards[0].point > winner.handCards[0].point))
                {
                    winner = p;
                }
            }
            SendResult(winner);
            m_state = State.End;
            RoomSync();
        }

        void SendResult(Player winner)
        {
            CBBattleResultSync msg = new CBBattleResultSync();
            msg.WinnerID.Add(winner.id);
            SendToAll(msg);
        }

        #endregion

        void UpdateTurnTime()
        {
            m_whosTurnCounter = Math.Max(m_whosTurnCounter - Time.deltaTime, 0);
        }

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

            SendTo(player.id, rep);

            Debug.Log("【{0}】 加入房间", player.user.name);
        }

        void OnReady(Player player, CBReady msg)
        {
            if (m_state != State.WaitReady)
                return;

            player.SetState(Player.State.Ready);

            CBReadySync sync = new CBReadySync();
            sync.FromID = player.id;
            SendToAll(sync);

            Debug.Log("【{0}】 已准备", player.user.name);
        }

        void OnPlayCard(Player player, CBPlayCard msg)
        {
            if (m_whosTurn != player)
            {
                Debug.LogError("【{0}】 出牌，但是不是他的回个，已禁止！", player.name);
                return;
            }

            m_isThisTrunPlayedCard = true;

            var card = GetCard(msg.CardID);
            player.RemoveCard(card);
            var target = GetPlayer(msg.TargetID);

            if (target != null)
                Debug.Log("【{0}】 出牌 【{1}】 指向 【{2}】", player.name, card.name, target.name);
            else
                Debug.Log("【{0}】 出牌 【{1}】", player.name, card.name);

            CBPlayCardSync sync = new CBPlayCardSync();
            sync.FromID = player.id;
            sync.TargetID = msg.TargetID;
            sync.CardID = card.id;
            SendToAll(sync);

            //TODO:中间技能过程

            TurnNextAndSendCard();
        }


        public bool CheckCanDismiss()
        {
            if (m_state == State.End && m_endKeepCD <= 0)
                return true;

            if (m_state == State.Started)
            {
                bool noPlayer = true;
                foreach (var p in m_players)
                {
                    if (!p.isAI && p.user.state == UserState.Battle)
                    {
                        noPlayer = false;
                        break;
                    }
                }
                return noPlayer;
            }
            return false;
        }





        private Player m_whosTurn = null;
        public void TurnNext()
        {
            m_whosTurnCounter = WHOS_TURN_TIME;
            m_isThisTrunPlayedCard = false;

            bool turnChanged = false;
            int nextSeat = m_whosTurn.seat % m_players.Count + 1;
            for (int i = 0; i < m_players.Count; i++)
            {
                var turn = m_players.First(a => a.seat == nextSeat);
                if (turn.state != Player.State.Out)
                {
                    m_whosTurn = turn;
                    turnChanged = true;
                    break;
                }
                nextSeat = (nextSeat + 1) % m_players.Count + 1;
            }

            if (!turnChanged)
            {
                Debug.LogError("Turn Not Changed --> {0}", m_whosTurn.name);
                return;
            }

            RoomSync();
            Debug.Log("轮到下一位 --> 【{0}】", m_whosTurn.name);
        }


        public void TurnNextAndSendCard()
        {
            if (m_cardMgr.leftCards.Count <= 0)
            {
                Debug.LogInfo("没有卡牌了，等待结算。");
                return;
            }
            if (m_players.Count(a => a.state != Player.State.Out) <= 1)
            {
                Debug.LogInfo("只剩下一个玩家，等待结算。");
                return;
            }
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
            Debug.Log("Game Start!!!");
            m_whosTurn = m_players.First(a => a.seat == 1);
            m_cardMgr.Reset();
            GameBegin_SyncCards();
            GameBegin_SendCards();
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
            SendToAll(msg);
        }


        public void SendCardToTurned()
        {
            Card card = m_cardMgr.TakeCard();
            m_whosTurn.AddCard(card);
            Debug.Log("发牌 【{0}】 给 【{1}】", card.name, m_whosTurn.name);
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
            // Debug.Log("Send {0} to all ", msg.GetType().Name);
            foreach (var p in m_players)
            {
                if (p.isAI || string.IsNullOrEmpty(p.user.sessionID))
                    continue;
                battleProxy.SendMessage(p.user.sessionID, msg);
            }
        }

        public void SendTo<T>(int playerId, T msg)
        {
            // Debug.Log("Send {0} to {1} ", msg.GetType().Name, playerId);
            var p = m_players.First(a => a.id == playerId);
            if (p.isAI)
                return;
            battleProxy.SendMessage(p.user.sessionID, msg);
        }


        private void RegisterMsg<T>(Action<Player, T> action)
        {
            foreach (var user in room.users)
            {
                battleProxy.RegisterUserMsg<T>(user.token, (msg) =>
                {
                    Action queAct = () =>
                    {
                        action.Invoke(m_players.First(a => a.user.token == user.token), msg);
                    };

                    lock (m_msgQueue)
                    {
                        m_msgQueue.Enqueue(queAct);
                    }
                });
            }
        }


        private Queue<Action> m_msgQueue = new Queue<Action>();

        private void HandlePoolMsg()
        {
            while (m_msgQueue.Count > 0)
            {
                var act = m_msgQueue.Dequeue();
                act.Invoke();
            }
        }


        // AI
        public void UpdateAI()
        {
            if (m_state == State.WaitJoin)
            {
                foreach (var p in m_players)
                {
                    if (p.isAI && p.state == Player.State.None)
                        OnJoinBattle(p, null);
                }
            }
            else if (m_state == State.WaitReady)
            {
                foreach (var p in m_players)
                {
                    if (p.isAI && p.state == Player.State.Joined)
                        OnReady(p, null);
                }
            }
            else if (m_state == State.Started)
            {
                UpdateAIPlay();
            }
        }

        private bool m_isThisTrunPlayedCard = false;
        void UpdateAIPlay()
        {
            if (m_whosTurn.isAI
                && !m_isThisTrunPlayedCard
                && m_whosTurnCounter <= 34
                && m_whosTurn.handCards.Count > 0)
            {
                CBPlayCard msg = new CBPlayCard();
                msg.CardID = RandPlayerCardID(m_whosTurn);
                if (GetCard(msg.CardID).type == CardType.ForOneTarget)
                    msg.TargetID = RandTargetID(m_whosTurn);
                OnPlayCard(m_whosTurn, msg);
            }
        }


        private int RandTargetID(Player p)
        {
            int index = rand.Next(m_players.Count - 1);
            return m_players.Where(a => a != p).ToList()[index].id;
        }

        private int RandPlayerCardID(Player p)
        {
            return p.handCards[rand.Next(p.handCards.Count)].id;
        }
    }
}
