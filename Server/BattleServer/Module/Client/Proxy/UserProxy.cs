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
        private Dictionary<long, UserData> m_users = new Dictionary<long, UserData>();


        public override void OnInit()
        {
            base.OnInit();
            RegisterMessage<CBLoginRequest>(OnLogin);
        }

        public UserData AddUser(Message.PlayerInfo playerInfo, int roomID)
        {
            UserData user = null;
            if (m_users.ContainsKey(playerInfo.Uid))
            {
                user = GetUser(playerInfo.Uid);
                Debug.LogError($"{playerInfo.Uid} is already in Battle");
            }
            else
            {
                user = new UserData();
                string token = Guid.NewGuid().ToString(); //Gen Token
                user.SetData(playerInfo, roomID, token);
                user.SetState(UserState.Offline);
                m_users.Add(user.uid, user);
            }
            return user;
        }


        void OnLogin(string sessionID, CBLoginRequest msg)
        {
            var user = m_users.Values.First(a => a.token == msg.Token);
            if (user == null)
            {
                Debug.LogError($"{sessionID}'s token {msg.Token} is wrong, refuse login.");
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


        public UserData GetUser(long uid)
        {
            return m_users[uid];
        }

        public UserData GetUser(string sessionID)
        {
            return m_users.Values.First(a => a.sessionID == sessionID);
        }

        public void RemoveUser(long uid)
        {
            m_users.Remove(uid);
        }


    }
}
