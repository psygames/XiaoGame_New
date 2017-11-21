using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Message;
using RedStone.Data;

namespace RedStone
{
    public class BattleServerProxy : MBProxyBase
    {
        private Dictionary<string, BattleServerData> m_datas = new Dictionary<string, BattleServerData>();


        public BattleServerData GetData(string sessionID)
        {
            return m_datas[sessionID];
        }

        public BattleServerData GetBestBattleServer()
        {
            // TODO: Best Battle Server , using ping or status
            return m_datas.Values.First();
        }


        public override void OnInit()
        {
            base.OnInit();

            network.server.onConnected = OnConnected;
            network.server.onClosed = OnClosed;
            RegisterMessage<BMLoginRequest>(OnLogin);
        }

        void OnConnected(string sessionID)
        {
            if (!m_datas.ContainsKey(sessionID))
            {
                var data = new BattleServerData();
                data.SetSessionID(sessionID);
                m_datas.Add(sessionID, data);
            }
        }

        void OnClosed(string sessionID)
        {
            m_datas.Remove(sessionID);
        }

        void OnLogin(string sessionID, BMLoginRequest msg)
        {
            GetData(sessionID).SetData(msg.ListenerAddress, msg.ListenerAddress);
            BMLoginReply reply = new BMLoginReply();
            reply.Name = GetData(sessionID).name;
            SendMessage(sessionID, reply);
        }
    }
}
