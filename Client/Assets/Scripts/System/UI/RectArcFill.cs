using UnityEngine;
using System.Collections;
using RedStone.UI;
 namespace RedStone.UI
{
    [ExecuteInEditMode]
    public class RectArcFill : MonoBehaviour
    {
        public Image fillImage;
        [Range(0, 1)]
        public float fillAmount;
        public float angleFrom;
        public float angleTo;
        public float rectSize;
        public float radius;
        public bool clockwise;

        public float topZRotOffset;

        public float imageEdgeOffest = 0;


        public Image topImage;
        private RectTransform m_imageRectTrans;

        void Awake()
        {
            if (fillImage == null)
                fillImage = GetComponent<Image>();
            m_imageRectTrans = fillImage.GetComponent<RectTransform>();
        }

        void Update()
        {
            float angle = Mathf.Lerp(angleFrom, angleTo, fillAmount);
            Quaternion qua = Quaternion.AngleAxis(angle, Vector3.forward);

            topImage.transform.localPosition = qua * (Vector3.down * radius);
            topImage.transform.localRotation = qua * Quaternion.AngleAxis(topZRotOffset, Vector3.forward);

            float a = angle - angleFrom;
            a = (Mathf.PI / 180 * a);
            float h = rectSize;
            float r = radius;
            float m = Mathf.Cos((Mathf.PI - a) * 0.5f - Mathf.Acos(0.5f * h / r)) * 2 * r * Mathf.Sin(a * 0.5f);

            float rectFill = (m + imageEdgeOffest) / h;
            fillImage.fillAmount = rectFill;
        }
    }
}