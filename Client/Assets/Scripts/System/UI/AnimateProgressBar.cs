using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 namespace RedStone.UI
{
	public class AnimateProgressBar : Slider
	{

		[SerializeField]
		public float animLength = 2f;

		public int[] valueStep;

		private int m_startValue = 0;

		public int startValue
		{
			get {
				return m_startValue;
			}
			set {
				m_startValue = value;
				ResetProgress ();
			}
		}

		private float m_currentValue = 0f;

		private int m_targetValue = 100;

		public int targetValue
		{
			get {
				return m_targetValue;
			}
			set {
				m_targetValue = value;
				ResetProgress ();
			}
		}


		private float animInterval = 0.1f;

		private int m_currentIndex = 0;
		public System.Action<int> onUpdate;

		private void ResetProgress()
		{
			m_currentValue = (float)m_startValue;
			m_currentIndex = 0;
			animInterval = ((float)m_targetValue - m_currentValue) / animLength;
			value = 0f;
		}
		void Update()
		{
			if (valueStep == null || valueStep.Length == 0 || m_currentIndex >= valueStep.Length) 
			{
				value = 1f;
				return;
			}
			if ((int)m_currentValue >= m_targetValue) 
				return;
			m_currentValue += animInterval * Time.deltaTime;
			if ((int)m_currentValue >= m_targetValue) 
			{
				m_currentValue = m_targetValue;
			}
			while (m_currentIndex < valueStep.Length  && m_currentValue >= (float)valueStep [m_currentIndex]) 
			{
				++m_currentIndex;
			}
			if (m_currentIndex >= valueStep.Length) 
			{
				value = 1f;
				if (onUpdate != null)
					onUpdate.Invoke ((int)m_currentValue);
				return;
			}
			float min = (float)(m_currentIndex == 0 ? 0 : valueStep [m_currentIndex - 1]);
			float max = (float) valueStep [m_currentIndex];

			value = (m_currentValue - min) / (max - min);
			if (onUpdate != null)
				onUpdate.Invoke ((int)m_currentValue);
		}
	}
}

