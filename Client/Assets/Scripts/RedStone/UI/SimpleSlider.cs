using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Hotfire.UI
{
	[AddComponentMenu("UI/SimpleSlider")]
	[RequireComponent(typeof(RectTransform))]
	public class SimpleSlider : UIBehaviour
{
		public Image targetGraphic;

		public bool relativeValue = false;
		[Range(0,1f)]
		public float minValue = 0f;
		[Range(0,1f)]
		public float maxValue = 1f;
		[SerializeField]
		[Range(0,1f)]
		protected float m_Value;
		public virtual float value
		{
			get
			{
				return m_Value;
			}
			set
			{
				Set(value);
			}
		}
		#if UNITY_EDITOR
		protected override void OnValidate ()
		{
			value = m_Value;
			base.OnValidate ();
		}
		#endif
		public bool vertical = false;
		private RectTransform m_RectTransform;
		public RectTransform rectTransform
		{
			get {
				if (m_RectTransform == null)
					m_RectTransform = GetComponent<RectTransform> ();
				return m_RectTransform;
			}
		}

		private void Set(float targetValue)
		{
			if (targetGraphic == null)
				return;
			targetGraphic.rectTransform.pivot = rectTransform.pivot;
			maxValue = Mathf.Clamp01 (maxValue);
			minValue = Mathf.Clamp (minValue, 0, maxValue);
			m_Value = Mathf.Clamp01(targetValue);
			if(!relativeValue)
				targetValue = m_Value = Mathf.Clamp(targetValue, minValue, maxValue);
			else
				targetValue = minValue + m_Value * (maxValue - minValue);
			if (m_Value <= 0)
				targetValue = 0f;
			var targetSize = rectTransform.sizeDelta;
			var size = targetSize;
			if (vertical)
			{
				targetSize.y = size.y * targetValue;
			} else
			{
				targetSize.x = size.x * targetValue;
			}
			targetGraphic.rectTransform.anchorMin = Vector3.zero;
			targetGraphic.rectTransform.anchorMax = Vector3.one;
			targetGraphic.rectTransform.sizeDelta = targetSize - size;
			targetGraphic.rectTransform.anchoredPosition3D = Vector3.zero;
		}
}

}