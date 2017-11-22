using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Message;
using Plugins;
using MongoDB.Bson;
using MongoDB.Driver;
using RedStone.DB;


namespace RedStone
{
    public class UserDaoProxy : ProxyBase
    {
        public override void OnInit()
        {
            base.OnInit();
            usrDao = DBX.GetCol<UserDB>();
            counterDao = DBX.GetCol<CounterDB>();
        }

        private IMongoCollection<UserDB> usrDao = null;
        private IMongoCollection<CounterDB> counterDao = null;

        public UserDB GetUserDB(long uid)
        {
            var user = usrDao.AsQueryable().FirstOrDefault(a => a.uid == uid);
            return user;
        }

        public UserDB Login(string deviceID)
        {
            var user = usrDao.AsQueryable().FirstOrDefault(a => a.deviceID == deviceID);
            if (user == null)
            {
                user = new UserDB();
                user.uid = GetCounter("UserDB");
                user.deviceID = deviceID; ;
                user.name = "Player" + user.uid;
                user.level = 1;
                user.exp = 0;
                user.gold = 0;
                user.isOnline = true;
                usrDao.InsertOne(user);
            }
            else if (!user.isOnline)
            {
                var query = Builders<UserDB>.Filter.Where(a => a.deviceID == deviceID);
                var update = Builders<UserDB>.Update.Set(a => a.isOnline, true);
                user = usrDao.FindOneAndUpdate(query, update);
            }
            return user;
        }

        public void Logout(long uid)
        {
            var query = Builders<UserDB>.Filter.Where(a => a.uid == uid);
            var update = Builders<UserDB>.Update.Set(a => a.isOnline, false);
            usrDao.UpdateOne(query, update);
        }


        private long GetCounter(string dbName)
        {
            long count = 10001;
            var counter = counterDao.AsQueryable().FirstOrDefault(a => a.dbName == dbName);
            if (counter == null)
            {
                counter = new CounterDB();
                counter.dbName = dbName;
                counter.counter = count;
                counterDao.InsertOne(counter);
            }
            else
            {
                count = counter.counter;
            }
            return count;
        }

    }


}
