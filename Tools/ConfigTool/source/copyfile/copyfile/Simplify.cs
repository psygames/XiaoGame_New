using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using Mono.Xml;
using Tools;

namespace copyfile
{
    /****************************************/
    /*  SecurityElement.Attribute("xxx")    */
    /*  会把&amp;等转义，慎用！！！           */
    /*  建议使用以下方式：                    */
    /*  SecurityElement.Attributes["xxx"]   */
    /****************************************/
    class Simplify
    {
        protected string outDir = "";
        protected bool isSucceed;

        public virtual bool  SimplifyStart(string srcDir, string outDir, string listPath = null)
        {
            isSucceed = true;
            this.outDir = outDir;

            if (!Directory.Exists(outDir))
                Directory.CreateDirectory(outDir);
            DirectoryInfo di = new DirectoryInfo(srcDir);

            string[] xmls = CopyFile.GetPropertiesList(listPath);

            foreach (FileInfo fi in di.GetFiles("*.xml"))
            {
                if (listPath == null
                    || xmls.Contains(Path.GetFileNameWithoutExtension(fi.FullName)))
                    SimplifyFile(fi.FullName);
            }

            return isSucceed;
        }

        protected string AddHeader(string content, string header = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n\n")
        {
            return header + content;
        }

        public void SimplifyFile(string filePath)
        {
            string xmlName = Path.GetFileName(filePath);
            Console.WriteLine(xmlName);
            string xmlText = FileOpt.ReadText(filePath);
            string content = SimplifyXmlText(xmlText);
            content = AddHeader(content);
            FileOpt.WriteText(outDir + xmlName, content);
        }

        string[] indentDepth = { "", "\t", "\t\t", "\t\t\t", "\t\t\t\t", "\t\t\t\t\t"
                                         , "\t\t\t\t\t\t", "\t\t\t\t\t\t\t", "\t\t\t\t\t\t\t\t"
                                         , "\t\t\t\t\t\t\t\t\t", "\t\t\t\t\t\t\t\t\t\t" };

        protected string FormatXml(SecurityElement root, int depth = 0)
        {
            string indent = indentDepth[depth];
            StringBuilder sb = new StringBuilder(); ;
            sb.Append(indent).Append("<").Append(root.Tag);
            if (root.Attributes != null)
                foreach (DictionaryEntry kv in root.Attributes)
                    sb.Append(" ").Append(kv.Key).Append("=\"").Append(kv.Value).Append("\"");
            if (string.IsNullOrEmpty(root.Text) && root.Children == null)
                sb.Append("/>\n");
            else
            {
                sb.Append(">\n");
                foreach (SecurityElement child in root.Children)
                {
                    sb.Append(FormatXml(child, depth + 1));
                }
                sb.Append(indent).Append("</").Append(root.Tag).Append(">\n");
            }
            return sb.ToString();
        }

        SecurityElement MergeData(SecurityElement root)
        {
            SecurityElement out_root = new SecurityElement("table");
            SecurityElement out_type = new SecurityElement("type");
            SecurityElement out_data = new SecurityElement("data");

            foreach (SecurityElement data in root.Children)
            {
                if (data.Children == null || data.Children.Count <= 0)
                    continue;

                if (data.Tag == out_type.Tag)
                {
                    if (out_type.Children == null || out_type.Children.Count <= 0)
                        out_type.AddChild(data.Children[0] as SecurityElement);
                }
                else if (data.Tag == out_data.Tag)
                {
                    foreach (SecurityElement item in data.Children)
                        out_data.AddChild(item);
                }
            }
            out_root.AddChild(out_type);
            out_root.AddChild(out_data);
            return out_root;
        }

        protected SecurityParser m_parser = new SecurityParser();
        const string TAG_WORKSHEET = "Worksheet";
        const string TAG_TABLE = "Table";
        const string TAG_ROW = "Row";
        const string TAG_CELL = "Cell";
        const string KEY_SHEET_NAME = "ss:Name";

        const int ROW_INDEX_ID = 0;
        const int ROW_INDEX_TYPE = 1;
        const int ROW_INDEX_DESCRIPTION = 2;
        const int ROW_INDEX_DATA_BEGIN = 3;
        const int MAX_SHEET_CHECK_COUNT = int.MaxValue;
        protected string SimplifyXmlText(string xmlText)
        {
            m_parser.LoadXml(xmlText);
            SecurityElement el_0 = m_parser.ToXml();
            int sheetIndex = 0;
            SecurityElement out_root = new SecurityElement("table");
            foreach (SecurityElement el_1 in el_0.Children)
            {
                if (sheetIndex >= MAX_SHEET_CHECK_COUNT)
                    break;
                if (el_1.Tag != TAG_WORKSHEET)
                    continue;
                Console.WriteLine("├─" + el_1.Attributes[KEY_SHEET_NAME]);
                foreach (SecurityElement el_2 in el_1.Children)
                {
                    if (el_2.Tag != TAG_TABLE || el_2.Children == null || el_2.Children.Count <= 0)
                        continue;
                    int rowIndex = 0;
                    int cellIndex = 0;
                    SecurityElement out_type = new SecurityElement("type");
                    SecurityElement out_data = new SecurityElement("data");
                    List<string> idList = new List<string>();

                    foreach (SecurityElement row in el_2.Children)
                    {
                        if (row.Tag != TAG_ROW || IsEmptyRow(row))
                            continue;
                        cellIndex = 0;
                        SecurityElement out_item = new SecurityElement("item");
                        foreach (SecurityElement cell in row.Children)
                        {
                            if (cell.Tag != TAG_CELL)
                                continue;

                            var idxStr = cell.Attribute("ss:Index");
                            if (!string.IsNullOrEmpty(idxStr))
                            {
                                var index = int.Parse(idxStr);
                                while(cellIndex < index - 1)
                                {
                                    out_item.AddAttribute(idList[cellIndex], "");
                                    ++cellIndex;
                                }
                            }
                            string data = GetCellData(cell);
                            if (rowIndex == ROW_INDEX_ID)
                            {
                                idList.Add(data);
                            }
                            else if (rowIndex == ROW_INDEX_TYPE)
                            {
                                out_item.AddAttribute(idList[cellIndex], data);
                            }
                            else if (rowIndex == ROW_INDEX_DESCRIPTION)
                            {

                            }
                            else if (rowIndex >= ROW_INDEX_DATA_BEGIN)
                            {
                                try
                                {
                                    out_item.AddAttribute(idList[cellIndex], data);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                }
                            }
                            cellIndex++;
                        }

                        if (rowIndex == ROW_INDEX_TYPE)
                            out_type.AddChild(out_item);
                        else if (rowIndex >= ROW_INDEX_DATA_BEGIN)
                            out_data.AddChild(out_item);
                        rowIndex++;
                    }
                    out_root.AddChild(out_type);
                    out_root.AddChild(out_data);
                }
                sheetIndex++;
            }
            out_root = MergeData(out_root);
            string result = FormatXml(out_root);
            return result;
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
            return sb.ToString();
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
    }
}
