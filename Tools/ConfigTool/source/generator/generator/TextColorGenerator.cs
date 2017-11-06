using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using xmlparser;

namespace generator
{
    class TextColorGenerator
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
            return row.cells[1].value;
        }

        public int GetID(RowData row)
        {
            return row.cells[0].intValue;
        }
        public string GetName(RowData row)
        {
            return row.cells[2].value;
        }

        public string GetValue(RowData row)
        {
            return row.cells[3].value;
        }
        struct ColorConfig
        {
            public int id;
            public string desc;
            public string name;
            public string value;
        }
        string GenGetColor(List<ColorConfig> config)
        {
            string content = "";
            foreach(var c in config)
            {
                content += string.Format("\t\t/// <summary>\n\t\t/// {0}\r\n", c.desc);
                content += string.Format("\t\tpublic static Color {0} {{ get {{ return TableManager.instance.GetData<TableTextColor>({1}).value; }} }}\r\n", c.name, c.id);

            }
            return content + "\n\n";
        }

        string GenColorCode(List<ColorConfig> config)
        {
            string content = "";

            content += "\t\tpublic static string GetColorCode(string name) \n\t\t{\r\n";
            content += "\t\t\tswitch(name){\r\n";
            foreach (var c in config)
            {
                content += string.Format("\t\t\t\t case \"{0}\" : return \"#{1}\";\r\n", c.name, c.value);
            }
            return content + "\t\t\t}\r\n\t\t\t return \"#ffffff\";\r\n\t\t}\r\n";
        }
        public bool GeneCs(string xmlPath, string outPutDir)
        {
            try
            {
                Console.WriteLine("Generate Text Color Config");
                TableData td = xParser.GetTableData(Path.GetFileNameWithoutExtension(xmlPath), FileOpt.ReadTextFromFile(xmlPath));
                string content = GetSharpHeader();
                List<ColorConfig> configList = new List<ColorConfig>();
                foreach (SheetData sd in td.sheets)
                {
                    foreach (RowData rd in sd.dataRows)
                    {
                        var config = new ColorConfig();
                         config.desc = GetSummary(rd);
                        config.id = GetID(rd);
                        config.name = GetName(rd);
                        config.value = GetValue(rd);
                        configList.Add(config);
                    }
                }
                content += GenGetColor(configList);
                content += GenColorCode(configList);
                content += "\t}\n}\n";
                FileOpt.WriteText(content, outPutDir + "ColorConfig.cs");
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
            string str = "using System;\n";
            str += "using UnityEngine;\n\n";
            str += "namespace Hotfire\n{\n";
            str += "\tpublic static class ColorConfig \n\t{\n";
            return str;
        }

        public string GetSharpRowStr(string key, string summary)
        {
            return string.Format("\t\t/// <summary>\n\t\t/// {1}\r\n\t\t/// </summary>\n\t\tpublic const string {0} = \"{0}\";\n", key, summary);
        }
    }
}
