using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedStone.Data;
using Message;


namespace RedStone
{
    public class UserProxy : ProxyBaseServer
    {
        private Dictionary<string, UserData> m_users = new Dictionary<string, UserData>();


        public override void OnInit()
        {
            base.OnInit();
            network.onConnected = OnConnected;
            network.onClosed = OnClosed;
            network.heartbeat.onTimeout = OnTimeout;
            RegisterMessage<CBLoginRequest>(OnLogin);
            RegisterMessage<CBReconnectRequest>(OnReconnect);
        }

        private void OnTimeout(string sessionID)
        {
            Logger.LogError($"{sessionID} heartbeat timeout!!!");
        }

        private void OnConnected(string sessionID)
        {
            Logger.Log($"{sessionID} socket connected.");
        }

        private void OnClosed(string sessionID)
        {
            Logger.Log($"{sessionID} socket closed.");
            var user = GetUserBySession(sessionID);
            if (user != null)
                user.SetState(UserState.Offline);
        }

        public UserData AddUser(Message.PlayerInfo playerInfo, int roomID, bool isAI)
        {
            UserData user = new UserData();
            string token = Guid.NewGuid().ToString(); //Gen Token
            user.SetData(playerInfo, roomID, token, isAI);
            user.SetState(isAI ? UserState.Login : UserState.Offline);
            m_users.Add(token, user);
            return user;
        }


        void OnLogin(string sessionID, CBLoginRequest msg)
        {
            UserData user = null;
            if (!m_users.TryGetValue(msg.Token, out user))
            {
                Logger.LogError($"LOGIN => {sessionID}'s token {msg.Token} is wrong, refuse login.");
                return;
            }

            user.SetSessionID(sessionID);
            user.SetState(UserState.Login);

            CBLoginReply rep = new CBLoginReply();
            rep.RoomID = user.roomID;
            SendMessage(sessionID, rep);
        }

        void OnReconnect(string sessionID, CBReconnectRequest msg)
        {
            Logger.Log($"{sessionID} Reconnect with token: {msg.Token} ");

            UserData user = null;
            if (!m_users.TryGetValue(msg.Token, out user))
            {
                Logger.LogError($"RECONNECT => {sessionID}'s token {msg.Token} is wrong, refuse reconnect.");
                return;
            }

            if (GetProxy<RoomProxy>().GetRoom(user.roomID) == null)
            {
                Logger.LogError($"RECONNECT => {sessionID}'s token {msg.Token} is wrong, refuse reconnect.");
                CBReconnectReply rep = new CBReconnectReply();
                rep.RoomState = (int)SOS.SOS_Logic.State.Dismiss;
                SendMessage(sessionID, rep);
                return;
            }

            SOS.SOS_Logic sosLogic = GetProxy<BattleServerProxy>().GetSosLogic(user.roomID);

            user.SetSessionID(sessionID);
            if (sosLogic.state == SOS.SOS_Logic.State.Started)
                user.SetState(UserState.Battle);
            else
                user.SetState(UserState.Login);

            CBReconnectReply reply = sosLogic.GetReconnectData(user.uid);
            SendMessage(sessionID, reply);
        }


        public UserData GetUser(string token)
        {
            return m_users[token];
        }

        public UserData GetUserBySession(string sessionID)
        {
            return m_users.Values.FirstOrDefault(a => a.sessionID == sessionID);
        }

        public void RemoveUser(string token)
        {
            m_users.Remove(token);
        }


    }
}
