using UnityEngine;
using System.Collections;
using System;


 namespace RedStone.UI
{
	public enum EClickType
	{
		HandleByParent,
		CloseParent,
		SendEvent,
		ShowModule,
		CloseAndShowModule,
	}
	[Serializable]
	public class ClickEventHandler
	{

		[SerializeField]
		private EClickType m_clickType = EClickType.HandleByParent;

		[SerializeField]
		private string m_showModuleName;

		[SerializeField]
		private string m_Event = "";
		public string Event
		{
			get
			{
				return m_Event;
			}
			set
			{
				m_Event = value;
			}
		}

		public object sendEvent
		{
			get;
			set;
		}
		public void OnEvent(Component comp)
		{
			OnEvent (comp, "OnClick", m_clickType, m_Event, m_showModuleName, sendEvent);
		}
		public void OnEvent (Component comp, string prefix, EClickType clickType, string evt, string moduleName, object evtBody)
		{
			if (clickType == EClickType.HandleByParent)
			{
				comp.DispatchEvent (prefix + evt);
			} 
			else
			{
				if (prefix == "OnClick")
				{
					if (clickType == EClickType.CloseParent || clickType == EClickType.CloseAndShowModule)
					{
						comp.DispatchEvent ("Close");
					}
				}
			}
		}
	}

}