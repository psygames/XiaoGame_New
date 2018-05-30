using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedStone.UI;

namespace RedStone
{
    public class HomeView : ViewBase
    {
        public GameObject matching;
        public Text gold;
        public Text level;
        public Text exp;

        private bool m_isMatching = false;

        public override void OnInit()
        {
            base.OnInit();

            Register(EventDef.MatchBegin, OnMatchBegin);
            Register(EventDef.MatchCancel, OnMatchCancel);
            Register(EventDef.MatchSuccess, OnMatchSuccess);
            Register(EventDef.PlayerInfoUpdate, OnPlayerInfoUpdate);
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

        void OnMatchSuccess()
        {
            m_isMatching = false;
            RefreshUI();
            //TODO: Match Success Effect
            Debug.Log("Match Success!!!");
        }

        void OnPlayerInfoUpdate()
        {
            RefreshUI();
        }

        private void RefreshUI()
        {
            if (!isActiveAndEnabled)
                return;
            matching.SetActive(m_isMatching);
            var data = GF.GetProxy<HallProxy>().playerData;

            gold.text = "金币: {0}".FormatStr(data.gold);
            level.text = "等级: {0}".FormatStr(data.level);
            exp.text = "经验值: {0}/{1}".FormatStr(data.exp, data.levelUpExp);
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

        void OnClickExit()
        {
            MessageBox.Show("退出游戏", "确定退出游戏吗？", MessageBoxStyle.OKCancelClose, (res) =>
               {
                   if (Application.isEditor && UnityEditor.EditorApplication.isPlaying)
                       UnityEditor.EditorApplication.isPlaying = false;
                   else
                       Application.Quit();
               });
        }
    }
}
