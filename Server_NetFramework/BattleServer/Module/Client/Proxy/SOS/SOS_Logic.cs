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


        private float m_whosTurnCountDown = 0;
        public State state { get; private set; }
        private List<Player> m_players = new List<Player>();
        private List<Player> alivePlayers { get { return m_players.Where(a => a.state != Player.State.Out).ToList(); } }

        public void Init(int roomID)
        {
            state = State.WaitJoin;
            m_roomID = roomID;
            roomRemainTime = ROOM_MAX_TIME;
            m_endKeepCD = END_KEEP_TIME;

            InitPlayers();

            // Register Msg
            RegisterMsg<CBJoinBattleRequest>(OnJoinBattle);
            RegisterMsg<CBReady>(OnReady);
            RegisterMsg<CBPlayCard>(OnPlayCard);
            RegisterMsg<CBSendMessage>(OnPlayerSendMessage);
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

        public List<Player> GetPlayers()
        {
            return m_players;
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

            UpdateEndKeepTime();
            UpdateTurnTime();
            UpdateAI();
        }

        #region Check State

        private float m_endKeepCD = 0;
        private void UpdateEndKeepTime()
        {
            if (state != State.End)
                return;

            m_endKeepCD -= Time.deltaTime;
        }

        private float roomRemainTime = 0;
        private void CheckEnd()
        {
            if (state != State.Started)
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
            if (state != State.WaitJoin)
                return;

            if (m_players.All(a => a.state == Player.State.Joined))
            {
                state = State.WaitReady;
                RoomSync();
            }
        }

        public void CheckAllReady()
        {
            if (state != State.WaitReady)
                return;

            if (m_players.All(a => a.state == Player.State.Ready))
            {
                state = State.Started;
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

            winner.AddScore(70);
            foreach (var p in m_players)
            {
                if (p.state != Player.State.Out && p.handCards.Count > 0)
                {
                    p.AddScore(30);
                }
            }


            SendResult(winner);
            state = State.End;
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
            m_whosTurnCountDown = Math.Max(m_whosTurnCountDown - Time.deltaTime, 0);
        }

        public CBReconnectReply GetReconnectData(long uid)
        {
            var player = m_players.FirstOrDefault(a => a.uid == uid);
            CBReconnectReply rep = new CBReconnectReply();
            foreach (var p in m_players)
            {
                BattlePlayerInfo info = new BattlePlayerInfo();
                info.Id = p.id;
                info.IsSelf = p == player;
                info.Level = p.user.level;
                info.Name = p.user.name;
                info.Gold = p.gold;
                info.Seat = p.seat;
                info.State = (int)p.state;
                foreach (var card in p.handCards)
                {
                    if (p == player)
                        info.HandCards.Add(card.id);
                    else
                        info.HandCards.Add(0); // 其他玩家不同步卡牌，防作弊
                }
                rep.PlayerInfos.Add(info);
            }

            foreach (var card in m_cardMgr.allCards)
            {
                CardInfo info = new CardInfo();
                info.Id = card.id;
                info.TableID = card.tableID;
                rep.Cards.Add(info);
            }

            rep.RoomInfo = new BattleRoomInfo();
            rep.RoomInfo.Id = room.id;
            rep.RoomInfo.Name = room.name;
            if (m_whosTurn != null)
                rep.WhoseTurn = m_whosTurn.id;
            rep.RoomState = (int)state;
            rep.LeftCardCount = m_cardMgr.leftCards.Count;
            rep.LeftTurnTime = m_whosTurnCountDown;
            rep.LastPlayedCardId = 0;

            return rep;
        }

        void OnJoinBattle(Player player, CBJoinBattleRequest msg)
        {
            if (state != State.WaitJoin)
                return;

            player.user.SetState(UserState.Battle);
            player.SetState(Player.State.Joined);

            var rep = new CBJoinBattleReply();
            rep.Info = new BattleRoomInfo();
            rep.Info.Id = m_roomID;
            rep.Info.Name = room.name;

            foreach (var p in m_players)
            {
                BattlePlayerInfo info = new BattlePlayerInfo();
                info.Id = p.id;
                info.IsSelf = p == player;
                info.Level = p.user.level;
                info.Name = p.user.name;
                info.Gold = p.gold;
                info.Seat = p.seat;
                info.State = (int)p.state;
                foreach (var card in p.handCards)
                {
                    info.HandCards.Add(card.id);
                }
                rep.PlayerInfos.Add(info);
            }

            SendTo(player.id, rep);

            Logger.Log("{0}\t加入房间", player.user.name);
        }

        void OnReady(Player player, CBReady msg)
        {
            if (state != State.WaitReady)
                return;

            player.SetState(Player.State.Ready);

            CBReadySync sync = new CBReadySync();
            sync.FromID = player.id;
            SendToAll(sync);

            Logger.Log("{0}\t已准备", player.user.name);
        }

        void OnPlayerSendMessage(Player player, CBSendMessage msg)
        {
            CBSendMessageSync sync = new CBSendMessageSync();
            sync.FromPlayerID = player.id;
            sync.Content = msg.Content;
            SendToAll(sync);
        }

        void OnPlayCard(Player player, CBPlayCard msg)
        {
            if (m_whosTurn != player)
            {
                Logger.LogError("{0}\t出牌，但不是他的回合，已禁止！", player.name);
                return;
            }

            if (m_isThisTrunPlayedCard)
            {
                return;
            }

            m_isThisTrunPlayedCard = true;

            var card = GetCard(msg.CardID);
            player.RemoveCard(card);
            var target = GetPlayer(msg.TargetID);


            if (msg.Extend > 0)
            {
                if (target != null)
                    Logger.Log("{0}\t出牌\t{1}\t指向\t{2}\tExt\t{3}", player.name, card.table.effect, target.name, msg.Extend);
                else
                    Logger.Log("{0}\t出牌\t{1}\tExt\t{2}", player.name, card.table.effect, msg.Extend);
            }
            else
            {
                if (target != null)
                    Logger.Log("{0}\t出牌\t{1}\t指向\t{2}", player.name, card.table.effect, target.name);
                else
                    Logger.Log("{0}\t出牌\t{1}", player.name, card.table.effect);
            }

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

            if (target != null && target.effect == Player.Effect.InvincibleOneRound)
            {
                Logger.LogError("玩家 {0} 无敌状态，技能 {1} 无效", target.name, card.table.effect);
                return 1;
            }

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
                cardIds = cardIds.OrderBy(a => Guid.NewGuid()).ToList();

                for (int i = 0; i < players.Count; i++)
                {
                    var p = players[i];
                    p.ChangeCard(GetCard(cardIds[i]));
                    msg.TargetID = p.id;
                    msg.TargetCardID = cardIds[i];
                    SendTo(p.id, msg);
                }
            }
            else if (cardTableID == 3) // 变革
            {
                m_cardMgr.PutCard(from.oneCard);    // 放回
                m_cardMgr.Shuffle();                // 洗牌
                var newCard = m_cardMgr.TakeCard(); // 抓牌
                from.ChangeCard(newCard);
                msg.TargetID = from.id;
                msg.TargetCardID = newCard.id;
                SendTo(from.id, msg);
            }
            else if (cardTableID == 4) // 壁垒
            {
                from.SetEffect(Player.Effect.InvincibleOneRound);
                foreach (var p in alivePlayers)
                {
                    msg.TargetID = p.id;
                    SendTo(p.id, msg);
                }
            }
            else if (cardTableID == 5) // 猜测
            {
                msg.TargetID = target.id;
                msg.TargetCardID = extend;
                if (extend == target.oneCard.tableID) // 猜卡牌TableID
                {
                    PlayerOut(target);
                    msg.Result = 1;
                }
                else
                {
                    msg.Result = 0;
                }
                SendToAll(msg);
                waitFor = 5;
            }
            else if (cardTableID == 6) // 决斗
            {
                msg.TargetID = target.id;
                if (from.oneCard.point > target.oneCard.point)
                {
                    from.AddScore(30);
                    PlayerOut(target);
                    msg.Result = 1;
                }
                else
                {
                    msg.Result = 0;
                }
                SendToAll(msg);
                waitFor = 5;
            }
            else if (cardTableID == 7) // 霸道 太阳
            {
                msg.TargetID = target.id;
                msg.TargetCardID = target.oneCard.id;

                bool isOut = false;
                if (target.oneCard.tableID == 10) // 弃置月亮航站，直接出局
                {
                    isOut = true;
                }
                DropCard(target);

                if (m_cardMgr.isEmpty || isOut)
                {
                    PlayerOut(target);
                    from.AddScore(30);
                }
                else
                {
                    SendCard(target);
                }
                SendToAll(msg);
            }
            else if (cardTableID == 8) // 交换
            {
                Card tmp = from.oneCard;
                from.ChangeCard(target.oneCard);
                target.ChangeCard(tmp);

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
            else if (cardTableID == 10) // 出局
            {
                PlayerOut(from);
            }

            LogStatus("出牌阶段");

            return waitFor;
        }

        public void PlayerOut(Player player)
        {
            player.SetState(Player.State.Out);
            CBPlayerOutSync msg = new CBPlayerOutSync();
            msg.PlayerID = player.id;
            if (player.oneCard != null)
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
            if (state == State.End && m_endKeepCD <= 0)
                return true;

            /* 玩家全部掉线情况（注释此代码，等待玩家重连！）
            if (state == State.Started)
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
            */
            return false;
        }





        private Player m_whosTurn = null;
        public void TurnNext()
        {
            m_whosTurnCountDown = WHOS_TURN_TIME;
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
                nextSeat = nextSeat % m_players.Count + 1;
            }

            if (!turnChanged)
            {
                Logger.LogError("Turn Not Changed --> {0}", m_whosTurn.name);
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
        }


        public void TurnNextAndSendCard()
        {
            if (m_cardMgr.leftCards.Count <= 0)
            {
                Logger.LogInfo("没有卡牌了，等待结算。");
                return;
            }
            if (alivePlayers.Count <= 1)
            {
                Logger.LogInfo("只剩下一个玩家，等待结算。");
                return;
            }
            TurnNext();
            SendCardToTurned();
            LogStatus("发牌阶段");
        }

        public void RoomSync()
        {
            CBRoomSync sync = new CBRoomSync();
            sync.State = (int)state;
            sync.LeftCardCount = m_cardMgr.leftCards.Count;
            if (m_whosTurn != null)
                sync.WhoseTurn = m_whosTurn.id;
            SendToAll(sync);
        }

        private void LogStatus(string title)
        {
            Logger.LogError("-------------------  " + title + "  --------------------");
            foreach (var p in m_players)
            {
                if (p.handCards.Count > 0)
                {
                    string cardStr = "";
                    foreach (var card in p.handCards)
                        cardStr += card.table.effect + "|";
                    cardStr = cardStr.TrimEnd('|');
                    Logger.LogError("{0}\t{1}\t{2}\n", p.name.PadRight(12), cardStr.PadRight(10), p.state);
                }
                else
                    Logger.LogError("{0}\t{1}\t{2}\n", p.name.PadRight(12), "空".PadRight(10), p.state);
            }
            Logger.LogError("---------------------------------------------------");
        }

        // SEND CARDS
        public void GameBegin()
        {
            Logger.LogInfo("Game Start!!!");
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

            Logger.Log("{1}\t获得\t{0}", card.table.effect, player.name);

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
            WaitJoin = 0,
            WaitReady = 1,
            Started = 2,
            End = 3,
            Dismiss = 4,
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
                battleProxy.RegisterUserTokenMsg<T>(user.token, (msg) =>
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
            if (state == State.WaitJoin)
            {
                foreach (var p in m_players)
                {
                    if (p.isAI && p.state == Player.State.None)
                        OnJoinBattle(p, null);
                }
            }
            else if (state == State.WaitReady)
            {
                foreach (var p in m_players)
                {
                    if (p.isAI && p.state == Player.State.Joined)
                        OnReady(p, null);
                }
            }
            else if (state == State.Started)
            {
                UpdateTurn();
                UpdateAIPlay();
            }
        }

        void UpdateTurn()
        {
            if (!m_whosTurn.isAI
                && m_whosTurnCountDown <= 0
                && !m_isThisTrunPlayedCard)
            {
                RandomPlayCard();
            }
        }

        private void RandomPlayCard()
        {
            CBPlayCard msg = new CBPlayCard();
            msg.CardID = RandomPlayerCardID(m_whosTurn);
            var card = GetCard(msg.CardID);
            if (card.type == CardType.ForOneTarget)
                msg.TargetID = RandomTargetID(m_whosTurn);
            if (card.tableID == 5) //猜测
            {
                List<Card> rnds = new List<Card>(m_cardMgr.leftCards);
                foreach (var p in alivePlayers)
                {
                    rnds.AddRange(p.handCards);
                }
                int rndIndex = rand.Next(0, rnds.Count);
                msg.Extend = rnds[rndIndex].tableID;
            }
            OnPlayCard(m_whosTurn, msg);
        }

        private int RandomTargetID(Player p)
        {
            int index = rand.Next(alivePlayers.Count - 1);
            return alivePlayers.Where(a => a != p).ToList()[index].id;
        }

        private int RandomPlayerCardID(Player p)
        {
            if (p.handCards.Any(a => a.tableID == 10)) // 不出月亮
                return p.handCards.First(a => a.tableID != 10).id;
            return p.handCards[rand.Next(p.handCards.Count)].id;
        }

        private bool m_isThisTrunPlayedCard = false;
        private float m_aiThinkTime = 10;

        void UpdateAIPlay()
        {
            if (m_whosTurn.isAI
                && !m_isThisTrunPlayedCard
                && m_whosTurnCountDown <= WHOS_TURN_TIME - m_aiThinkTime
                && m_whosTurn.handCards.Count > 0)
            {
                RandomPlayCard();
                m_aiThinkTime = rand.Next(7, 15);
            }
        }


    }
}
