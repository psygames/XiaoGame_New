using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedStone.Data;
using Core;


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
                info.Name = GetRandomName();
                info.Uid = -1;
                info.Gold = 0;
                var user = GetProxy<UserProxy>().AddUser(info, room.id, true);
                if (user == null)
                    continue;
                users.Add(user);
            }

            room.SetUsers(users);
            NewPvL(room.id);
            Logger.LogInfo("房间创建【{0}】", room.id);
            return room;
        }

        private string GetRandomName()
        {
            var names = TableManager.instance.GetAllData<TableName>().Values.ToList();
            int randIndex = rand.Next(0, names.Count);
            string name = names[randIndex].name.Trim();
            if (name.Length > 8)
                name = name.Substring(0, 8);
            return name;
        }

        public void RegisterUserTokenMsg<T>(string token, Action<T> action)
        {
            RegisterMessage<T>((sessionID, msg) =>
            {
                var user = GetProxy<UserProxy>().GetUserBySession(sessionID);
                if (user == null)
                    Logger.LogError($"User Msg Handle Failed : {user.name}");
                else if (token == user.token)
                    action(msg);
            });
        }

        //TODO: Register Room Dismiss Event
        private void OnRoomDismiss(int roomID)
        {
            Logger.LogInfo("房间销毁 【{0}】", roomID);
            m_pvlLogics.Remove(roomID);
            GetProxy<RoomProxy>().RemoveRoom(roomID);
        }


        #region PvL
        public SOS.SOS_Logic GetSosLogic(int roomID)
        {
            SOS.SOS_Logic logic = null;
            m_pvlLogics.TryGetValue(roomID, out logic);
            return logic;
        }

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
