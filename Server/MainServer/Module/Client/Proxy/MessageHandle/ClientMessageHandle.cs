using System;
using System.Collections.Generic;
using System.Text;
using Message;

namespace RedStone
{

    public class ClientMessageHandle
    {
        public string sessionID { get; private set; }
        public PlayerData data = new PlayerData();
        private Plugins.EventManager m_eventMgr = new Plugins.EventManager();

        public void Init()
        {
            RegisterMsg<CMLoginRequest>(OnLogin);
        }

        private void OnLogin(CMLoginRequest login)
        {
            Debug.Log(login.DeviceID);
        }





        private void RegisterMsg<T>(Action<T> action)
        {
            m_eventMgr.Register(typeof(T).Name, action);
        }

        public void OnMessage(object message)
        {
            m_eventMgr.Send(message.GetType().Name, message);
        }
    }
}
