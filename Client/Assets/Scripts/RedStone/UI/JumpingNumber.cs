using Coolfish.System;
using UnityEngine;

namespace Hotfire.UI
{
	[RequireComponent (typeof(UnityEngine.UI.Text))]
	[DisallowMultipleComponent]
	public class JumpingNumber : MonoBehaviour
	{
		Text m_text;

		public int startNumber = 0;
		public int targetNumber = 0;

		public float duration = 0.5f;

		private float m_currentNumber = 0;

		public string format = "{0}";
		public Color normalColor = Color.white;
		public Color increaseColor = Color.green;
		public Color decreaseColor = Color.red;

		public bool jumping = true;
		public bool reset = false;
		// Use this for initialization
		void Start ()
		{
			m_text = GetComponent<Text> ();
		}
	
		public void Reset()
		{
			m_currentNumber = (int)startNumber;
			if (m_text != null)
			{
				SetText (startNumber);
				m_text.color = normalColor;
			}
			reset = false;

		}
		public static void Play(Text text, int start, int target, float duration, string format = "{0}")
		{
			Play (text, start, target, duration, Color.white, Color.green, Color.red);
		}
		public static void Play(Text text, int start, int target, float duration, Color normalColor, Color increaseColor, Color decreaseColor, string format = "{0}")
		{
			if (text == null)
				return;
			var jn = text.GetComponent<JumpingNumber>();
			if (jn == null)
				jn = text.gameObject.AddComponent<JumpingNumber> ();
			jn.normalColor = normalColor;
			jn.increaseColor = increaseColor;
			jn.decreaseColor = decreaseColor;
			jn.duration = duration;
			jn.Play(start, target);
		}
		public void Play(int start, int target)
		{
			startNumber = start;
			targetNumber = target;
			Reset ();
			jumping = true;
		}

		void SetText (int currentNumberInt)
		{
			m_text.text = format == "{0}" ? currentNumberInt.ToString () : format.FormatStr(currentNumberInt);
		}

		// Update is called once per frame
		void Update ()
		{
			if (m_text == null)
				return;
			if (reset)
				Reset ();
			if (!jumping)
				return;
			//Profiler.BeginSample ("Calculate");
			float deltaTime = UnityEngine.Time.deltaTime;
			int currentNumberInt = (int)m_currentNumber;
			if (targetNumber == startNumber || duration == 0 )
			{
				m_currentNumber = (float)targetNumber;
				SetText (targetNumber);
				m_text.color = normalColor;
				jumping = false;
			} else
			{
				var color = (targetNumber > startNumber ? increaseColor : decreaseColor);
				var delta = (targetNumber - startNumber) / duration;
				m_currentNumber += delta * deltaTime;
				if ((targetNumber > startNumber && m_currentNumber > targetNumber)
				    || (targetNumber < startNumber && m_currentNumber < targetNumber))
				{
					m_currentNumber = (float)targetNumber;
					currentNumberInt = targetNumber;
					SetText (targetNumber);
					m_text.color = color;
					jumping = false;
				} else
				{
					currentNumberInt = (int)m_currentNumber;
					SetText (currentNumberInt);
					m_text.color = color;
				}
			}
		}
	}


}