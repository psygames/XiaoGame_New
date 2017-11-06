using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xmlparser
{
    public class CellData
    {
        public CellData(int rowIndex, int columnIndex, string value
            , string name = null, string type = null, string desc = null)
        {
            this.rowIndex = rowIndex;
            this.columnIndex = columnIndex;
            this.value = value;
            this.name = name;
            this.type = type;
            this.desc = desc;
        }

        public void SetHead(string name, string type, string desc)
        {
            this.name = name;
            this.type = type;
            this.desc = desc;
        }

        public string value;
        public int rowIndex;
        public int columnIndex;
        public string name;
        public string type;
        public string desc;

        public int intValue { get { return int.Parse(value); } }
        public float floatValue { get { return float.Parse(value); } }
        public bool boolValue { get { return bool.Parse(value); } }
    }
}
