using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedStone.UI;
using RedStone.Data.SOS;
using System;

namespace RedStone
{
    public class SosPlayer : EventHandleItem
    {
        public PlayerData data { get; private set; }
        public Transform cardRoot;
        public SosCard cardTemplate;
        public Transform playedCardRoot;

        public Image headIcon;
        public Image headIconBg;
        public Text playerName;
        public Image effect;
        public Text state;
        public UIRepeat backCardRepeat;

        protected SosCard m_playedCard = null;

        public bool isSelected { get; private set; }

        public Action<PlayerData> onClickCallback = null;

        public void Reset()
        {
            m_lastPlayedCardData = null;
        }

        public virtual void Init(PlayerData data)
        {
            this.data = data;

            Reset();
            m_playedCard = GameObject.Instantiate(cardTemplate).GetComponent<SosCard>();
            UIHelper.SetParent(playedCardRoot, m_playedCard.transform);

            headIcon.enableGrey = true;
            m_playedCard.bg.enableGrey = true;
            m_playedCard.image.enableGrey = true;


            playerName.text = data.name;
            headIcon.SetSprite("user_icon_man" + UnityEngine.Random.Range(0, 4)); //TODO:Head Icon

            RefreshUI();
        }

        protected virtual void OnCardClick(CardData card)
        {

        }

        protected void RefreshUI()
        {
            state.text = data.state.ToString();
            if (isSelected)
                headIconBg.SetSprite("common_border_red", false);
            else if (data.isMain)
                headIconBg.SetSprite("common_border_blue", false);
            else
                headIconBg.SetSprite("common_border_white", false);

            if (backCardRepeat != null)
                backCardRepeat.repeatCount = data.handCards.Count;

            if (data.state == PlayerData.State.Out)
            {
                headIcon.Grey = true;
            }
            else
            {
                headIcon.Grey = false;
            }

            effect.gameObject.SetActive(data.effect == PlayerData.Effect.InvincibleOneRound);

            RefreshPlayedCard(false);
        }

        private CardData m_lastPlayedCardData = null;
        void RefreshPlayedCard(bool ignoreTurn)
        {
            if (m_lastPlayedCardData != null
                && (GF.GetProxy<SosProxy>().room.state == RoomData.State.End
                || ignoreTurn || !data.isTurned))
            {
                m_playedCard.gameObject.SetActive(true);
                m_playedCard.SetData(m_lastPlayedCardData);
                if (data.state == PlayerData.State.Out)
                {
                    m_playedCard.bg.Grey = true;
                    m_playedCard.image.Grey = true;
                }
                else
                {
                    m_playedCard.bg.Grey = false;
                    m_playedCard.image.Grey = false;
                }
            }
            else
            {
                m_playedCard.gameObject.SetActive(false);
            }
        }

        public void BeSelected(bool isSelected)
        {
            this.isSelected = isSelected;
            RefreshUI();
        }

        public virtual void TakeCard(CardData card)
        {
            RefreshUI();
        }

        public virtual void ChangeCard(CardData card)
        {
            RefreshUI();
        }

        public virtual void DropCard(CardData card)
        {
            //TODO: Drop Card
            PlayCard(card);
        }

        public virtual void PlayCard(CardData card)
        {
            m_lastPlayedCardData = card;
            RefreshUI();
            RefreshPlayedCard(true);
        }

        public virtual void Out(CardData handCard)
        {
            //TODO: OUT Effect
            Toast.instance.Show("{0} 出局".FormatStr(data.name));

            DropCard(handCard);
            PlayOutEffect();
            RefreshUI();
        }

        public void InvincibleOneRound()
        {
            //TODO: Effect
        }

        public void CancelInvincible()
        {

        }

        void PlayOutEffect()
        {
            //TODO:Play Out Effect
        }

        public void SetReady(bool isReady)
        {
            RefreshUI();
        }

        public virtual void OnClick()
        {
            if (onClickCallback != null)
                onClickCallback.Invoke(data);
        }

        public virtual void Update()
        {

        }
    }
}