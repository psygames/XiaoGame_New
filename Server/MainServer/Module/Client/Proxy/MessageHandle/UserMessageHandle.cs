using System;
using System.Collections.Generic;
using System.Text;
using Message;
using RedStone.Data;

namespace RedStone
{

    public class UserMessageHandle
    {
        public string sessionID { get; private set; }
        private UserData data { get { return ProxyManager.instance.GetProxy<UserProxy>().GetData(sessionID); } }
        private Plugins.EventManager m_eventMgr = new Plugins.EventManager();

        public ClientDaoProxy dao
        {
            get
            {
                return ProxyManager.instance.GetProxy<ClientDaoProxy>();
            }
        }

        public void Init(string sessionID)
        {
            this.sessionID = sessionID;
            RegisterMsg<CMLoginRequest>(OnLogin);
        }

        private void OnLogin(CMLoginRequest msg)
        {
            Debug.Log(msg.DeviceID);
            var db = dao.Login(msg.DeviceID, sessionID);
            data.SetData(sessionID, db);
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
