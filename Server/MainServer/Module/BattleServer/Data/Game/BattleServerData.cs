using System.Collections;

namespace RedStone.Data
{
    public class BattleServerData : DataBase
    {
        public string name { get; private set; }
        public string sessionID { get; private set; }
        public string address { get; private set; }
        public State state { get; private set; }

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

        public enum State
        {
            Open,
            Maintenance,
        }
    }
}