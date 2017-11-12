using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using uTools;

 namespace RedStone.UI
{
    [TweenEffect]
    public class Breath : MonoBehaviour
    {
        public float lowAlpha = 0f;
        public float highAlpha = 1.0f;
        public float time = 2f;

        public uTweenAlpha tween { get; private set; }

        public bool includeChildren = false;

        //void Awake()
        //{
        //    canvasGroup = this.GetComponent<CanvasGroup>();
        //}

        void OnEnable()
        {
            tween = uTweenAlpha.Begin(this.gameObject, lowAlpha, highAlpha, time, 0, includeChildren);
            tween.style = uTweener.Style.PingPong;
        }

        void Update()
        {
            tween.duration = time;
            tween.from = lowAlpha;
            tween.to = highAlpha;
        }
    }
}