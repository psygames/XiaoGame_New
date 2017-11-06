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
    class SimplifyLanguage : Simplify
    {
        public const string BASE_KV_TITLE = "en";
        public const string TAG_ID = "id";
        public const string TAG_TEXT = "text";
        public const string TAG_ITEM = "item";
        public const string TAG_TYPE = "type";
        public const string TAG_DATA = "data";
        public const string TAG_DESCRIPTION = "description";
        public const string TAG_NAME_TYPE = "nameType";
        public const string TAG_VERSION = "version";
        public const string TAG_TABLE = "table";
        public const string UPDATE_SUFFIX = "_update";
        public List<string> ParamTags = new List<string> { "s:", "i:" };

        public override bool SimplifyStart(string srcDir, string outDir, string listPath = null)
        {
            this.outDir = outDir;
            isSucceed = true;
            base.SimplifyStart(srcDir, outDir);
            SimplifyOnlyLangs(srcDir + "languages.xml");
            return isSucceed;
        }

        private void SimplifyOnlyLangs(string filePath)
        {
            FileOpt.ClearFolder(outDir, "languages.xml");
            string xmlName = Path.GetFileName(filePath);
            Console.WriteLine(xmlName);
            string xmlText = FileOpt.ReadText(filePath);
            string content = SimplifyXmlText(xmlText);
            content = AddHeader(content);

            Dictionary<string, string> spContent = SplitLang(content);
            foreach (var data in spContent)
            {
                string fPath = outDir + FormatXmlName(data.Key);
                FileOpt.WriteText(fPath, data.Value);
            }
        }

        private string FormatXmlName(string key)
        {
            return "string_" + key + ".xml";
        }

        /// <summary>
        /// 需要先 Load Xml
        /// </summary>
        /// <param name="xmlContent"></param>
        /// <returns></returns>
        private Dictionary<string, string> GetBaseKV()
        {
            Dictionary<string, string> base_kv = new Dictionary<string, string>();
            SecurityElement e_root = m_parser.ToXml();
            SecurityElement e_data = e_root.SearchForChildByTag(TAG_DATA);
            foreach (SecurityElement e_data_item in e_data.Children)
            {
                string key = e_data_item.Attributes[TAG_ID].ToString();
                string value = e_data_item.Attributes[BASE_KV_TITLE].ToString();
                base_kv.Add(key, value);
            }
            return base_kv;
        }

        private Dictionary<string, bool> GetUsedStandaloneAttr()
        {
            Dictionary<string, bool> is_use_standalone_attr = new Dictionary<string, bool>();
            is_use_standalone_attr.Add(TAG_ID, true);
            is_use_standalone_attr.Add(TAG_DESCRIPTION, true);
            is_use_standalone_attr.Add(TAG_NAME_TYPE, true);
            is_use_standalone_attr.Add(TAG_VERSION, false);
            return is_use_standalone_attr;
        }

        private void GetHolders(string text, Action<string> action)
        {
            char[] ts = text.ToCharArray();
            List<string> holders = new List<string>();
            bool isHolder = false;
            StringBuilder holder = new StringBuilder();
            for (int i = 0; i < ts.Length; i++)
            {
                if (isHolder)
                {
                    if (ts[i] == '}')
                    {
                        isHolder = false;
                        bool istag = false;
                        for (var j = 0; j < ParamTags.Count; ++j)
                        {
                            if (holder.ToString().StartsWith(ParamTags[j]))
                            {
                                istag = true;
                                break;
                            }
                        }
                        if (!istag)
                        {
                            action(holder.ToString());
                        }
                        holder.Length = 0;
                    }
                    else
                    {
                        holder.Append(ts[i]);
                    }
                }
                else
                {
                    if (ts[i] == '{')
                    {
                        isHolder = true;
                    }
                }
            }
        }
        private List<string> GetHolders(string text)
        {
            List<string> holders = new List<string>();
            GetHolders(text, str => 
            {
                if(!str.Contains(":"))
                    holders.Add(str);
            });
            return holders;
        }

        HashSet<int> indexSet = new HashSet<int>();
        List<string> noTagStrList = new List<string>();
        Dictionary<string, int> holdersDict = new Dictionary<string, int>();
        private Dictionary<string, int> GetBaseHolders(string text)
        {
            indexSet.Clear();
            holdersDict.Clear();
            noTagStrList.Clear();
            GetHolders(text, str =>
            {
                var tagIndex = str.IndexOf(":");
                if (tagIndex < 0)
                    noTagStrList.Add(str);
                else
                {
                    var body = str.Substring(tagIndex + 1);
                    var indexStr = str.Substring(0, tagIndex);
                    int index = -1;
                    if (int.TryParse(indexStr, out index))
                    {
                        if (!holdersDict.ContainsKey(body))
                        {
                            if (!indexSet.Contains(index))
                            {
                                holdersDict.Add(body, index);
                                indexSet.Add(index);
                            }
                            else
                            {
                                noTagStrList.Add(body);
                            }
                        }
                    }
                    else
                    {
                        noTagStrList.Add(body);
                    }
                }
            });
            int curIndex = 0;
            for(int i = 0; i < noTagStrList.Count; ++i)
            {
                while (indexSet.Contains(curIndex))
                    ++curIndex;
                holdersDict.Add(noTagStrList[i], curIndex);
                indexSet.Add(curIndex);
            }
            return holdersDict;
        }
        private string ReplaceTextHolder(string text, params object[] holder)
        {
            char[] ts = text.ToCharArray();
            StringBuilder sb = new StringBuilder();
            int holderCount = 0;
            bool isHolder = ts[0] == '{';
            for (int i = 0; i < ts.Length; i++)
            {
                if (holderCount < holder.Length)
                {
                    if (isHolder)
                    {
                        if (ts[i] == '}')
                        {
                            isHolder = false;
                            sb.Append(holder[holderCount++]);
                        }
                    }
                    else
                    {
                        if (ts[i] == '{')
                        {
                            isHolder = true;
                        }
                        if (!isHolder)
                            sb.Append(ts[i]);
                    }
                }
                else
                {
                    sb.Append(ts[i]);
                }
            }
            return sb.ToString();
        }

        private string AddOrderNum4PlaceHolder(string base_value, string cur_value)
        {
            Dictionary<string,int> base_holders = GetBaseHolders(base_value);
            List<string> cur_holders = GetHolders(cur_value);

            for (int j = 0; j < cur_holders.Count; j++)
           {
                int index = -1;
                if(base_holders.TryGetValue(cur_holders[j], out index))
               {
                  cur_holders[j] = "{" + index + ":" + cur_holders[j] + "}";
               }
           }
            string result = ReplaceTextHolder(cur_value, cur_holders.ToArray());
            return result;
        }

        private bool IsUsedStandaloneAttr(Dictionary<string, bool> usedStaAttrs, string attr)
        {
            bool used = false;
            usedStaAttrs.TryGetValue(attr, out used);
            return used;
        }

        private bool IsExistStandaloneAttr(Dictionary<string, bool> usedStaAttrs, string attr)
        {
            return usedStaAttrs.ContainsKey(attr);
        }

        private Dictionary<string, string> SplitLang(string content)
        {
            m_parser.LoadXml(content);

            Dictionary<string, bool> used_standalone_attrs = GetUsedStandaloneAttr();
            Dictionary<string, string> base_kv = GetBaseKV();

            /*--------------- Result ----------------*/
            Dictionary<string, string> langs = new Dictionary<string, string>();
            Dictionary<string, SecurityElement> out_items = new Dictionary<string, SecurityElement>();
            SecurityElement e_root = m_parser.ToXml();
            SecurityElement e_type = e_root.SearchForChildByTag(TAG_TYPE);
            SecurityElement e_data = e_root.SearchForChildByTag(TAG_DATA);

            /*--------------- Type Line ---------------*/
            SecurityElement e_type_item = e_type.SearchForChildByTag(TAG_ITEM);
            var type_item_itr = e_type_item.Attributes.GetEnumerator();
            while (type_item_itr.MoveNext())
            {
                string attr = type_item_itr.Key.ToString();
                if (IsExistStandaloneAttr(used_standalone_attrs, attr))
                    continue;
                SecurityElement table = new SecurityElement(TAG_TABLE);
                SecurityElement type = new SecurityElement(TAG_TYPE);
                SecurityElement item = new SecurityElement(TAG_ITEM);
                SecurityElement data = new SecurityElement(TAG_DATA);
                foreach (KeyValuePair<string, bool> except_attr in used_standalone_attrs)
                {
                    if (except_attr.Value)
                    {
                        string k = except_attr.Key;
                        string v = e_type_item.Attributes[k].ToString();
                        item.AddAttribute(k, v);
                    }
                }
                item.AddAttribute(TAG_TEXT, e_type_item.Attributes[attr].ToString());
                type.AddChild(item);
                table.AddChild(type);
                table.AddChild(data);
                out_items.Add(attr, table);
                out_items.Add(attr + UPDATE_SUFFIX, SecurityElement.FromString(table.ToString()));

            }

            /*------------- Data Set ---------------*/
            int curIndex = 0;
            int languageVersion = 0;
            foreach (SecurityElement e_data_item in e_data.Children)
            {
                ++curIndex;

                var curVersion = 0;
                int.TryParse(e_data_item.Attributes[TAG_VERSION].ToString(), out curVersion);
                if (curIndex == 1)
                {
                    languageVersion = curVersion;
                    Console.WriteLine("Language Version : {0}", languageVersion);
                    continue;
                }
                var data_item_itr = e_data_item.Attributes.GetEnumerator();
                while (data_item_itr.MoveNext())
                {
                    string attr = data_item_itr.Key.ToString();
                    if (IsExistStandaloneAttr(used_standalone_attrs, attr))
                        continue;
                    SecurityElement table = (curVersion <= languageVersion ? out_items[attr] : out_items[attr + UPDATE_SUFFIX]);
                    SecurityElement data = table.SearchForChildByTag(TAG_DATA);
                    SecurityElement item = new SecurityElement(TAG_ITEM);
                    foreach (KeyValuePair<string, bool> except_attr in used_standalone_attrs)
                    {
                        if (except_attr.Value)
                        {
                            string k = except_attr.Key;
                            string v = e_data_item.Attributes[k].ToString();
                            item.AddAttribute(k, v);
                        }
                    }
                    string key = e_data_item.Attributes[TAG_ID].ToString();
                    string text = e_data_item.Attributes[attr].ToString();
                    text = AddOrderNum4PlaceHolder(base_kv[key], text);
                    item.AddAttribute(TAG_TEXT, text);
                    data.AddChild(item);
                }
            }

            /*---------------- To Langs ----------------*/
            foreach (var d in out_items)
            {
                string text = FormatXml(d.Value);
                text = AddHeader(text);
                langs.Add(d.Key, text);
            }
            return langs;
        }
    }
}
