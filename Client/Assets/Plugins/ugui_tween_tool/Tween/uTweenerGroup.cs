using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace uTools
{
    public class uTweenerGroup : MonoBehaviour
    {
        public bool onEnablePlay = false;
        public bool autoDeactive = false;

        public Action<uTweener> onFinish { get; set; }
        public Action onAllFinish { get; set; }

        private List<uTweener> tweeners = new List<uTweener>();
        private Action<uTweener> m_twEvt;

        void OnEnable()
        {
            if (onEnablePlay)
                Play();
        }

        void Awake()
        {
            ReCheckTweeners();
        }

        public void ReCheckTweeners()
        {
            var tws = GetComponentsInChildren<uTweener>(true);
            tweeners.Clear();
            foreach (var tw in tws)
            {
                if (CheckRule(tw))
                {
                    tweeners.Add(tw);
                    tw.onFinishedAction = (a) =>
                    {
                        OnFinish(tw);
                    };
                }
            }
        }

        public void Play()
        {
            if (tweeners == null)
                return;

            foreach (var tw in tweeners)
            {
                tw.enabled = true;
                tw.ResetToBeginning();
            }
        }

        private bool CheckRule(uTweener tw)
        {
            if (tw.style == uTweener.Style.Once || tw.style == uTweener.Style.PingPongOnce)
                return true;
            return false;
        }

        private void OnFinish(uTweener tw)
        {
            if (onFinish != null)
                onFinish.Invoke(tw);

            // check all finish 
            bool isAllFinish = true;
            foreach (var ttw in tweeners)
            {
                if (tw != ttw && ttw.enabled)
                    isAllFinish = false;
            }

            if (isAllFinish)
            {
                if (autoDeactive)
                    gameObject.SetActive(false);

                if (onAllFinish != null)
                    onAllFinish.Invoke();
            }
        }
    }
}