using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedStone.UI;
using RedStone.Data.SOS;
using System;

namespace RedStone
{
    public class SosGuessCardItem : EventHandleItem
    {
        public Text content;
        public Image bg;
        private Action<TableSosCard> m_callback = null;
        private TableSosCard m_data = null;

        public void SetData(TableSosCard data, Action<TableSosCard> callback)
        {
            this.m_data = data;
            this.m_callback = callback;
            content.text = data.name.Substring(0, data.name.Length - 2);
            bg.color = Color.white;
        }

        public void OnEnter()
        {
            bg.color = Color.grey;
        }

        public void OnLeave()
        {
            bg.color = Color.white;
        }

        public void OnClick()
        {
            if (m_callback != null)
                m_callback.Invoke(m_data);
        }
    }
}