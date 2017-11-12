using UnityEngine;
using System.Collections;
using System.Collections.Generic;


 namespace RedStone.UI
{
    public class UIAnimatorTest : MonoBehaviour
    {
        public string triggerName;
        public bool activeTrigger = false;

        private Animator m_animator;

        void Awake()
        {
            m_animator = GetComponent<Animator>();
        }

        void Update()
        {
            CheckActTrigger();
        }

        void Trigger()
        {
            m_animator.SetTrigger(triggerName);
        }

        void CheckActTrigger()
        {
            if (activeTrigger)
            {
                activeTrigger = false;
                Trigger();
            }
        }
    }
}

