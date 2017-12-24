using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedStone.UI;
using System;

namespace RedStone
{
    public class SosCard : EventHandleItem
    {
        private Data.CardData m_data = null;

        public Text point;
        public Text title;
        public Text description;
        public Text effect;

        public Action<Data.CardData> onClickCallback = null;

        public void SetData(Data.CardData card)
        {
            this.m_data = card;
            this.point.text = card.point.ToString();
            this.title.text = card.name;
            this.description.text = card.table.description;
            this.effect.text = card.table.effect;
        }

        public void OnClick()
        {
            if (onClickCallback != null)
                onClickCallback.Invoke(m_data);
        }
    }
}