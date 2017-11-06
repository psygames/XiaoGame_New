using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using xmlparser;

namespace generator
{
    class ConstGenerator
    {
        XmlParser xParser = new XmlParser(1000);
        public bool GeneJava(string[] serverNames, string[] outputDirs, string xmlPath, params string[] typeExcept)
        {
            for (int i = 0; i < serverNames.Length; i++)
            {
                if (!GeneJava(serverNames[i], outputDirs[i], xmlPath, typeExcept))
                    return false;
            }
            return true;
        }

        public bool GeneJava(string serverName, string outPutDir, string xmlPath, params string[] typeExcept)
        {
            if (string.IsNullOrEmpty(outPutDir))
            {
                Console.WriteLine("JAVA 空路径不生成！");
                return true;
            }

            try
            {
                FileOpt.ClearFolder(outPutDir);
                TableData td = xParser.GetTableData(Path.GetFileNameWithoutExtension(xmlPath), FileOpt.ReadTextFromFile(xmlPath));
                string configmngr = serverName + "Config";
                string pkgPath = GetPackagePath(outPutDir);

                foreach (SheetData sd in td.sheets)
                {
                    if (sd.name.Contains("(") && !sd.name.ToLower().Contains("(server"))
                    {
                        continue;
                    }

                    int i = sd.name.IndexOf('(');
                    if (i > 0)
                        sd.name = sd.name.Substring(0, i);

                    string content = GetHeader(configmngr, pkgPath, td.name, sd.name);
                    foreach (RowData rd in sd.dataRows)
                    {
                        if (typeExcept.Contains(rd.cells[2].value))
                            continue;
                        content += GetJavaRowStr(td.name, sd.name, configmngr
                            , rd.cells[0].intValue, rd.cells[1].value
                            , rd.cells[2].value, rd.cells[3].value
                            , rd.cells[4].value);
                    }
                    content += "}\n";
                    FileOpt.WriteText(content, outPutDir + sd.name + ".java");
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public string GetHeader(string configmngr, string pkgPath, string tabname, string sheetname)
        {
            string str = "package " + pkgPath + ";\n\n";
            str += "import com.coolfish.hotfire.configure.TableConst;\n";
            str += "import com.coolfish.hotfire.commons.utils.CommonUtil;\n\n";
            str += "import com.coolfish.hotfire.commons.math.Vector3;\n\n";
            str += "public class " + sheetname + "{\n";
            str += "\tprivate static " + sheetname + " instance = new " + sheetname + "();\n\n";
            str += "\tprivate " + sheetname + "(){ }\n\n";
            str += "\tpublic static " + sheetname + " getInstance(){return instance;}\n\n";
            return str;
        }


        public string GetJavaRowStr(string tablename, string sheetname
            , string configmngr, int id, string key, string type
            , string value, string description)
        {
            string str = "";
            str += "\n\t/**\n";
            str += "\t * " + description + " (" + value + ")\n\t */\n";
            str += "\t" + "public " + SharpToJavaType(type) + " get";
            str += UP0(key) + "(){\n";
            str += "\t\t" + "return " + GetTypeParseStart(type);
            str += "((" + tablename + ")" + configmngr;
            str += ".getInstance().getConstTable(" + id + ")).getValue()";
            str += GetTypeParseEnd(type) + ";\n";
            str += "\t}\n";
            return str;
        }

        private string GetPackagePath(string outDir)
        {
            return outDir.Substring(outDir.IndexOf("\\com\\") + 1).Trim('\\').Replace('\\', '.');
        }

        private string SharpToJavaType(string type)
        {
            if (type.Contains("bool"))
                type = type.Replace("bool", "boolean");
            else if (type.Contains("string")) // C#里String是小写的
                type = type.Replace("string", "String");
            return type;
        }

        private string GetTypeParseStart(string type)
        {
            switch (type)
            {
                case "bool":
                    return "Boolean.parseBoolean(";
                case "int":
                    return "Integer.parseInt(";
                case "float":
                    return "Float.parseFloat(";
                case "Vector2":
                    return "CommonUtil.Vector2Parse(";
                case "Vector3":
                    return "CommonUtil.Vector3Parse(";
                case "int[]":
                    return "CommonUtil.IntArrayParse(";
                case "float[]":
                    return "CommonUtil.FloatArrayParse(";
                case "string[]":
                    return "CommonUtil.StringArrayParse(";
                case "Vector2[]":
                    return "CommonUtil.Vector2ArrayParse(";
                case "Vector3[]":
                    return "CommonUtil.Vector3ArrayParse(";
                default:
                    return "";
            }
        }

        private string GetTypeParseEnd(string type)
        {
            if (type != "string")
                return ")";
            return "";
        }

        private string UP0(string key)
        {
            if (!string.IsNullOrEmpty(key))
                return key[0].ToString().ToUpper() + key.Substring(1);
            return key;
        }



        public bool GeneCs(string xmlPath, string outPutDir)
        {
            try
            {
                TableData td = xParser.GetTableData(Path.GetFileNameWithoutExtension(xmlPath), FileOpt.ReadTextFromFile(xmlPath));

                List<string> except = new List<string>();
                except.Add("LTKey.cs");
                foreach (SheetData sd in td.sheets)
                {
                    if (sd.name.Contains("(") && !sd.name.ToLower().Contains("(client"))
                    {
                        continue;
                    }

                    int i = sd.name.IndexOf('(');
                    if (i > 0)
                        sd.name = sd.name.Substring(0, i);

                    string content = GetSharpHeader(sd.name);
                    foreach (RowData rd in sd.dataRows)
                    {
                        content += GetSharpRowStr(td.name, rd.cells[0].intValue
                            , rd.cells[1].value, rd.cells[2].value
                            , rd.cells[3].value, rd.cells[4].value);
                    }
                    content += "    }\n}\n";
                    except.Add(sd.name);
                    FileOpt.WriteText(content, outPutDir + sd.name + ".cs");
                }
                FileOpt.ClearFolder(outPutDir, "*.cs", except.ToArray());
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public string GetSharpHeader(string sheetname)
        {
            string str = "using System;\nusing UnityEngine;\n\n";
            str += "namespace Hotfire\n{\n";
            str += "    public class " + sheetname + "\n    {\n";
            return str;
        }

        public string GetSharpRowStr(string tablename, int id, string key, string type, string value, string description)
        {
            string sKey = "s_" + key;
            string bKey = "b_" + key;
            string str = "";
            str += "\t\tprivate static " + type + " " + sKey + ";\r\n";
            str += "\t\tprivate static bool " + bKey + ";\r\n";
            str += "\t\t/// <summary>\r\n\t\t/// " + description;
            str += " (" + value + ")\r\n";
            str += "\t\t/// </summary>\r\n";
            str += "\t\t" + "public static " + type + " " + key + "\r\n";
            str += "\t\t{\r\n\t\t\tget\r\n\t\t\t{\r\n";
            str += "\t\t\t\tif (!" + bKey + ")\r\n\t\t\t\t{\r\n";
            str += "\t\t\t\t\t" + bKey + " = true;\r\n";
            str += "\t\t\t\t\t" + sKey + " = " + "(" + type + ")TableManager.ParseValue(\"" + type + "\", ";
            str += "(TableManager.instance.GetData<" + tablename + ">(" + id + ").value));\r\n";
            str += "\t\t\t\t}\r\n";
            str += "\t\t\t\treturn " + sKey + ";\r\n";
            str += "\t\t\t}\r\n";
            str += "\t\t}\r\n\r\n";
            str = str.Replace("\t", "    ");
            return str;
        }
    }
}
