using System;

using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

namespace Hotfire.UI
{
    [AddComponentMenu("Project UI/Toggle Group", 32)]
	[DisallowMultipleComponent]
	[RequireComponent(typeof(RectTransform))]
    public class ToggleGroup : UnityEngine.UI.ToggleGroup
    {
        private bool m_isLastAllSwitchOff = false;
        public Action onAllSwitchOff;

        public void Update()
        {
            CheckAllSwicthOff();
        }

        private void CheckAllSwicthOff()
        {
            bool isAllOff = !AnyTogglesOn();
            if (isAllOff && !m_isLastAllSwitchOff)
            {
                if (onAllSwitchOff != null)
                    onAllSwitchOff.Invoke();
            }
            m_isLastAllSwitchOff = isAllOff;
        }
    }
}
