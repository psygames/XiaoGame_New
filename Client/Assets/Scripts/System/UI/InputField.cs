using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

 namespace RedStone.UI
{
    /// <summary>
    /// Editable text input field.
    /// </summary>

	[AddComponentMenu("Project UI/Input Field", 31)]
	[RequireComponent(typeof(RectTransform))]
    public class InputField : UnityEngine.UI.InputField
    {
    }
}
