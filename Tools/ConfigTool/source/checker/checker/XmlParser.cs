using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using Mono.Xml;

namespace xmlparser
{
    class XmlParser
    {
        private SecurityParser m_parser = new SecurityParser();
        const string TAG_WORKSHEET = "Worksheet";
        const string TAG_TABLE = "Table";
        const string TAG_ROW = "Row";
        const string TAG_CELL = "Cell";
        const string KEY_SHEET_NAME = "ss:Name";
        const string KEY_CELL_INDEX = "ss:Index";

        const int ROW_INDEX_ID = 0;
        const int ROW_INDEX_TYPE = 1;
        const int ROW_INDEX_DESCRIPTION = 2;
        const int ROW_INDEX_DATA_BEGIN = 3;

        private int m_maxSheetCheckCount = 100;
        private bool m_enableLog = false;

        public XmlParser(int maxSheetCheckCount, bool enableLog = false)
        {
            this.m_maxSheetCheckCount = maxSheetCheckCount;
            this.m_enableLog = enableLog;
        }

        public TableData GetTableData(string tabName, string xmlText)
        {
            LogLine(tabName);
            List<SheetData> sheets = new List<SheetData>();
            m_parser.LoadXml(xmlText);
            SecurityElement el_0 = m_parser.ToXml();
            foreach (SecurityElement el_1 in el_0.Children)
            {
                if (sheets.Count >= m_maxSheetCheckCount)
                    break;
                if (el_1.Tag != TAG_WORKSHEET)
                    continue;

                string sheetname = el_1.Attributes[KEY_SHEET_NAME].ToString();
                Log("├─" + sheetname);
                foreach (SecurityElement el_2 in el_1.Children)
                {
                    if (el_2.Tag != TAG_TABLE || el_2.Children == null || el_2.Children.Count <= 0)
                        continue;
                    List<RowData> rows = new List<RowData>();
                    foreach (SecurityElement rowEl in el_2.Children)
                    {
                        if (rowEl.Tag != TAG_ROW || rowEl.Children == null
                            || rowEl.Children.Count <= 0 || IsEmptyRow(rowEl))
                            continue;
                        List<CellData> cells = new List<CellData>();
                        foreach (SecurityElement cellEl in rowEl.Children)
                        {
                            if (cellEl.Tag != TAG_CELL)
                                continue;
                            var idxStr = cellEl.Attribute(KEY_CELL_INDEX);
                            if (!string.IsNullOrEmpty(idxStr))
                            {
                                var index = int.Parse(idxStr);
                                while (cells.Count < index - 1)
                                {
                                    cells.Add(new CellData(rows.Count, cells.Count, ""));
                                }
                            }

                            cells.Add(new CellData(rows.Count, cells.Count, GetCellData(cellEl)));
                        }

                        // 补齐行尾
                        while (rows.Count > 0 && cells.Count < rows[0].cells.Count)
                        {
                            cells.Add(new CellData(rows.Count, cells.Count, ""));
                        }

                        RowData row = new RowData(cells);
                        rows.Add(row);
                    }

                    // Set Head
                    for (int i = 3; i < rows.Count; i++)
                    {
                        for (int j = 0; j < rows[i].cells.Count; j++)
                        {
                            var cell = rows[i].cells[j];
                            string[] heads = new string[3];
                            for (int k = 0; k < 3; k++)
                            {
                                if (j < rows[k].cells.Count)
                                    heads[k] = rows[k].cells[j].value;
                            }
                            // name, type, desc
                            cell.SetHead(heads[0], heads[1], heads[2]);
                        }
                    }

                    if (rows.Count > 0)
                    {
                        sheets.Add(new SheetData(sheetname, rows));
                    }
                    else
                    {
                        Log("(empty)");
                    }
                    LogLine();
                }
            }
            LogLine();
            return new TableData(tabName, sheets);
        }

        private void Log(string text)
        {
            if (m_enableLog)
                Console.Write(text);
        }

        private void LogLine(string text = "")
        {
            if (m_enableLog)
                Console.WriteLine(text);
        }

        public bool IsEmptyRow(SecurityElement row)
        {
            if (row.Children == null)
                return true;
            foreach (SecurityElement cell in row.Children)
            {
                if (cell.Tag != TAG_CELL)
                    continue;
                if (!string.IsNullOrEmpty(GetCellData(cell)))
                    return false;
            }
            return true;
        }


        public string GetCellData(SecurityElement cell)
        {
            if (!cell.Tag.Contains("Data") && cell.Children != null)
                foreach (SecurityElement ce in cell.Children)
                    return GetCellData(ce);

            string cellText = cell.ToString();
            cellText = cellText.Replace("\r\n", "");
            StringBuilder sb = new StringBuilder();
            bool isQuot = false;
            for (int i = 0; i < cellText.Length; i++)
            {
                char c = cellText[i];
                if (isQuot)
                {
                    if (c == '>')
                        isQuot = false;
                }
                else
                {
                    if (c == '<')
                        isQuot = true;
                    else
                        sb.Append(c);
                }
            }
            return ReplaceReference(sb.ToString());
        }

        private string ReplaceReference(string value)
        {
            value = value.Replace("&lt;", "<");
            value = value.Replace("&gt;", ">");
            value = value.Replace("&apos;", "\'");
            value = value.Replace("&quot;", "\"");
            value = value.Replace("&amp;", "&");
            return value;
        }
    }
}
