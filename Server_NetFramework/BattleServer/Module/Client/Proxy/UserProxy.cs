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
            network.server.onClosed = OnClosed;
            RegisterMessage<CBLoginRequest>(OnLogin);
        }

        private void OnClosed(string sessionID)
        {
            GetUserBySession(sessionID).SetState(UserState.Offline);
        }

        public UserData AddUser(Message.PlayerInfo playerInfo, int roomID, bool isAI)
        {
            UserData user = null;
            user = new UserData();
            string token = Guid.NewGuid().ToString(); //Gen Token
            user.SetData(playerInfo, roomID, token, isAI);
            user.SetState(isAI ? UserState.Login : UserState.Offline);
            m_users.Add(token, user);
            return user;
        }


        void OnLogin(string sessionID, CBLoginRequest msg)
        {
            var user = m_users.Values.First(a => a.token == msg.Token);
            if (user == null)
            {
                Logger.LogError($"{sessionID}'s token {msg.Token} is wrong, refuse login.");
            }
            else
            {
                user.SetSessionID(sessionID);
                user.SetState(UserState.Login);
            }

            CBLoginReply rep = new CBLoginReply();
            rep.RoomID = user.roomID;
            SendMessage(sessionID, rep);
        }


        public UserData GetUser(string token)
        {
            return m_users[token];
        }

        public UserData GetUserBySession(string sessionID)
        {
            return m_users.Values.First(a => a.sessionID == sessionID);
        }

        public void RemoveUser(string token)
        {
            m_users.Remove(token);
        }


    }
}
