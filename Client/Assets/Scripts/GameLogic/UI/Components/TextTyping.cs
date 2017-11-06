using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextTyping : MonoBehaviour
{
	public Text target;
	public string prefix;
	public string typeText;
	public float typeInterval = 0.5f;

	private float m_typeTime;
	private int typeIndex = 0;
	void Start()
	{
		m_typeTime = typeInterval;
	}

	void Update()
	{
		m_typeTime -= Time.deltaTime;
		if (m_typeTime <= 0)
		{
			m_typeTime += typeInterval;
			typeIndex = (typeIndex + 1) % (typeText.Length + 1);
		}

		target.text = prefix + typeText.Substring(0, typeIndex);
	}
}
