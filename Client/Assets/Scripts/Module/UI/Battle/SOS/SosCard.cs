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

        public Text point;
        public Text title;
        public Text description;
        public Text effect;
        public Image bg;
        public Image image;
        public Image titleBg;

        public Action<CardData> onClickCallback = null;

        public void SetData(CardData card)
        {
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

            image.SetSprite(card.image);
            bg.SetSprite(card.bg);
            titleBg.SetSprite(card.titleBg, false);
        }

        public void OnClick()
        {
            if (onClickCallback != null)
                onClickCallback.Invoke(data);
        }
    }
}