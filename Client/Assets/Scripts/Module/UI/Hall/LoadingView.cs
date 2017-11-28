using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedStone.UI;

namespace RedStone
{
    public class LoadingView : ViewBase
    {
        public Text text;
        public Image fill;

        private float m_fillValue = 0;
        public override void OnInit()
        {
            base.OnInit();
            Register<LoadingStatus>(MessageDefine.HallLoading, OnLoadingStateChanged);
        }

        public override void OnOpen()
        {
            base.OnOpen();
            fill.fillAmount = 0;
            text.text = "";
        }

        void OnLoadingStateChanged(LoadingStatus status)
        {
            text.text = status.text;
            m_fillValue = status.percent * 0.01f;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            fill.fillAmount = Mathf.Lerp(fill.fillAmount, m_fillValue, Time.deltaTime * 5);
        }

    }

    public class LoadingStatus
    {
        public float percent = 0;
        public string text = "";
        public int bgType = 1;

        public LoadingStatus(string text, float percent, int bgType = 1)
        {
            this.percent = percent;
            this.text = text;
            this.bgType = bgType;
        }
    }
}
