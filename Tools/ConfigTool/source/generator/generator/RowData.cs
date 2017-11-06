using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xmlparser
{
    public class RowData
    {
        public RowData(List<CellData> cellList)
        {
            this.cells = cellList;
        }
        public List<CellData> cells;
    }
}
