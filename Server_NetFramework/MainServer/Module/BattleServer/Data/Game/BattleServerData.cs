using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RedStone.Data
{
    public class BattleServerData : DataBase
    {
        public string name { get; private set; }
        public string sessionID { get; private set; }
        public string address { get; private set; }
        public State state { get; private set; }
        public List<RoomData> rooms { get; private set; }

        public BattleServerData()
        {
            rooms = new List<RoomData>();
        }

        public void SetSessionID(string sessionID)
        {
            this.sessionID = sessionID;
        }

        public void SetState(State state)
        {
            this.state = state;
        }

        public void SetData(string name, string address)
        {
            this.name = name;
            this.address = address;
        }

        public void AddRoom(RoomData data)
        {
            rooms.Add(data);
        }

        public void RemoveRoom(int roomID)
        {
            rooms.RemoveAll(a => a.id == roomID);
        }

        public RoomData GetRoom(int roomID)
        {
            return rooms.First(a => a.id == roomID);
        }









        public enum State
        {
            Close = 0,
            Open = 1,
            Maintenance = 2,
        }
    }
}