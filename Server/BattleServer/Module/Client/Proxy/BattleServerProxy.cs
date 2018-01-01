using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedStone.Data;


namespace RedStone
{
    public class BattleServerProxy : ProxyBaseServer
    {

        public override void OnInit()
        {
            base.OnInit();

        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            UpdatePvL();
            CheckRoomDismiss();
        }

        Random rand = new Random();
        public RoomData MainServerRequsetCreateRoom(IList<Message.PlayerInfo> playerInfos)
        {
            var room = GetProxy<RoomProxy>().CreateRoom();
            var users = new List<UserData>();

            foreach (var info in playerInfos)
            {
                var user = GetProxy<UserProxy>().AddUser(info, room.id, false);
                if (user == null)
                    continue;
                users.Add(user);
            }

            while (users.Count < 4)
            {
                Message.PlayerInfo info = new Message.PlayerInfo();
                info.Exp = 0;
                info.Level = 1;
                info.Name = TableManager.instance.GetData<TableName>(rand.Next(1, 100)).name.Trim();
                info.Uid = -1;
                info.Gold = 0;
                var user = GetProxy<UserProxy>().AddUser(info, room.id, true);
                if (user == null)
                    continue;
                users.Add(user);
            }

            room.SetUsers(users);
            NewPvL(room.id);
            Debug.LogInfo("房间创建【{0}】", room.id);
            return room;
        }

        public void RegisterUserMsg<T>(string token, Action<T> action)
        {
            RegisterMessage<T>((sessionID, msg) =>
            {
                var user = GetProxy<UserProxy>().GetUserBySession(sessionID);
                if (user == null)
                    Debug.LogError($"User Msg Handle Failed : {user.name}");
                else if (token == user.token)
                    action(msg);
            });
        }

        //TODO: Register Room Dismiss Event
        private void OnRoomDismiss(int roomID)
        {
            Debug.LogInfo("房间销毁 【{0}】", roomID);
            m_pvlLogics.Remove(roomID);
            GetProxy<RoomProxy>().RemoveRoom(roomID);
        }


        #region PvL
        private Dictionary<int, SOS.SOS_Logic> m_pvlLogics = new Dictionary<int, SOS.SOS_Logic>();
        public void NewPvL(int id)
        {
            SOS.SOS_Logic logic = new SOS.SOS_Logic();
            logic.Init(id);
            m_pvlLogics.Add(id, logic);
        }

        public void RemovePvL(int id)
        {
            m_pvlLogics.Remove(id);
        }

        private void UpdatePvL()
        {
            lock (m_pvlLogics) //Update 中 foreah 的 List 需要 Lock
            {
                foreach (var pvl in m_pvlLogics)
                {
                    pvl.Value.Update();
                }
            }
        }

        private void CheckRoomDismiss()
        {
            List<int> needRemoveIDs = new List<int>();
            foreach (var kv in m_pvlLogics)
            {
                if (kv.Value.CheckCanDismiss())
                {
                    needRemoveIDs.Add(kv.Key);
                }
            }

            foreach (var key in needRemoveIDs)
            {
                OnRoomDismiss(key);
            }
        }
        #endregion
    }
}
