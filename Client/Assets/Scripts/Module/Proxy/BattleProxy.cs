using UnityEngine;
using System.Collections;
using Message;
using Plugins;

namespace RedStone
{
    public class BattleProxy : BattleProxyBase
    {
        public BattleServerInfo serverInfo { get { return GetProxy<HallProxy>().battleServerInfo; } }
        private string m_serverState = "";

        public BattleProxy()
        {
        }

        public override void OnInit()
        {
            network.socket.onConnected = () =>
            {
                Login();
            };
        }

        public void Connect()
        {
            if (serverInfo != null)
            {
                string ip = NetTool.GetIP(serverInfo.Address);
                int port = NetTool.GetPort(serverInfo.Address);
                network.socket.Setup(ip, port);
                network.socket.Connect();
                network.socket.onConnected = () =>
                {
                    Login(); // 连接成功后，LOGIN
                };
            }
            else
            {
                Debug.LogError("BattleServerInfo is NULL");
            }
        }

        public void Login()
        {
            CBLoginRequest req = new CBLoginRequest();
            req.SessionID = serverInfo.SessionID;
            network.Send<CBLoginRequest, CBLoginReply>(req
            , (rep) =>
            {
                m_serverState = rep.State;
            });
        }

        public void JoinBattle()
        {

        }
    }
}