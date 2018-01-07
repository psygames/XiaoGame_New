using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedStone.UI;
using RedStone.Data.SOS;

namespace RedStone
{
    public class SosRoom : MonoBehaviour
    {
        public Text whosTurnText;
        public Text stateText;
        public Text leftCountText;

        public GameObject readyBtn;
        public GameObject playBtn;
        public GameObject backBtn;

        public SosMainPlayer mainPlayer;
        public SosPlayer leftPlayer;
        public SosPlayer rightPlayer;
        public SosPlayer oppositePlayer;
        public SosTurnClock clock;
        public SosAimTargetLine aimTargetLine;
        public BattleHint hint;
        public SosGuessPanel guessPanel;
        public SosResultPanel resultPanel;

        private List<SosPlayer> m_players = new List<SosPlayer>();
        private RoomData data { get { return GF.GetProxy<SosProxy>().room; } }
        private CardData selectedCard { get { return mainPlayer.selectedCard; } }
        private SosPlayer whosTurn { get { return m_players.First(a => a.data.isTurned); } }
        public int curGuessCardID { get; private set; }

        public void Init()
        {
            Reset();
        }

        public void Reset()
        {
            m_players.Clear();
            clock.gameObject.SetActive(false);
            backBtn.SetActive(false);
            readyBtn.SetActive(false);
            playBtn.SetActive(false);
            resultPanel.gameObject.SetActive(false);
            guessPanel.gameObject.SetActive(false);
        }

        public void InitPlayers()
        {
            m_players.Clear();

            mainPlayer.gameObject.SetActive(true);
            mainPlayer.onCardSelectedCallback = OnCardSelected;
            m_players.Add(mainPlayer);

            if (data.players.Count == 2)
            {
                m_players.Add(oppositePlayer);
            }
            else if (data.players.Count == 3)
            {
                m_players.Add(rightPlayer);
                m_players.Add(leftPlayer);
            }
            else if (data.players.Count == 4)
            {
                m_players.Add(rightPlayer);
                m_players.Add(oppositePlayer);
                m_players.Add(leftPlayer);
            }

            leftPlayer.gameObject.SetActive(m_players.Contains(leftPlayer));
            rightPlayer.gameObject.SetActive(m_players.Contains(rightPlayer));
            oppositePlayer.gameObject.SetActive(m_players.Contains(oppositePlayer));
        }

        // On Event
        public void OnJoined()
        {
            InitPlayers();

            // seats
            for (int i = 0; i < m_players.Count; i++)
            {
                int seat = i + data.mainPlayer.seat;
                seat = (seat - 1) % m_players.Count + 1;
                m_players[i].Init(data.players.First(a => a.seat == seat));
            }

            foreach (var p in m_players)
            {
                p.onClickCallback = OnPlayerClick;
            }
        }

        private PlayerData m_selectedPlayer = null;
        void OnPlayerClick(PlayerData playerData)
        {
            if (data.whosTurn != mainPlayer.data
                || selectedCard.type != CardType.ForOneTarget
                || playerData.state == PlayerData.State.Out)
                return;

            if (selectedCard.tableID == 5) // 猜测
            {
                curGuessCardID = -1;
                guessPanel.Show();
                guessPanel.onSelectedCallback = (a) =>
                {
                    curGuessCardID = a;
                    m_selectedPlayer = playerData;
                    RefreshPlayers();
                };
            }
            else
            {
                m_selectedPlayer = playerData;
                RefreshPlayers();
            }
        }

        void OnCardSelected(CardData card)
        {
            m_selectedPlayer = null;
            RefreshUI();
        }

        public void OnReady(int id)
        {
            GetPlayer(id).SetReady(true);
        }

        public void OnRoomSync()
        {
            if (data.whosTurn != null
                && !data.whosTurn.isMain)
            {
                m_selectedPlayer = null;
            }

            RefreshUI();
            RefreshClock();
        }

        private PlayerData m_lastTurn = null;
        void RefreshClock()
        {
            if (data.state != RoomData.State.Started)
            {
                clock.gameObject.SetActive(false);
                return;
            }

            clock.gameObject.SetActive(true);
            if (data.whosTurn != null && data.whosTurn != m_lastTurn)
            {
                Vector2 pos = UIHelper.GetUIPosition(whosTurn.playedCardRoot) - UIConfig.virtualScreenCenter;
                clock.transform.localPosition = pos;
                clock.Reset();
                m_lastTurn = data.whosTurn;
            }
        }

        public void OnSendCard(Message.CBSendCardSync msg)
        {
            var card = data.GetCard(msg.CardID);
            GetPlayer(msg.TargetID).TakeCard(card);
        }

        public void OnPlayCard(Message.CBPlayCardSync msg)
        {
            var from = GetPlayer(msg.FromID);
            from.PlayCard(data.GetCard(msg.CardID));
            clock.gameObject.SetActive(false);

            var target = GetPlayer(msg.TargetID);
            if (target != null)
                ShowAimTargetLine(from.playedCardRoot, target.headIcon.transform);
        }

        public void OnBattleResult(Message.CBBattleResultSync msg)
        {
            resultPanel.Show(msg);
        }

        public void OnCardEffect(Message.CBCardEffectSync msg)
        {
            var fromPlayer = data.GetPlayer(msg.FromPlayerID);
            var fromCard = data.GetCard(msg.FromCardID);
            int cardTableID = fromCard.tableID;
            int result = msg.Result;
            var target = data.GetPlayer(msg.TargetID);

            if (cardTableID == 1) // 侦察
            {
                var targetCard = data.GetCard(msg.TargetCardID);
                hint.Show("【{0}】的手牌是【{1}】".FormatStr(target.name, targetCard.name));
            }
            else if (cardTableID == 2) //混乱
            {
                var targetCard = data.GetCard(msg.TargetCardID);
                hint.Show("【{0}】技能后，你获得【{1}】".FormatStr(fromCard.name, targetCard.name));
                GetPlayer(msg.TargetID).ChangeCard(targetCard);
            }
            else if (cardTableID == 3) // 变革
            {
                var targetCard = data.GetCard(msg.TargetCardID);
                hint.Show("【{0}】技能后，你获得【{1}】".FormatStr(fromCard.name, targetCard.name));
                GetPlayer(msg.TargetID).ChangeCard(targetCard);
            }
            else if (cardTableID == 4) // 壁垒
            {
                hint.Show("【{0}】获得壁垒保护效果，一回合内无敌".FormatStr(fromPlayer.name));
                GetPlayer(msg.TargetID).InvincibleOneRound();
            }
            else if (cardTableID == 5) // 猜卡牌TableID
            {
                var targetCardTable = TableManager.instance.GetData<TableSosCard>(msg.TargetCardID);
                if (result == 0)
                {
                    hint.Show("【{0}】猜测【{1}】错误！".FormatStr(fromPlayer.name, targetCardTable.name));
                }
                else
                {
                    hint.Show("【{0}】猜测【{1}】正确，玩家【{2}】出局！"
                        .FormatStr(fromPlayer.name, targetCardTable.name, target.name));
                }
            }
            else if (cardTableID == 6) // 决斗
            {
                if (result == 1)
                {
                    hint.Show("【{0}】决斗胜利，玩家【{1}】出局！"
                        .FormatStr(fromPlayer.name, target.name));
                }
                else
                {
                    hint.Show("【{0}】决斗【{1}】失败。"
                        .FormatStr(fromPlayer.name, target.name));
                }
            }
            else if (cardTableID == 7) // 霸道 太阳
            {
                hint.Show("【{0}】使用【{1}】技能，强制【{2}】弃置手牌！"
                        .FormatStr(fromPlayer.name, fromCard.table.effect, target.name));
            }
            else if (cardTableID == 8) // 交换
            {
                var targetCard = data.GetCard(msg.TargetCardID);
                hint.Show("【{0}】技能后，你获得【{1}】".FormatStr(fromCard.name, targetCard.name));
                GetPlayer(msg.TargetID).ChangeCard(targetCard);
            }
            else if (cardTableID == 9) // 开溜（只限制出牌阶段，出牌类型，出牌后无效果）
            {

            }
            else if (cardTableID == 10)
            {
                hint.Show("【{0}】主动使用【{1}】，出局！"
                        .FormatStr(fromPlayer.name, fromCard.name));
            }
        }

        public void OnDropCard(Message.CBPlayerDropCardSync msg)
        {
            var p = GetPlayer(msg.PlayerID);
            p.DropCard(data.GetCard(msg.CardID));
        }

        public void OnPlayerOut(Message.CBPlayerOutSync msg)
        {
            var p = GetPlayer(msg.PlayerID);
            p.Out(data.GetCard(msg.HandCardID));
        }

        void ShowAimTargetLine(Transform from, Transform to)
        {
            aimTargetLine.Show(UIHelper.GetUIPosition(from), UIHelper.GetUIPosition(to));
        }

        // Refresh
        void RefreshUI()
        {
            if (data.whosTurn != null)
                this.whosTurnText.text = data.whosTurn.name;
            else
                this.whosTurnText.text = "空";

            leftCountText.text = data.leftCardCount.ToString();
            stateText.text = data.state.ToString();
            RefreshPlayers();
        }

        private void Update()
        {
            if (data.state == RoomData.State.Started
                && data.whosTurn == mainPlayer.data
                && selectedCard != null)
            {
                playBtn.SetActive(true);
            }
            else
            {
                playBtn.SetActive(false);
            }


            if (data.state == RoomData.State.WaitReady)
            {
                readyBtn.SetActive(true);
            }
            else
            {
                readyBtn.SetActive(false);
            }


            if (data.state == RoomData.State.End)
            {
                backBtn.SetActive(true);
            }
            else
            {
                backBtn.SetActive(false);
            }
        }


        void RefreshPlayers()
        {
            foreach (var p in m_players)
            {
                if (m_selectedPlayer != null && m_selectedPlayer == p.data)
                {
                    p.BeSelected(true);
                }
                else
                {
                    p.BeSelected(false);
                }
            }
        }

        // Interface
        public SosPlayer GetPlayer(int playerID)
        {
            return m_players.First(a => a.data.id == playerID);
        }

        public void OnClickPlayCard()
        {
            if (selectedCard == null)
            {
                Toast.instance.Show("必须选择一张卡牌!");
                return;
            }

            if (selectedCard.type == CardType.ForOneTarget
                && m_selectedPlayer == null)
            {
                Toast.instance.Show("请选择一个玩家作为释放对象！");
                return;
            }

            //当你手上有太阳航站或火星航站时，必须弃置水星航站。
            if (data.mainPlayer.handCards.Any(a => a.tableID == 9)
                && data.mainPlayer.handCards.Any(a => a.tableID == 6 || a.tableID == 7)
                && selectedCard.tableID != 9)
            {
                Toast.instance.Show("当你手上有太阳航站或火星航站时，必须弃置水星航站。");
                return;
            }

            if (selectedCard.tableID == 5 && curGuessCardID <= 0)
            {
                Toast.instance.Show("请选择一张猜测的卡牌！");
                guessPanel.Show();
                return;
            }


            GF.GetProxy<SosProxy>().PlayCard(selectedCard.id
                , m_selectedPlayer == null ? 0 : m_selectedPlayer.id
                , curGuessCardID <= 0 ? 0 : curGuessCardID);
        }
    }
}