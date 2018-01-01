using System.Collections;
using System.Collections.Generic;
using RedStone.Data.SOS;
using UnityEngine;

namespace RedStone
{
    public class SosMainPlayer : SosPlayer
    {
        public override void Init(PlayerData data)
        {
            base.Init(data);


        }
        protected List<SosCard> m_handCards = new List<SosCard>();

        public CardData selectedCard { get; private set; }
        protected override void OnCardClick(CardData card)
        {
            if (!data.isTurned)
                return;

            if (selectedCard == card)
            {
                selectedCard = null;
                GetCard(card.id).BeSelected(false);
                return;
            }


            if (selectedCard != null)
                GetCard(selectedCard.id).BeSelected(false);

            selectedCard = card;
            GetCard(selectedCard.id).BeSelected(true);
        }

        public override void TakeCard(int cardID)
        {
            base.TakeCard(cardID);

            var cardData = data.GetHandCard(cardID);
            var card = GameObjectHelper.AddChild(cardRoot, cardTemplate);
            card.gameObject.SetActive(true);
            card.SetData(cardData);
            card.onClickCallback = OnCardClick;
            //TODO: Do animation or something else

            m_handCards.Add(card);
            RefreshUI();
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