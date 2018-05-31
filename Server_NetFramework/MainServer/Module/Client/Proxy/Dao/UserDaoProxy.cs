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
                CreatUser(deviceID);

            user.isOnline = true;
            Update(user.uid, user);
            return user;
        }

        private void CreatUser(string deviceID)
        {
            var user = new UserDB();
            user.uid = GetCounter("UserDB");
            user.deviceID = deviceID; ;
            user.name = "Player" + user.uid;
            user.level = 1;
            user.exp = 0;
            user.gold = 0;
            user.isOnline = false;
            usrDao.InsertOne(user);
        }

        public void CalReuslt(long uid, int incrGold, int incrExp)
        {
            var u = GetUserDB(uid);
            u.gold += incrGold;
            u.exp += incrExp;
            int levelUpExp = GetLevelUpExp(u.level);
            while (u.exp >= levelUpExp)
            {
                u.level++;
                u.exp -= levelUpExp;
                levelUpExp = GetLevelUpExp(u.level);
            }
            while (u.exp < 0)
            {
                if (u.level <= 1)
                {
                    u.exp = 0;
                    break;
                }

                u.level--;
                levelUpExp = GetLevelUpExp(u.level);
                u.exp += levelUpExp;
            }
            Update(uid, u);
        }

        private int GetLevelUpExp(int level)
        {
            return (int)(Math.Pow(1.5f, level) * 100);
        }

        public void Logout(long uid)
        {
            var u = GetUserDB(uid);
            u.isOnline = true;
            Update(uid, u);
        }

        private UserDB Update(long uid, UserDB user)
        {
            var query = Builders<UserDB>.Filter.Where(a => a.uid == uid);
            var update = Builders<UserDB>.Update
                .Set(a => a.name, user.name)
                .Set(a => a.gold, user.gold)
                .Set(a => a.isOnline, user.isOnline)
                .Set(a => a.level, user.level)
                .Set(a=>a.exp ,user.exp);
            return usrDao.FindOneAndUpdate(query, update);
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
                count = counter.counter + 1;
                var query = Builders<CounterDB>.Filter.Where(a => a.dbName == dbName);
                var update = Builders<CounterDB>.Update.Set(a => a.counter, count);
                counterDao.UpdateOne(query, update);
            }
            return count;
        }

    }


}
