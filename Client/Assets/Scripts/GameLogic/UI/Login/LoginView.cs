using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace RedStone
{
	public class LoginView : ViewBase
	{
		public UIEventListener playListener;
		public GameObject matchingObj;
		public RawImage bg;
		private bool isMatching = false;

		public override void OnInit()
		{
			base.OnInit();
			isBottom = true;
			playListener.onClick = OnClickPlay;
			Register(Event.Gomuku.AssignRoomReply, OnAssignRoomReply);
		}

		public override void OnDestory()
		{
			UnRegister(Event.Gomuku.AssignRoomReply, OnAssignRoomReply);

			base.OnDestory();
		}

		public override void OnOpen()
		{
			base.OnOpen();
			//string url = "http://img1.3lian.com/2015/w3/17/d/62.jpg";
			//ResourceManager.instance.Load<Texture>(url, (obj) =>
			//{
			//	bg.texture = obj;
			//	bg.SetNativeSize();
			//});
		}

		public void OnAssignRoomReply()
		{
			isMatching = false;
		}

		public void OnClickPlay(UIEventListener listener)
		{
			GetProxy<HallProxy>().AssignRoom();
			isMatching = true;
		}

		void Update()
		{
			UpdateMatching();
		}

		private void UpdateMatching()
		{
			if (isMatching)
			{
				playListener.gameObject.SetActive(false);
				matchingObj.SetActive(true);
			}
			else
			{
				playListener.gameObject.SetActive(true);
				matchingObj.SetActive(false);
			}
		}
	}
}