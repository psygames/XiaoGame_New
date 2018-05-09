using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedStone.UI;
using RedStone.Data.SOS;

namespace RedStone
{
    public class SosMainPanel : EventHandleItem
    {
        public Button playBtn;
        public Button readyBtn;
        public Button selectTargetBtn;
        public GameObject outObj;
        public Text playedCardText;
        public Image numBg;
        public Text numTagText;
        public SosMainPlayer mainPlayer;
        public SosGuessPanel guessPanel;
        public Transform cardRoot;
        public SosCard cardTemplate;
        public GameObject lockPanel;
        public Text lockPanelText;
        public Text selectPlayerText;

        public int m_guessCardID { get; private set; }
        private RoomData roomData { get { return sosProxy.room; } }
        private SosProxy sosProxy { get { return GF.GetProxy<SosProxy>(); } }

        public SosCard firstCard { get; private set; }
        public SosCard sndCard { get; private set; }
        private CardData m_selectedCard = null;
        private PlayerData m_selectedPlayer = null;

        private void Awake()
        {
            guessPanel.gameObject.SetActive(false);

            firstCard = GameObjectHelper.AddChild(cardRoot, cardTemplate);
            sndCard = GameObjectHelper.AddChild(cardRoot, cardTemplate);

            firstCard.onClickCallback = OnClickCard;
            sndCard.onClickCallback = OnClickCard;
        }

        public void OnClickPlayCard()
        {
            if (m_selectedCard == null)
            {
                Toast.instance.Show("必须选择一张卡牌!");
                return;
            }

            if (m_selectedCard.type == CardType.ForOneTarget
                && m_selectedPlayer == null)
            {
                Toast.instance.Show("请选择一个玩家作为释放对象！");
                return;
            }

            //当你手上有太阳航站或火星航站时，必须弃置水星航站。
            if (mainPlayer.data.handCards.Any(a => a.tableID == 9)
                && mainPlayer.data.handCards.Any(a => a.tableID == 6 || a.tableID == 7)
                && m_selectedCard.tableID != 9)
            {
                Toast.instance.Show("当你手上有太阳航站或火星航站时，必须弃置水星航站。");
                return;
            }

            if (m_selectedCard.tableID == 5 && m_guessCardID <= 0)
            {
                Toast.instance.Show("请选择一张猜测的卡牌！");
                guessPanel.Show();
                return;
            }

            GF.GetProxy<SosProxy>().PlayCard(m_selectedCard.id
                , m_selectedPlayer == null ? 0 : m_selectedPlayer.id
                , m_guessCardID <= 0 ? 0 : m_guessCardID);
        }

        public void OnClickPlayer(PlayerData playerData)
        {
            if (!mainPlayer.data.isTurned
                || m_selectedCard == null
                || m_selectedCard.type != CardType.ForOneTarget
                || playerData.state == PlayerData.State.Out)
                return;

            m_selectedPlayer = playerData;
            if (m_selectedCard.tableID == 5) // 猜测
            {
                m_guessCardID = -1;
                guessPanel.Show();
                guessPanel.onSelectedCallback = (a) =>
                {
                    m_guessCardID = a;
                };
            }

            RefreshUI();
        }

        public void NewTurn()
        {
            m_guessCardID = -1;
            m_selectedPlayer = null;
            m_selectedCard = null;
            RefreshUI();
        }

        void OnClickCard(CardData card)
        {
            if (card == null || m_selectedCard == card)
                return;

            m_guessCardID = -1;
            m_selectedCard = card;
            RefreshUI();
        }

        void OnClickReady()
        {
            sosProxy.Ready();
        }

        void OnClickSelectTarget()
        {

        }

        void OnClickLockPanel()
        {
            Toast.instance.Show("其他玩家回合，不能操作！");
        }

        public void RefreshUI()
        {
            if (roomData.state == RoomData.State.Started
                && !mainPlayer.data.isTurned
                && roomData.whosTurn != null)
            {
                lockPanelText.text = "当前为{0}回合".FormatStr(roomData.whosTurn.numTag);
                lockPanel.SetActive(true);
            }
            else
            {
                lockPanel.SetActive(false);
            }


            if (roomData.state == RoomData.State.WaitReady)
            {
                readyBtn.gameObject.SetActive(true);
                readyBtn.interactable = true;
                playBtn.gameObject.SetActive(false);
                outObj.SetActive(false);
            }
            else if (roomData.state == RoomData.State.WaitJoin)
            {
                readyBtn.interactable = false;
                readyBtn.gameObject.SetActive(true);
                playBtn.gameObject.SetActive(false);
                outObj.SetActive(false);
            }
            else if (roomData.state == RoomData.State.Started)
            {
                readyBtn.gameObject.SetActive(false);
                if (mainPlayer.data.state == PlayerData.State.Out)
                {
                    outObj.SetActive(true);
                    playBtn.gameObject.SetActive(false);
                }
                else
                {
                    outObj.SetActive(false);
                    playBtn.gameObject.SetActive(true);
                }
            }
            else if (roomData.state == RoomData.State.End)
            {
                readyBtn.gameObject.SetActive(false);
                playBtn.gameObject.SetActive(false);
                outObj.SetActive(false);
            }

            if (mainPlayer.lastPlayedCard == null)
                playedCardText.text = "空";
            else
                playedCardText.text = mainPlayer.lastPlayedCard.name;

            numBg.SetSprite("member_bg_" + mainPlayer.data.seat.ToString());
            numTagText.text = mainPlayer.data.seat.ToString();

            if (mainPlayer.data.handCards.Count >= 2)
                firstCard.SetData(mainPlayer.data.handCards[1]);
            else
                firstCard.SetData(null);
            sndCard.SetData(mainPlayer.data.oneCard);

            firstCard.BeSelected(m_selectedCard != null && m_selectedCard == firstCard.data);
            sndCard.BeSelected(m_selectedCard != null && m_selectedCard == sndCard.data);

            if (m_selectedCard != null && m_selectedCard.type == CardType.ForOneTarget)
            {
                if (m_selectedPlayer != null)
                    selectPlayerText.text = "目标{0}".FormatStr(m_selectedPlayer.numTag);
                else
                    selectPlayerText.text = "选择指定对象";
            }
            else
            {
                selectPlayerText.text = "无需目标";
            }
        }
    }
}