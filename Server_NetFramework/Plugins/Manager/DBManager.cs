using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Plugins
{
    public static class DBX
    {
        public static IMongoDatabase db{ get { return DBManager.instance.db; }}
        public static IMongoCollection<T> GetCol<T>(string collectionName = null)
        {
            return DBManager.instance.GetCollection<T>(collectionName);
        }
    }

    public class DBManager : Core.Singleton<DBManager>
    {
        private MongoClient m_client = null;
        private IMongoDatabase m_db = null;

        public void Init(string url, string dbName)
        {
            m_client = new MongoClient(url);
            m_db = m_client.GetDatabase(dbName);
        }

        public IMongoDatabase db
        {
            get { return m_db; }
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName = null)
        {
            return m_db.GetCollection<T>(collectionName ?? typeof(T).Name);
        }
    }
}

