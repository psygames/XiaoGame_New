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
        CardMgr m_cardMgr = new CardMgr();

        //最长时间一小时，超时房间自动解散
        private const float ROOM_MAX_TIME = 60 * 60;
        const float WHOS_TURN_TIME = 35f;
        const float END_KEEP_TIME = 20f;


        private float m_whosTurnCounter = 0;
        private State m_state = State.WaitJoin;
        private List<Player> m_players = new List<Player>();
        private List<Player> alivePlayers { get { return m_players.Where(a => a.state != Player.State.Out).ToList(); } }

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
                    && (winner == null || p.oneCard.point > winner.oneCard.point))
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
            foreach (var p in m_players)
            {
                BattleResultPlayerInfo info = new BattleResultPlayerInfo();
                info.IsWin = winner == p;
                info.PlayrID = p.id;
                info.RewardAmount = info.IsWin ? 1 : 0;
                info.State = (int)p.state;
                foreach (var card in p.handCards)
                {
                    info.Cards.Add(card.id);
                }
                msg.ResultInfos.Add(info);
            }
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
            sync.Extend = msg.Extend;
            SendToAll(sync);

            float waitFor = CardEffectLogic(player, target, card, msg.Extend);

            Task.WaitFor(waitFor, () =>
            {
                TurnNextAndSendCard();
            });
        }

        public float CardEffectLogic(Player from, Player target, Card card, int extend)
        {
            float waitFor = 3;
            CBCardEffectSync msg = new CBCardEffectSync();
            msg.FromPlayerID = from.id;
            msg.FromCardID = card.id;
            int cardTableID = card.tableID;

            if (cardTableID == 1) // 侦察
            {
                msg.TargetID = target.id;
                msg.TargetCardID = target.oneCard.id;
                SendTo(from.id, msg);
            }
            else if (cardTableID == 2) //混乱
            {
                List<int> cardIds = new List<int>();
                var players = alivePlayers;
                foreach (var p in players)
                {
                    cardIds.Add(p.oneCard.id);
                }
                cardIds.OrderBy(a => Guid.NewGuid());

                for (int i = 0; i < players.Count; i++)
                {
                    var p = players[i];
                    msg.TargetID = p.id;
                    msg.TargetCardID = cardIds[i];
                    SendTo(p.id, msg);
                }
            }
            else if (cardTableID == 3) // 变革
            {
                m_cardMgr.PutCard(from.oneCard);
                m_cardMgr.Shuffle();
                var newCard = m_cardMgr.TakeCard();
                msg.TargetID = from.id;
                msg.TargetCardID = newCard.id;
                SendTo(from.id, msg);
            }
            else if (cardTableID == 4) // 壁垒
            {
                from.SetEffect(Player.Effect.InvincibleOneRound);
            }
            else if (cardTableID == 5)
            {
                if (extend == target.oneCard.tableID) // 猜卡牌TableID
                {
                    PlayerOut(target);
                }
                waitFor = 5;
            }
            else if (cardTableID == 6) // 猜拳，我日
            {

            }
            else if (cardTableID == 7) // 霸道 太阳
            {
                DropCard(target);
                if (m_cardMgr.isEmpty)
                {
                    PlayerOut(target);
                }
                else
                {
                    SendCard(target);
                }
            }
            else if (cardTableID == 8) // 交换
            {
                Card tmp = from.oneCard;
                from.RemoveCard(from.oneCard);
                from.AddCard(target.oneCard);
                target.RemoveCard(target.oneCard);
                target.AddCard(tmp);

                msg.TargetID = from.id;
                msg.TargetCardID = from.oneCard.id;
                SendTo(from.id, msg);

                msg.TargetID = target.id;
                msg.TargetCardID = target.oneCard.id;
                SendTo(target.id, msg);
            }
            else if (cardTableID == 9) // 开溜（只限制出牌阶段，出牌类型，出牌后无效果）
            {

            }
            else if (cardTableID == 10)
            {
                PlayerOut(from);
            }

            return waitFor;
        }

        public void PlayerOut(Player player)
        {
            player.SetState(Player.State.Out);
            CBPlayerOutSync msg = new CBPlayerOutSync();
            msg.PlayerID = player.id;
            msg.HandCardID = player.oneCard.id;
            SendToAll(msg);
        }

        public void DropCard(Player player)
        {
            CBPlayerDropCardSync msg = new CBPlayerDropCardSync();
            Card card = player.oneCard;
            msg.PlayerID = player.id;
            msg.CardID = card.id;
            player.RemoveCard(card);
            SendToAll(msg);
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

            // 重置State
            foreach (var p in alivePlayers)
            {
                if (p == m_whosTurn)
                    p.SetState(Player.State.Turn);
                else
                    p.SetState(Player.State.NotTurn);
            }

            // 重置玩家Effect
            if (m_whosTurn.effect == Player.Effect.InvincibleOneRound)
                m_whosTurn.SetEffect(Player.Effect.None);

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
            if (alivePlayers.Count <= 1)
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
            sync.LeftCardCount = m_cardMgr.leftCards.Count;
            if (m_whosTurn != null)
                sync.WhoseTurn = m_whosTurn.id;
            SendToAll(sync);
        }

        // SEND CARDS
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

        public void SendCard(Player player)
        {
            Card card = m_cardMgr.TakeCard();

            Debug.Log("发牌 【{0}】 给 【{1}】", card.name, player.name);

            player.AddCard(card);

            CBSendCardSync msg = new CBSendCardSync();
            msg.TargetID = player.id;

            //除主角外，不发送卡牌ID（防作弊）
            SendToAll(msg, new int[] { player.id });

            // 发送给主角
            msg.CardID = card.id;
            SendTo(player.id, msg);
        }


        public void SendCardToTurned()
        {
            SendCard(m_whosTurn);
        }




        public enum State
        {
            WaitJoin,
            WaitReady,
            Started,
            End,
        }

        public void SendToAll<T>(T msg, int[] exceptIds = null)
        {
            // Debug.Log("Send {0} to all ", msg.GetType().Name);
            foreach (var p in m_players)
            {
                if (exceptIds != null && exceptIds.Contains(p.id))
                    continue;
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
            int index = rand.Next(alivePlayers.Count - 1);
            return alivePlayers.Where(a => a != p).ToList()[index].id;
        }

        private int RandPlayerCardID(Player p)
        {
            return p.handCards[rand.Next(p.handCards.Count)].id;
        }
    }
}
