using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

 namespace RedStone.UI
{
	[TweenEffect]
    [RequireComponent(typeof(CanvasGroup))]
    public class Flicker : MonoBehaviour
    {
        public float lowAlpha = 0f;
        public float highAlpha = 0.8f;
        public float interval = 0.05f;
        public float delayMinTime = 0f;
        public float delayMaxTime = 0.6f;

        public int flickerTimes = 5;

		WaitForSeconds waitForInterval = null;

        private CanvasGroup canvasGroup;

        void OnEnable()
        {
			if(canvasGroup == null)
				canvasGroup = this.GetComponent<CanvasGroup>();
            StartCoroutine(Play());
        }

        private IEnumerator Play()
        {
            float delayTime = Random.Range(delayMinTime, delayMaxTime);
            canvasGroup.alpha = 0;
            yield return new WaitForSeconds(delayTime);
            for (int i = 0; i < flickerTimes; i++)
            {
                canvasGroup.alpha = i % 2 == 0 ? lowAlpha : highAlpha;
				if (waitForInterval == null)
				{
					waitForInterval = new WaitForSeconds (interval);
				}
				yield return waitForInterval;
            }
            canvasGroup.alpha = 1;
        }

    }
}