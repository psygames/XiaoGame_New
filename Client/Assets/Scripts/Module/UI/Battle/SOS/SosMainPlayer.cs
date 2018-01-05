using System.Collections;
using System.Collections.Generic;
using RedStone.Data.SOS;
using UnityEngine;
using System;

namespace RedStone
{
    public class SosMainPlayer : SosPlayer
    {
        public override void Init(PlayerData data)
        {
            base.Init(data);


        }
        protected List<SosCard> m_handCards = new List<SosCard>();
        public Action<CardData> onCardSelectedCallback { get; set; }

        public CardData selectedCard { get; private set; }
        protected override void OnCardClick(CardData card)
        {
            if (!data.isTurned)
                return;

            if (selectedCard == card)
            {
                selectedCard = null;
                GetCard(card.id).BeSelected(false);
                if (onCardSelectedCallback != null)
                    onCardSelectedCallback.Invoke(null);
                return;
            }


            if (selectedCard != null)
                GetCard(selectedCard.id).BeSelected(false);

            selectedCard = card;
            GetCard(selectedCard.id).BeSelected(true);

            if (onCardSelectedCallback != null)
                onCardSelectedCallback.Invoke(selectedCard);
        }

        public override void TakeCard(CardData cardData)
        {
            base.TakeCard(cardData);

            var card = GameObjectHelper.AddChild(cardRoot, cardTemplate);
            card.gameObject.SetActive(true);
            card.SetData(cardData);
            card.onClickCallback = OnCardClick;
            //TODO: Do animation or something else

            m_handCards.Add(card);
            RefreshUI();
        }

        public override void DropCard(CardData card)
        {
            base.DropCard(card);
        }

        public override void PlayCard(CardData card)
        {
            base.PlayCard(card);

            var sosCard = GetCard(card.id);
            if (sosCard != null)
            {
                selectedCard = null;
                m_handCards.Remove(sosCard);
                DestroyImmediate(sosCard.gameObject);
            }
        }

        public override void ChangeCard(CardData card)
        {
            base.ChangeCard(card);

            if (m_handCards.Count == 1)
                m_handCards[0].SetData(card);
            else
                Debug.LogError("手牌数量不正确，不能换牌！");

            RefreshUI();
        }

        public SosCard GetCard(int cardID)
        {
            return m_handCards.First(a => a.data.id == cardID);
        }

        public override void OnClick()
        {
            //TODO: Click Main Player
        }

        public override void Update()
        {
        }
    }
}