using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedStone.UI;
using System;
using RedStone.Data.SOS;

namespace RedStone
{
    public class SosCard : EventHandleItem
    {
        public CardData data { get; private set; }

        public GameObject normalCard;
        public GameObject emptyCard;
        public Text point;
        public Text title;
        public Text description;
        public Text effect;
        public Image bg;
        public Image border;

        public Action<CardData> onClickCallback { get; set; }
        public bool isSelected { get; private set; }

        private void Awake()
        {
        }

        public void SetData(CardData card)
        {
            if (card == null)
            {
                normalCard.SetActive(false);
                emptyCard.SetActive(true);
                return;
            }

            normalCard.SetActive(true);
            emptyCard.SetActive(false);
            this.data = card;
            if (card.point >= 10)
            {
                this.point.text = "∞";
            }
            else
            {
                this.point.text = card.point.ToString();
            }
            this.title.text = card.name;
            this.description.text = card.table.description;
            this.effect.text = card.table.effect;

            bg.SetSprite(card.bg);
        }

        public void OnDrag(Vector2 delta)
        {
        }

        public void OnClick()
        {
            if (onClickCallback != null)
                onClickCallback.Invoke(data);
        }

        public void BeSelected(bool isSelected)
        {
            this.isSelected = isSelected;

            if (isSelected)
                border.SetSprite("card_border_selected", false);
            else
                border.SetSprite("card_border_normal", false);
        }
    }
}