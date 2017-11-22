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

        public void SetSessionID(string sessionID)
        {
            this.sessionID = sessionID;
        }

        public void SetState(UserState state)
        {
            this.state = state;
        }

        public void SetData(Message.PlayerInfo info)
        {
            this.uid = info.Uid;
            this.name = info.Name;
            this.level = info.Level;
            this.exp = info.Exp;
            this.gold = info.Gold;
        }
    }

    public enum UserState
    {
        Game,
        Offline,
    }
}