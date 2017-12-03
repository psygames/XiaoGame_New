using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedStone.UI;
using System;

namespace RedStone
{
    public class MessageBoxView : ViewBase
    {
        public MessageBoxItem template;

        private List<MessageBoxItem> m_boxes = new List<MessageBoxItem>();

        private void Awake()
        {
            template.gameObject.SetActive(false);
        }

        public override void OnInit()
        {
            base.OnInit();

            for (int i = 0; i < 2; i++)
            {
                var item = GameObjectHelper.AddChild(transform, template);
                item.gameObject.SetActive(false);
                m_boxes.Add(item);
            }

            Register<MessageBoxEvent>(EventDef.MessageBox, OnEvent);
        }

        public void OnEvent(MessageBoxEvent evt)
        {
            var box = m_boxes.First(a => !a.isShowing);
            if (box != null)
            {
                box.SetTitle(evt.title);
                box.SetMessage(evt.msg);
                box.style = evt.style;
                box.callBackHandler = evt.callback;
                box.SetBtnOKDesc("");
                box.SetBtnCancelDesc("");
                box.Show();
            }
        }
    }

    public class MessageBoxEvent
    {
        public string title;
        public string msg;
        public MessageBoxStyle style;
        public Action<MessageBoxResult> callback;

        public MessageBoxEvent(string title, string msg, MessageBoxStyle style, Action<MessageBoxResult> callback)
        {
            this.title = title;
            this.msg = msg;
            this.style = style;
            this.callback = callback;
        }
    }

    public static class MessageBox
    {
        public static void Show(string title, string msg, MessageBoxStyle style, Action<MessageBoxResult> callback = null)
        {
            EventManager.instance.Send(EventDef.MessageBox, new MessageBoxEvent(title, msg, style, callback));
        }
    }
}
