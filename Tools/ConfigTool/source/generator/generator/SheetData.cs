using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xmlparser
{
    public class SheetData
    {
        public SheetData(string sheetname, List<RowData> rawData)
        {
            this.name = sheetname;
            this.rows = rawData;
        }

        public string name;
        public List<RowData> rows;
        public List<RowData> dataRows { get { return rows.FindAll(a => rows.IndexOf(a) > 2); } }
    }
}
