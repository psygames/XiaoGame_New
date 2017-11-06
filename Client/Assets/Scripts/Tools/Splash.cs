using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Splash : MonoBehaviour
{
	public RawImage img;
	public float duration = 1f;
	private float m_cd = 0;

	void Awake()
	{
		m_cd = duration;

	}

	void Update () 
	{
		m_cd = Mathf.Max (0, m_cd - Time.deltaTime);
		if (m_cd <= 0)
			gameObject.SetActive (false);
		for (int i = 0; i < transform.parent.childCount; i++)
		{
			transform.parent.GetChild(i).GetComponent<RectTransform>().SetSiblingIndex(i+2);
		}
		transform.GetComponent<RectTransform> ().SetSiblingIndex (1);
		img.color = Color.Lerp (Color.white,Color.red,m_cd / duration);
	}
}
