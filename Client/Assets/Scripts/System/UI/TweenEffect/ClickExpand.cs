using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using uTools;

 namespace RedStone.UI
{
	[TweenEffect]
	public class ClickExpand : MonoBehaviour, IPointerClickHandler
	{
		public GameObject expandObject;
		public float expandScale = 1.5f;
		public float expandDuration = 0.2f;
		public bool fadeout = true;
		void OnEnable()
		{
			Hide (expandObject);
		}
		void OnDisable()
		{
			Hide(expandObject);
		}
		void Hide(GameObject go = null)
		{
			if (go == null)
				return;
			go.SetActive (false);
		}
		public void OnPointerClick (PointerEventData eventData)
		{
			if (expandObject == null)
				return;
			expandObject.SetActive (true);
			var scale = uTweenScale.Begin (expandObject, Vector3.one, new Vector3 (expandScale, expandScale, 1f), expandDuration);
			scale.onFinishedAction = Hide;
			if (fadeout)
				uTweenAlpha.Begin (expandObject, 1f, 0f, expandDuration, 0, true);
		}
	}

}