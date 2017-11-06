using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Hotfire.UI
{
    [RequireComponent(typeof(Animator))]
    public class UIAnimator : MonoBehaviour
    {
        private Animator m_animator;
        public string lastTrigger { get; private set; }

        void Awake()
        {
            m_animator = GetComponent<Animator>();
        }

        void OnDisable()
        {
            lastTrigger = "";
        }

        public void Trigger(string triggerName)
        {
            if (m_animator == null)
                return;
            m_animator.SetTrigger(triggerName);
            lastTrigger = triggerName;
        }

        public void Play(params int[] indexes)
        {

        }

        public void Stop()
        {
            Trigger("Stop");
        }

        public void StopAllLayers()
        {

        }
    }
}

