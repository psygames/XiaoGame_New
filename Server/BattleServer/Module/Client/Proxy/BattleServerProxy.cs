using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedStone.Data;


namespace RedStone
{
    public class BattleServerProxy : MBProxyBase
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
                var user = GetProxy<UserProxy>().AddUser(info);
                if (user == null)
                    continue;
                uids.Add(user.uid);
                user.SetRoomID(room.id);
            }

            room.SetUsers(uids);

            return room;
        }
    }
}
