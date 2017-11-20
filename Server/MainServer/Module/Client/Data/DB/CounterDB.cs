using System;
using System.Collections.Generic;
using System.Text;

namespace RedStone.DB
{
    public class CounterDB : CommonDB
    {
        public string dbName { get; set; }
        public long counter { get; set; }
    }
}
