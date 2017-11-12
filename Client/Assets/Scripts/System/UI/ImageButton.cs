using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

 namespace RedStone.UI
{
    // Button that's meant to work with mouse or touch-based devices.
	[RequireComponent(typeof(Image))]
	[RequireComponent(typeof(RectTransform))]
    public class ImageButton : Button
    {
		[SerializeField]
		public GameObject highlightedObject;

		[SerializeField]
		public GameObject pressedObject;

		[SerializeField]
		public GameObject disabledObject;

		private bool disableObject;

		[SerializeField]
		public bool hideHightlightWhenDisabled = false;

		public void ShowDisable(bool show)
		{
			disableObject = show;
			this.DoStateTransition (SelectionState.Normal, true); 
		}

		private void ProcessObject(SelectionState state = SelectionState.Normal)
		{
			if (highlightedObject != null)
			{
				if (hideHightlightWhenDisabled)
					highlightedObject.SetActive (!disableObject && interactable && (state == SelectionState.Highlighted || state == SelectionState.Normal));
				else
					highlightedObject.SetActive (!disableObject && (!interactable || state == SelectionState.Highlighted || state == SelectionState.Normal));
			}	
			if(pressedObject != null)
				pressedObject.SetActive(!disableObject && (interactable && (state == SelectionState.Pressed)));
			if(disabledObject != null)
				disabledObject.SetActive (disableObject || !interactable);
		}

		protected override void DoStateTransition (SelectionState state, bool instant)
		{
			base.DoStateTransition (state, instant);
			ProcessObject (state);
		}

	}
}
