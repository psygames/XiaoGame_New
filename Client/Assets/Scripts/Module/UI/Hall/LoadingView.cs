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

            Register<LoadingState>(OnLoadingStateChanged);
        }

        void OnLoadingStateChanged(LoadingState state)
        {
            text.text = state.text;
            m_fillValue = state.percent;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            fill.fillAmount = Mathf.Lerp(fill.fillAmount, m_fillValue, Time.deltaTime * 5);
        }

        public class LoadingState
        {
            public int percent = 0;
            public string text = "";
            public int bgType = 1;
        }
    }
}
