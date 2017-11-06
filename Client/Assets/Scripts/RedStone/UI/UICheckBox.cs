using UnityEngine;
using System;
using System.Collections;
namespace Hotfire.UI
{
	[RequireComponent(typeof(RectTransform))]
	public class UICheckBox : MonoBehaviour
    {
        public UUIEventListener listener;
        public Toggle toggle;
        public Text label;
        public int id;
        public Action<int> callback;
        public bool selected
        {
            get { return toggle.isOn; }
            set { toggle.isOn = value; }
        }

        public void Start()
        {
            listener.onClick += OnValueChange;
        }

        public void OnValueChange(UUIEventListener listener)
        {//设为true的时候才调用
            if (callback != null)
            {
                callback(id);
            }
        }
    }
}