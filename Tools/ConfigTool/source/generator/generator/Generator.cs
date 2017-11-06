using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text;
using Mono.Xml;
using xmlparser;

namespace generator
{
    class Generator
    {
        Dictionary<string, string> typeToGetMethod4JavaMap;
        XmlParser xParser = new XmlParser(1000);


        public Generator()
        {
            typeToGetMethod4JavaMap = new Dictionary<string, string>();
            typeToGetMethod4JavaMap.Add("float", "getFloat");
            typeToGetMethod4JavaMap.Add("int", "getInt");
            typeToGetMethod4JavaMap.Add("String", "getString");
            typeToGetMethod4JavaMap.Add("boolean", "getBoolean");
            typeToGetMethod4JavaMap.Add("Vector2", "getVector2");
            typeToGetMethod4JavaMap.Add("Vector3", "getVector3");
            typeToGetMethod4JavaMap.Add("Date", "getDate");
            typeToGetMethod4JavaMap.Add("String[]", "getStringArray");
            typeToGetMethod4JavaMap.Add("int[]", "getIntArray");
            typeToGetMethod4JavaMap.Add("float[]", "getFloatArray");
            typeToGetMethod4JavaMap.Add("Vector2[]", "getVector2Array");
            typeToGetMethod4JavaMap.Add("Vector3[]", "getVector3Array");
            typeToGetMethod4JavaMap.Add("IntFloat[]", "getIntFloatArray");
            typeToGetMethod4JavaMap.Add("Date[]", "getDateArray");
        }

        public bool GeneCs(String xmlDir, String xmlListFile, String csOutPath)
        {
            string[] lists = GetPropertiesList(xmlListFile, "ForCSharp");

            //ClearFolder(csOutPath);

            foreach (string csName in lists)
            {
                string text = FileOpt.ReadTextFromFile(xmlDir + csName + ".xml");
                Dictionary<string, List<MyClassMember>> dictionary = xParser.GeneMembersByXmlText(csName, text);

                foreach (var d in dictionary)
                {
                    List<MyClassMember> members = d.Value;
                    string content = GeneCsFile(csName, members);
                    if (FileOpt.WriteText(content, csOutPath + csName + ".cs"))
                        Console.WriteLine("生成CS数据表成功：" + csName);
                }
            }
            return true;
        }

        public bool GeneJava(String xmlDir, String xmlListFile, String package, String javaOutPath)
        {
            try
            {
                string[] lists = GetPropertiesList(xmlListFile, "ForJava" + package);

                //ClearFolder(javaOutPath);
                Console.WriteLine("-----> " + javaOutPath);

                foreach (string csName in lists)
                {
                    string text = FileOpt.ReadTextFromFile(xmlDir + csName + ".xml");
                    Dictionary<string, List<MyClassMember>> dictionary = xParser.GeneMembersByXmlText(csName, text);

                    foreach (var d in dictionary)
                    {
                        List<MyClassMember> members = d.Value;
                        string content = GeneJavaFile(csName, members);
                        if (FileOpt.WriteText(content, javaOutPath + csName + ".java"))
                            Console.WriteLine("生成Java文件成功：" + "ForJava-" + package + "   " + csName);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }


        /// <summary>
        /// 根据PropertiesList文件，获取配置文件名的List
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        private string[] GetPropertiesList(string filePath, string fileType)
        {
            string text = FileOpt.ReadTextFromFile(filePath);
            text = text.Replace("\r", "");
            string[] lists = text.Split('\n');
            int index = 0;
            while (!lists[index].Contains(fileType))
                index++;

            List<string> result = new List<string>();

            for (int i = index + 1; i < lists.Length; i++)
            {
                if (String.IsNullOrEmpty(lists[i]))
                    continue;
                if (lists[i].Contains("["))
                {
                    break;
                }
                result.Add(lists[i]);
            }
            return result.ToArray();
        }

        /// <summary>
        /// 通过成员变量参数，生成.cs文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="members"></param>
        private string GeneCsFile(string className, List<MyClassMember> members)
        {
            string returnSpace = "\r\n";

            string headString = "using UnityEngine;" + returnSpace + "using System;" + returnSpace + "using System.Collections;" + returnSpace
                + returnSpace + "namespace Hotfire" + returnSpace + "{" + returnSpace + "\tpublic class " + className
                + returnSpace + "\t{" + returnSpace + "";
            string tailString = "\t}" + returnSpace + "}";

            string constructMethodHeadString = "\t\tpublic " + className + "() { }" + returnSpace;
            constructMethodHeadString += "\t\tpublic " + className + "(IDictionary dict)" + returnSpace + "\t\t{" + returnSpace;
            string constructMethodTailString = "\t\t}" + returnSpace + returnSpace;

            string content = "";
            string constructMethodContent = "";
            string methodDescription = "";

            foreach (MyClassMember member in members)
            {
                string stype = member.Type;
                if (stype.Contains("Date"))
                    stype = stype.Replace("Date", "DateTime");
                if (stype.StartsWith("array"))
                    stype = stype.Substring(6) + "[]";

                methodDescription = member.Description.Replace("\r", "");
                string[] descrp = methodDescription.Split('\n');
                methodDescription = descrp[0];
                for (int i = 1; i < descrp.Length; i++)
                {
                    methodDescription += "\r\n\t\t/// " + descrp[i];
                }
                content += "\t\t/// <summary>" + returnSpace + "\t\t/// " + methodDescription + returnSpace + "\t\t/// </summary>" + returnSpace;
                content += "\t\tpublic " + stype + " " + member.Name + ";" + returnSpace;
                constructMethodContent += "\t\t\tthis." + member.Name + " = (" + stype + ")dict[\"" + member.Name + "\"];" + returnSpace;
            }

            constructMethodContent = constructMethodHeadString + constructMethodContent + constructMethodTailString;
            content = headString + constructMethodContent + content + tailString;

            return content;
        }

        /// <summary>
        /// 通过成员变量参数，生成.Java文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="members"></param>
        private string GeneJavaFile(string className, List<MyClassMember> members)
        {
            string returnSpace = "\r\n";

            string packageString = "package com.coolfish.hotfire.configure;" + returnSpace;
            string importString = "";
            importString += "import java.util.HashMap;" + returnSpace;
            importString += "import com.coolfish.hotfire.commons.utils.TableUtils;" + returnSpace;

            string headString = "public class " + className + returnSpace + "{" + returnSpace + "";

            string tailString = "}" + returnSpace;

            string constructMethodHeadString = "\tpublic " + className + "(HashMap<String, Object> map)";
            string constructMethodTailString = "\t}" + returnSpace + returnSpace;

            string content = "";
            string getterContent = "";
            string constructMethodContent = "";
            string methodDescription = "";

            foreach (MyClassMember member in members)
            {
                string stype = member.Type;
                if (stype.Contains("bool"))
                    stype = stype.Replace("bool", "boolean");
                else if (stype.Contains("string")) // C#里String是小写的
                    stype = stype.Replace("string", "String");
                else if (stype.Contains("Vector2") && !importString.Contains("Vector2"))
                    importString += "import com.coolfish.hotfire.commons.math.Vector2;" + returnSpace;
                else if (stype.Contains("Vector3") && !importString.Contains("Vector3"))
                    importString += "import com.coolfish.hotfire.commons.math.Vector3;" + returnSpace;
                else if (stype.Contains("IntFloat") && !importString.Contains("IntFloat"))
                    importString += "import com.coolfish.hotfire.commons.math.IntFloat;" + returnSpace;
                else if (stype.Contains("Date") && !importString.Contains("Date"))
                    importString += "import java.util.Date;" + returnSpace;

                methodDescription = member.Description.Replace("\r", "");
                string[] descrp = methodDescription.Split('\n');
                methodDescription = descrp[0];
                for (int i = 1; i < descrp.Length; i++)
                {
                    methodDescription += "\n\t * " + descrp[i];
                }
                content += "\tprivate " + stype + " " + member.Name + ";" + returnSpace;
                getterContent += "\t/**" + returnSpace + "\t * " + methodDescription + returnSpace + "\t */" + returnSpace;
                getterContent += "\tpublic " + stype + " get" + ("" + member.Name[0]).ToUpper() + member.Name.Substring(1) + "()" + returnSpace;
                getterContent += "\t{" + returnSpace + "\t\treturn this." + member.Name + ";" + returnSpace;
                getterContent += "\t}" + returnSpace;
                //constructMethodHeadString += stype + " " + member.Name + ", ";
                constructMethodContent += "\t\tthis." + member.Name + " = TableUtils.getInstance()." + typeToGetMethod4JavaMap[stype] + "(map, \"" + member.Name + "\");" + returnSpace;
            }
            //constructMethodHeadString = constructMethodHeadString.Substring(0, constructMethodHeadString.Length - 2);
            constructMethodHeadString += returnSpace + "\t{" + returnSpace;

            constructMethodContent = constructMethodHeadString + constructMethodContent + constructMethodTailString;
            getterContent = returnSpace + getterContent;

            if (importString != "")
                importString = returnSpace + importString + returnSpace;

            content = packageString + importString + headString + constructMethodContent + content + getterContent + tailString;
            return content;
        }

    }
}
