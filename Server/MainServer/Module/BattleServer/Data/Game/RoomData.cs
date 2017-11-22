using System.Collections;
using System.Collections.Generic;

namespace RedStone.Data
{
    public class RoomData : DataBase
    {
        public string name { get; private set; }
        public int id { get; private set; }
        public string token { get; private set; }
        public List<long> users { get; private set; }

        public void SetData(int id, string token, string name)
        {
            this.id = id;
            this.token = token;
            this.name = name;
        }

        public void SetUsers(List<long> users)
        {
            this.users = users;
        }
    }
}