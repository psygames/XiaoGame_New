using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Hotfire.UI
{
	[RequireComponent(typeof(RectTransform))]
	public class SpriteFrameAnimator : MonoBehaviour
	{

		public string[] m_spriteNames = null;

		public Image img1;
		public Image img2;

		public float animatorSpeed = 0.5f;

		private float m_currentTime = 0f;

		void OnEnable()
		{
			Reset ();
		}

		void Update ()
		{
			if (m_spriteNames == null || m_spriteNames.Length < 2 || img1 == null || animatorSpeed == 0f)
				return;

			int count = m_spriteNames.Length;
			int currentIndex = ((int)(m_currentTime / animatorSpeed)) % count;
			float factor = m_currentTime % animatorSpeed;
			img1.SetSprite (m_spriteNames [currentIndex]);
			if (img2 != null)
			{
				img1.SetAlpha (1f - factor);
				int nextIndex = (currentIndex + 1) % count;
				img2.SetSprite (m_spriteNames [nextIndex]);
				img2.SetAlpha (factor);
			}
			m_currentTime += Time.deltaTime;
			if (m_currentTime > count * animatorSpeed)
				m_currentTime = 0f;
		}

		public void Init(string[] spriteNames)
		{
			m_spriteNames = spriteNames;
		}

		public void Reset()
		{
			m_currentTime = 0f;
			if (img1 != null)
				img1.SetAlpha (1f);
			if (img2 != null)
				img2.SetAlpha (1f);
		}
	}
}

