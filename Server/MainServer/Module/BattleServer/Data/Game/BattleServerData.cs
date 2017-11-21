using System.Collections;

namespace RedStone.Data
{
    public class BattleServerData : DataBase
    {
        public long uid { get; private set; }
        public string name { get; private set; }
        public string sessionID { get; private set; }
        public UserState state { get; private set; }

        public void SetSessionID(string sessionID)
        {
            this.sessionID = sessionID;
        }

        public void SetState(UserState state)
        {
            this.state = state;
        }

        public void SetData(string sessionID,DB.UserDB db)
        {
            this.uid = db.uid;
            this.name = db.name;
            this.sessionID = sessionID;
        }

        public enum State
        {
            Open,
            Maintenance,
        }
    }
}