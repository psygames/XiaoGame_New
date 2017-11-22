using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedStone.Data;


namespace RedStone
{
    public class UserProxy : MBProxyBase
    {
        private Dictionary<long, UserData> m_users = new Dictionary<long, UserData>();


        public override void OnInit()
        {
            base.OnInit();

        }

        public UserData AddUser(Message.PlayerInfo playerInfo)
        {
            if (m_users.ContainsKey(playerInfo.Uid))
            {
                Debug.LogError($"{playerInfo.Uid} is already in Battle");
            }
            else
            {
                UserData user = new UserData();
                user.SetData(playerInfo);
                m_users.Add(user.uid, user);
                return user;
            }
            return null;
        }

        public UserData GetUser(int uid)
        {
            return m_users[uid];
        }

        public void RemoveUser(int uid)
        {
            m_users.Remove(uid);
        }
    }
}
