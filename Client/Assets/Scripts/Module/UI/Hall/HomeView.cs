using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedStone.UI;

namespace RedStone
{
    public class HomeView : ViewBase
    {
        public Text btnText;
        public GameObject matching;

        private bool m_isMatching = false;

        public override void OnInit()
        {
            base.OnInit();

            Register(EventDef.MatchBegin, OnMatchBegin);
            Register(EventDef.MatchCancel, OnMatchCancel);
        }

        public override void OnOpen()
        {
            base.OnOpen();

            RefreshUI();
        }

        private void OnMatchBegin()
        {
            m_isMatching = true;
            RefreshUI();
        }
        private void OnMatchCancel()
        {
            m_isMatching = false;
            RefreshUI();
        }

        private void RefreshUI()
        {
            if (m_isMatching)
            {
                btnText.text = "取消";
            }
            else
            {
                btnText.text = "开始";
            }
            matching.SetActive(m_isMatching);
        }

        void OnClickBattle()
        {
            if (!m_isMatching)
            {
                GF.GetProxy<HallProxy>().BeginMatch(1, 1);
            }
            else
            {
                GF.GetProxy<HallProxy>().CancelMatch();
            }
        }
    }
}
