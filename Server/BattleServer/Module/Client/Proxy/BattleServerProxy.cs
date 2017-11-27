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


        public RoomData MainServerRequsetCreateRoom(IList<Message.PlayerInfo> playerInfos)
        {

            var room = GetProxy<RoomProxy>().CreateRoom();
            var uids = new List<long>();

            foreach (var info in playerInfos)
            {
                var user = GetProxy<UserProxy>().AddUser(info,room.id);
                if (user == null)
                    continue;
                uids.Add(user.uid);
            }

            room.SetUsers(uids);
            NewPvL(room.id);
            return room;
        }

        public void RegisterUserMsg<T>(long uid, Action<T> action)
        {
            RegisterMessage<T>((sessionID, msg) =>
            {
                var user = GetProxy<UserProxy>().GetUser(sessionID);
                if (user == null)
                    Debug.LogError($"User Msg Handle Failed : {uid}");
                else if (uid == user.uid)
                    action(msg);
            });
        }

        //TODO: Register Room Dismiss Event
        private void OnRoomDismiss(int roomID)
        {
            m_pvlLogics.Remove(roomID);
            GetProxy<RoomProxy>().RemoveRoom(roomID);
        }


        #region PvL
        private Dictionary<int, PvL_Logic> m_pvlLogics = new Dictionary<int, PvL_Logic>();
        public void NewPvL(int id)
        {
            PvL_Logic logic = new PvL_Logic();
            logic.Init(id);
            m_pvlLogics.Add(id, logic);
        }

        public void RemovePvL(int id)
        {
            m_pvlLogics.Remove(id);
        }

        private void UpdatePvL()
        {
            foreach (var pvl in m_pvlLogics)
            {
                pvl.Value.Update();
            }
        }
        #endregion
    }
}
