using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedStone.Data
{
    public class RoomData
    {
        public int id { get; private set; }
        public string name { get; private set; }

        public void SetData(Message.BattleRoomInfo info)
        {
            id = info.Id;
            name = info.Name;
        }
    }
}
