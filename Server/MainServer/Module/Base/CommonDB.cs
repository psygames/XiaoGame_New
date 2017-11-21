using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace RedStone.DB
{
    public abstract class CommonDB
    {
        public ObjectId _id { get; set; }
    }
}
