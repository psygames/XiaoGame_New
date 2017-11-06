using System;
using System.Collections.Generic;
using System.Text;

namespace xmlparser
{
    class MyClassMember
    {
        public MyClassMember() { }
        public MyClassMember(string name, string type, string description)
        {
            Name = name;
            Type = type;
            Description = description;
        }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }
}
