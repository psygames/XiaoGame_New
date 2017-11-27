using System;
using MongoDB.Bson;
using Message;

namespace Plugins
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            TestProtoSerializer();
        }


        static void TestProtoSerializer()
        {
            ProtoSerializer serializer = new ProtoSerializer();
            serializer.LoadProtoNum(typeof(Message.ProtoNumTest));
            LoginRequest msg = new LoginRequest();
            msg.SessionID = "hello";
            byte[] data = serializer.Serialize(msg);

            var deMsg = serializer.Deserialize(data);
        }

        static void TestDB()
        {
            DBManager.CreateInstance();
            DBManager.instance.Init("mongodb://localhost:27017", "xiao_game");

            var userCol = DBX.GetCol<User>();
            //添加一个待审核的商品
            User user = new User() { name = "黄河", deviceID = "RED_STONE_666" };
            userCol.InsertOne(user);
            Console.WriteLine($"添加商品：{user.ToJson()}成功。");
            var query = new BsonDocument();
            var usr = userCol.FindSync<User>(query).Current;
        }
    }

    class User
    {
        public ObjectId _id { get; set; }
        public string deviceID { get; set; }
        public string name { get; set; }
    }
}
