using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedStone.UI;
using RedStone.Data.SOS;

namespace RedStone
{
    public class SosRoom : MonoBehaviour
    {
        public Text roomName;
        public Button exitBtn;
        public Text recordText;
        public Text lastPlayedCardText;
        public Text playedCardCount;
        public InputField chatInput;
        public Transform netPlayerRoot;
        public SosNetPlayer netPlayerTemplate;
        public SosResultPanel resultPanel;
        public SosMainPanel mainPanel;
        public SosTurnClock clock;
        public ListLayoutGroup chatMsgGroup;

        private List<SosPlayer> m_players = new List<SosPlayer>();
        private List<SosNetPlayer> m_netPlayers = new List<SosNetPlayer>();
        private RoomData data { get { return GF.GetProxy<SosProxy>().room; } }
        private SosPlayer whosTurn { get { return m_players.First(a => a.data.isTurned); } }
        private CardData m_lastPlayedCard = null;
        private PlayerData m_lastTurn;

        public void Init()
        {
            Reset();
        }

        public void Reset()
        {
            netPlayerTemplate.gameObject.SetActive(false);
            resultPanel.gameObject.SetActive(false);
            m_chatMsgs.Clear();
            Record("");
        }

        private void InitPlayers()
        {
            m_players.Clear();
            m_netPlayers.Clear();

            mainPanel.mainPlayer.Init(data.mainPlayer);
            m_players.Add(mainPanel.mainPlayer);

            var netPlayers = data.players.ToListFromPool(a => !a.isMain);
            netPlayers.Sort((a, b) => a.seat.CompareTo(b.seat));
            GameObjectHelper.SetListContent(netPlayerTemplate, netPlayerRoot, m_netPlayers, netPlayers
            , (index, item, data) =>
            {
                item.Init(data);
                item.onClickCallback = mainPanel.OnClickPlayer;
                m_players.Add(item);
            });
        }

        public void OnReady(int id)
        {
            GetPlayer(id).SetReady(true);
            RefreshUI();
        }

        public void OnJoined()
        {
            InitPlayers();
            RefreshUI();
        }

        public void OnRoomSync()
        {
            if (m_lastTurn != data.whosTurn)
            {
                NewTurn();
            }
            m_lastTurn = data.whosTurn;

            RefreshUI();
        }

        public void NewTurn()
        {
            clock.Reset();
            mainPanel.NewTurn();
        }

        public void OnSendCard(Message.CBSendCardSync msg)
        {
            var card = data.GetCard(msg.CardID);
            GetPlayer(msg.TargetID).TakeCard(card);

            RefreshUI();
        }

        public void OnPlayCard(Message.CBPlayCardSync msg)
        {
            var from = GetPlayer(msg.FromID);
            var card = data.GetCard(msg.CardID);
            from.PlayCard(card);

            var target = GetPlayer(msg.TargetID);

            var record = "";
            record += "{0} 发动 {1} 效果 \"{2}\"".FormatStr(from.data.numTag, card.nameRT, card.effectRT);
            if (target != null)
                record += " 指定 {0}。".FormatStr(target.data.numTag);
            Record(record);
            m_lastPlayedCard = card;
            RefreshUI();
        }

        public void OnBattleResult(Message.CBBattleResultSync msg)
        {
            resultPanel.Show(msg);
            RefreshUI();
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
                RecordAppend("{0}的手牌是{1}。".FormatStr(target.numTag, targetCard.nameRT));
            }
            else if (cardTableID == 2) //混乱
            {
                var targetCard = data.GetCard(msg.TargetCardID);
                RecordAppend("{0}效果后，你获得{1}。".FormatStr(fromCard.effectRT, targetCard.nameRT));
                GetPlayer(msg.TargetID).ChangeCard(targetCard);
            }
            else if (cardTableID == 3) // 变革
            {
                var targetCard = data.GetCard(msg.TargetCardID);
                RecordAppend("{0}效果后，你获得{1}。".FormatStr(fromCard.effectRT, targetCard.nameRT));
                GetPlayer(msg.TargetID).ChangeCard(targetCard);
            }
            else if (cardTableID == 4) // 壁垒
            {
                RecordAppend("{0}获得壁垒保护效果，一回合内无敌".FormatStr(fromPlayer.numTag));
                GetPlayer(msg.TargetID).InvincibleOneRound();
            }
            else if (cardTableID == 5) // 猜卡牌TableID
            {
                var targetCardTable = TableManager.instance.GetData<TableSosCard>(msg.TargetCardID);
                var targetCardNameRT = "<color=#ff8000>{0}</color>".FormatStr(targetCardTable.name);
                if (result == 0)
                {
                    RecordAppend("{0}猜测{1}错误！".FormatStr(fromPlayer.numTag, targetCardNameRT));
                }
                else
                {
                    RecordAppend("{0}猜测{1}正确，{2}出局！"
                        .FormatStr(fromPlayer.numTag, targetCardNameRT, target.numTag));
                }
            }
            else if (cardTableID == 6) // 决斗
            {
                if (result == 1)
                {
                    RecordAppend("{0}决斗 {1}胜利！"
                        .FormatStr(fromPlayer.numTag, target.numTag));
                }
                else
                {
                    RecordAppend("{0}决斗 {1}失败。"
                        .FormatStr(fromPlayer.numTag, target.numTag));
                }
            }
            else if (cardTableID == 7) // 霸道 太阳
            {
                RecordAppend("强制目标弃置手牌！");
            }
            else if (cardTableID == 8) // 交换
            {
                var targetCard = data.GetCard(msg.TargetCardID);
                RecordAppend("{0}效果后，你获得{1}".FormatStr(fromCard.effectRT, targetCard.nameRT));
                GetPlayer(msg.TargetID).ChangeCard(targetCard);
            }
            else if (cardTableID == 9) // 开溜（只限制出牌阶段，出牌类型，出牌后无效果）
            {

            }
            else if (cardTableID == 10)
            {
                RecordAppend("{0}主动使用{1}，出局！"
                        .FormatStr(fromPlayer.numTag, fromCard.nameRT));
            }

            RefreshUI();
        }

        public void OnDropCard(Message.CBPlayerDropCardSync msg)
        {
            var p = GetPlayer(msg.PlayerID);
            p.DropCard(data.GetCard(msg.CardID));
            RefreshUI();
        }

        public void OnPlayerOut(Message.CBPlayerOutSync msg)
        {
            var p = GetPlayer(msg.PlayerID);
            var handCard = data.GetCard(msg.HandCardID);
            p.Out(handCard);
            RefreshUI();
        }


        private List<Message.CBSendMessageSync> m_chatMsgs = new List<Message.CBSendMessageSync>();
        public void OnSendMessageSync(Message.CBSendMessageSync msg)
        {
            m_chatMsgs.Add(msg);

            chatMsgGroup.SetData<SosChatMessageItem, Message.CBSendMessageSync>(m_chatMsgs
            , (index, item, data) =>
            {
                var player = GetPlayer(data.FromPlayerID).data;
                item.SetData(player, data.Content);
            });

            var tw = uTools.uTweenFloat.Begin(chatMsgGroup.gameObject, chatMsgGroup.GetScrollRect().verticalNormalizedPosition, 0, 0.2f, 0);
            tw.onUpdate = () =>
            {
                chatMsgGroup.GetScrollRect().verticalNormalizedPosition = tw.value;
            };
        }

        void RefreshUI()
        {
            roomName.text = data.name;
            lastPlayedCardText.text = m_lastPlayedCard == null ? "" : m_lastPlayedCard.name;
            playedCardCount.text = data.playedCardCount.ToString();
            if (data.state == RoomData.State.Started)
            {
                clock.gameObject.SetActive(true);
            }
            else
            {
                clock.gameObject.SetActive(false);
            }
            mainPanel.RefreshUI();
        }

        private void Record(string text)
        {
            recordText.text = text;
        }

        private void RecordAppend(string text)
        {
            recordText.text = recordText.text + "\n" + text;
        }

        public SosPlayer GetPlayer(int playerID)
        {
            return m_players.First(a => a.data.id == playerID);
        }

        public void OnClickSendMessage()
        {
            if (!string.IsNullOrEmpty(chatInput.text))
            {
                GF.GetProxy<SosProxy>().SendChatMessage(chatInput.text);
                chatInput.text = "";
            }
        }
    }
}