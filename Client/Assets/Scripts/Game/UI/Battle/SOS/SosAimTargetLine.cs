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

        public const float maxAlpha = 0.8f;

        public void Awake()
        {
            gameObject.SetActive(false);
        }

        public void Show(Vector2 from, Vector2 to)
        {
            gameObject.SetActive(true);

            image.SetAlpha(maxAlpha);
            image.fillAmount = 0f;

            lineRect.sizeDelta = new Vector2((to - from).magnitude, 15);
            lineRect.localPosition = (from + to) * 0.5f;
            lineRect.localRotation = Quaternion.FromToRotation(Vector3.right, to - from);

            var tw2 = uTools.uTweenFloat.Begin(gameObject, 0, 1, 0.4f, 0);
            tw2.method = uTools.EaseType.easeOutCirc;
            tw2.onUpdate = () =>
            {
                image.fillAmount = tw2.value;
            };

            // Auto Hide
            Hide(3);
        }

        public void Hide(float delay = 0)
        {
            if (!gameObject.activeSelf)
                return;

            var tw = uTools.uTweenFloat.Begin(image.gameObject, Mathf.Min(image.color.a, maxAlpha), 0, 1, 0);
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