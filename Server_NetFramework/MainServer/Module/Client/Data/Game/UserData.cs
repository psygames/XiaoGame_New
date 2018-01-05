using System.Collections;

namespace RedStone.Data
{
    public class UserData : DataBase
    {
        public long uid { get; private set; }
        public string name { get; private set; }
        public string sessionID { get; private set; }
        public int level { get; private set; }
        public int exp { get; private set; }
        public int gold { get; private set; }
        public UserState state { get; private set; }
        public bool isOnline { get; private set; }

        public void SetSessionID(string sessionID)
        {
            this.sessionID = sessionID;
        }

        public void SetState(UserState state)
        {
            this.state = state;
        }

        public void Logout()
        {
            isOnline = false;
        }

        public void Login()
        {
            isOnline = true;
        }

        public void SetData(string sessionID,DB.UserDB db)
        {
            this.uid = db.uid;
            this.name = db.name;
            this.sessionID = sessionID;
            this.level = db.level;
            this.exp = db.exp;
            this.gold = db.gold;
        }
    }

    public enum UserState
    {
        None = 0,
        Hall = 1,
        Matching = 2,
        Game = 3,
        GameEnd = 4,
    }
}