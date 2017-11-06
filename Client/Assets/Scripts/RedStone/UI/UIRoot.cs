using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hotfire.UI
{
    public class UIRoot : MonoBehaviour
    {
        private Canvas m_Canvas;
        private Queue<string> m_PopupWindowQueue = new Queue<string>();
        private Dictionary<string, string> m_PageStorageData = new Dictionary<string, string>();
        protected void Start()
        {
            m_Canvas = GetComponent<Canvas>();
        }
        public Canvas canvas
        {
            get { return m_Canvas; }
        }
        public void ShowWaiting()
        {
            throw new NotImplementedException();
        }
        public void ShowTips(string message)
        {
            throw new NotImplementedException();
        }
        public void ShowAlert(string message, string title = null, AlertStyle style = AlertStyle.Ok, Action okAction = null, Action cancelAction = null)
        {
            throw new NotImplementedException();
        }
        //打开页面
        public void OpenPage(string pageName)
        {
            throw new NotImplementedException();
        }
        //弹出窗口
        public void PopupWindow(string windowName, bool closeable, params object[] parameters)
        {
            throw new NotImplementedException();
        }

    }
}
