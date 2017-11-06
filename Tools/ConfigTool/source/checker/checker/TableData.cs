using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xmlparser
{
    public class TableData
    {
        public string name;
        public List<SheetData> sheets = new List<SheetData>();
        public TableData(string name, List<SheetData> sheets)
        {
            this.name = name;
            this.sheets = sheets;
        }
    }
}
