using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedStone.Data;


namespace RedStone
{
    public class RoomProxy : ProxyBaseServer
    {
        private Dictionary<int, RoomData> m_rooms = new Dictionary<int, RoomData>();


        public override void OnInit()
        {
            base.OnInit();
        }

        int m_lastRoomID = 1;   // TODO:自增，是否要存数据库？
        public RoomData CreateRoom()
        {
            int roomID = m_lastRoomID++;
            string token = Guid.NewGuid().ToString();
            RoomData data = new RoomData();
            data.SetData(roomID, token, "RED STONE");

            m_rooms.Add(roomID, data);
            return data;
        }

        public void RemoveRoom(int roomID)
        {
            m_rooms.Remove(roomID);
        }

        public RoomData GetRoom(int roomID)
        {
            return m_rooms[roomID];
        }

    }
}
