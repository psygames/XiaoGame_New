using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedStone.UI;

namespace RedStone
{
    public class SosAimTargetLine : MonoBehaviour
    {
        public RectTransform lineRect;
        public Image image;

        public void Awake()
        {
            gameObject.SetActive(false);
        }

        public void Show(Vector2 from, Vector2 to)
        {
            gameObject.SetActive(true);

            image.SetAlpha(0.7f);
            lineRect.sizeDelta = new Vector2((to - from).magnitude, 15);
            lineRect.localPosition = (from + to) * 0.5f;
            lineRect.localRotation = Quaternion.FromToRotation(Vector3.right, to - from);
            image.fillAmount = 0f;

            var tw2 = uTools.uTweenFloat.Begin(gameObject, 0, 1, 0.2f, 0);
            tw2.method = uTools.EaseType.easeOutCirc;
            tw2.onUpdate = () =>
            {
                image.fillAmount = tw2.value;
            };

            var tw = uTools.uTweenFloat.Begin(image.gameObject, 0.7f, 0, 1, 1);
            tw.method = uTools.EaseType.easeInCubic;
            tw.onUpdate = () =>
            {
                image.SetAlpha(tw.value);
            };

            tw.onFinishedAction = (a) =>
            {
                gameObject.SetActive(false);
            };
        }
    }
}