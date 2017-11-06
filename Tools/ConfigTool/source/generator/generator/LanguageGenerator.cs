using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using xmlparser;

namespace generator
{
    class LanguageGenerator
    {
        XmlParser xParser = new XmlParser(1000);

        HashSet<string> keyDict = new HashSet<string>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public string GetSummary(RowData row)
        {
            return row.cells[5].value;
        }

        public string GetLTKey(RowData row)
        {
            var data = row.cells;
            int nameType = -1;
            for (int i = 0; i < data.Count; ++i)
                if (data[i].name == "nameType")
                    nameType = data[i].intValue;
            if (nameType == -1)
                return null;
            if (nameType == 0 && keyDict.Contains(data[0].value))
                return null;
            var str = data[0].value;
            if (nameType > 0)
            {
                var list = str.Split('_');
                str = list[0];
                for (int i = 1; i < list.Length; ++i)
                {
                    if (i < nameType)
                    {
                        str += "_" + list[i];
                    }
                }
            }
            if (keyDict.Contains(str))
                return null;
            keyDict.Add(str);
            return str;
        }
        public bool GeneCs(string xmlPath, string outPutDir)
        {
            try
            {
                Console.WriteLine("Generate Language");
                TableData td = xParser.GetTableData(Path.GetFileNameWithoutExtension(xmlPath), FileOpt.ReadTextFromFile(xmlPath));
                string content = GetSharpHeader();
                List<string> except = new List<string>();
                foreach (SheetData sd in td.sheets)
                {
                    foreach (RowData rd in sd.dataRows)
                    {
                        var summary = GetSummary(rd);
                        var ltk = GetLTKey(rd);
                        if (!string.IsNullOrEmpty(ltk))
                            content += GetSharpRowStr(ltk, summary);
                    }
                    except.Add(sd.name);
                }
                content += "\t}\n}\n";
                FileOpt.WriteText(content, outPutDir + "LTKey.cs");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public string GetSharpHeader()
        {
            string str = "using System;\n\n";
            str += "namespace Hotfire\n{\n";
            str += "\tpublic static class LTKey \n\t{\n";
            return str;
        }

        public string GetSharpRowStr(string key, string summary)
        {
            return string.Format("\t\t/// <summary>\n\t\t/// {1}\r\n\t\t/// </summary>\n\t\tpublic const string {0} = \"{0}\";\n", key, summary);
        }
    }
}
