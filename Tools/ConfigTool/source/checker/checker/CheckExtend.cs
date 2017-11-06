using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xmlparser;

namespace checker
{
    public static class TableExtend
    {
        public static CheckResult Check(this TableData table)
        {
            Console.WriteLine(table.name);

            CheckResult result = new CheckResult(true);

            //raw_name, raw_type
            List<CellData> rawNames = table.sheets[0].rows[0].cells;
            List<CellData> rawTypes = table.sheets[0].rows[1].cells;

            //check repeated
            foreach (var rawName in rawNames)
            {
                var group = rawNames.FindAll(a => a.value == rawName.value);
                if (group.Count >= 2)
                {
                    result.Set(false, string.Format("Sheet({0}) 存在重复的属性名(第一行是属性名)" +
                       ", 属性名{1}, {2}行,{3}({4})列 与 {5}行,{6}({7})列 "
                       , table.sheets[0].name, rawName.value
                       , group[0].rowIndex + 1, group[0].columnIndex + 1, CellExtend.IntToNumberSystem26(group[0].columnIndex + 1)
                       , group[1].rowIndex + 1, group[1].columnIndex + 1, CellExtend.IntToNumberSystem26(group[1].columnIndex + 1)));
                    return result;
                }
            }

            for (int i = 1; i < table.sheets.Count; i++)
            {
                for (int j = 0; j < rawNames.Count; j++)
                {
                    var rawNameCell = rawNames[j];
                    var nameCell = table.sheets[i].rows[0].cells[j];
                    if (rawNameCell.value != nameCell.value)
                    {
                        result.Set(false, string.Format("Sheet({0}) 存在不一致的属性名(第一行是属性名)" +
                            ", 属性名{1}, 原始属性名{2}, {3}行,{4}({5})列 "
                            , table.sheets[i].name, nameCell.value, rawNameCell.value
                            , nameCell.rowIndex + 1, nameCell.columnIndex + 1, CellExtend.IntToNumberSystem26(nameCell.columnIndex + 1)));
                        return result;
                    }


                    var rawTypeCell = rawTypes[j];
                    var typeCell = table.sheets[i].rows[1].cells[j];
                    if (rawTypeCell.value != typeCell.value)
                    {
                        result.Set(false, string.Format("Sheet({0}) 存在不一致的类型(第二行是类型)" +
                            ", 类型{1}, 原始类型{2}, {3}行,{4}({5})列 "
                            , table.sheets[i].name, typeCell.value, rawTypeCell.value
                            , typeCell.rowIndex + 1, typeCell.columnIndex + 1, CellExtend.IntToNumberSystem26(typeCell.columnIndex + 1)));
                        return result;
                    }
                }
            }

            HashSet<string> ids = new HashSet<string>();
            foreach (var sheet in table.sheets)
            {
                int ignoreRow = 3;
                foreach (var row in sheet.rows)
                {
                    if (row.cells.Count < 0 || ignoreRow-- > 0)
                        continue;
                    string id = row.cells[0].value;
                    if (ids.Contains(id))
                    {
                        result.Set(false,
                            string.Format("Sheet({0}) 存在重复的ID：{1}",
                            sheet.name, id));
                        return result;
                    }
                    else
                    {
                        ids.Add(id);
                    }
                }
            }


            if (!result.isSucceed)
                return result;

            foreach (var sheet in table.sheets)
            {
                result = sheet.Check();
                if (!result.isSucceed)
                    break;
            }
            Console.WriteLine();
            return result;
        }
    }

    public static class SheetExtend
    {
        public static CheckResult Check(this SheetData sheet)
        {
            Console.WriteLine("├─" + sheet.name);

            CheckResult result = new CheckResult(true);

            int cellCount = sheet.rows[0].cells.Count;
            for (int i = 0; i < sheet.rows.Count; i++)
            {
                var row = sheet.rows[i];

                if (row.cells.Count != cellCount)
                {
                    result.Set(false, string.Format("第{0}行长度({1})不正确，正确长度{2}"
                        , i + 1, row.cells.Count, cellCount));
                    return result;
                }
            }

            if (!result.isSucceed)
                return result;

            foreach (var row in sheet.rows)
            {
                result = row.Check();
                if (!result.isSucceed)
                    break;
            }
            return result;
        }
    }

    public static class RowExtend
    {

        public static CheckResult Check(this RowData row)
        {
            CheckResult result = new CheckResult(true);

            //TODO: CHECK ROW

            if (!result.isSucceed)
                return result;

            foreach (var cell in row.cells)
            {
                result = cell.Check();
                if (!result.isSucceed)
                    break;
            }
            return result;
        }

    }

    public static class CellExtend
    {
        public static CheckResult Check(this CellData cell)
        {
            CheckResult result = new CheckResult(true);

            if (cell.rowIndex < 2)// id type
            {
                if (string.IsNullOrEmpty(cell.value))
                {
                    result.Set(false, string.Format(" 数据不能为空！==> {0}行,{1}({2})列 "
                        , cell.rowIndex + 1, cell.columnIndex + 1, IntToNumberSystem26(cell.columnIndex + 1)));
                }
            }
            else if (cell.rowIndex >= 3)// data
            {
                if (string.IsNullOrEmpty(cell.value)
                    && cell.type != "string" // can be none type
                    )
                {
                    result.Set(false, string.Format(" 数据不能为空！==> {0}行,{1}({2})列 "
                        , cell.rowIndex + 1, cell.columnIndex + 1, IntToNumberSystem26(cell.columnIndex + 1)));
                }
                else if (!CheckDataType(cell.value, cell.type))
                {
                    result.Set(false, string.Format(" 数据类型不匹配！==> {0}行,{1}({2})列 , 数据类型：{3}, 数据:{4}"
                        , cell.rowIndex + 1, cell.columnIndex + 1, IntToNumberSystem26(cell.columnIndex + 1)
                        , cell.type, cell.value));
                }
            }

            return result;
        }


        /// <summary>
        /// 将指定的自然数转换为26进制表示。映射关系：[1-26] ->[A-Z]。
        /// </summary>
        /// <param name="n">自然数（如果无效，则返回空字符串）。</param>
        /// <returns>26进制表示。</returns>
        public static string IntToNumberSystem26(int n)
        {
            string s = string.Empty;
            while (n > 0)
            {
                int m = n % 26;
                if (m == 0) m = 26;
                s = (char)(m + 64) + s;
                n = (n - m) / 26;
            }
            return s;
        }


        public static bool CheckDataType(string data, string type)
        {
            if (type.EndsWith("[]"))
                return ArrayCheck(data, type.Substring(0, type.Length - 2));
            switch (type)
            {
                case "float":
                    return FloatCheck(data);
                case "int":
                    return IntCheck(data);
                case "string":
                    return true;
                case "bool":
                    return BoolCheck(data);
                case "Vector2":
                    return Vector2Check(data);
                case "Vector3":
                    return Vector3Check(data);
                case "Color":
                    return ColorCheck(data);
                case "Date":
                    return DateCheck(data);
                default:
                    return false;
            }
        }

        private static bool IntCheck(string val)
        {
            int iValue;
            if (int.TryParse(val, out iValue))
            {
                return true;
            }
            return false;
        }

        private static bool FloatCheck(string val)
        {
            float fValue;
            if (float.TryParse(val, out fValue))
            {
                return true;
            }
            return false;
        }

        private static bool BoolCheck(string val)
        {
            bool bValue;
            if (bool.TryParse(val, out bValue))
            {
                return true;
            }
            return false;
        }


        private static bool DateCheck(string date)
        {
            DateTime dt = new DateTime();
            if (DateTime.TryParse(date, out dt))
            {
                return true;
            }
            return false;
        }

        public const string PATTERN = @"^[0-9A-Fa-f]+$";
        public static bool IsIllegalHexadecimal(string hex)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(hex, PATTERN);
        }

        private static bool ColorCheck(string color)
        {
            color = color.Trim('#');
            if (color.Length == 6 || color.Length == 8)
            {
                return IsIllegalHexadecimal(color);
            }
            return false;
        }


        private static bool Vector2Check(string sv2)
        {
            sv2 = sv2.Trim('(', ')');
            string[] farray = sv2.Split(',');
            if (farray.Length == 2 && FloatCheck(farray[0]) && FloatCheck(farray[1]))
            {
                return true;
            }
            return false;
        }

        private static bool Vector3Check(string sv3)
        {
            sv3 = sv3.Trim('(', ')');
            string[] farray = sv3.Split(',');
            if (farray.Length == 3 && FloatCheck(farray[0]) && FloatCheck(farray[1]) && FloatCheck(farray[2]))
            {
                return true;
            }
            return false;
        }

        private static bool ArrayCheck(string array, string itemType)
        {
            array = array.Trim();
            if (!array.StartsWith("[") || !array.EndsWith("]"))
                return false;
            string[] arrays = ArrayParse(array);
            if (arrays.Length <= 0)
                return true;
            foreach (string s in arrays)
            {
                if (!CheckDataType(s, itemType))
                    return false;
            }
            return true;
        }

        private static string[] ArrayParse(string value)
        {
            bool isClass = value.Contains("(");
            value = value.Trim();
            value = value.Replace("[", "");
            value = value.Replace("]", "");
            value = value.Replace("),", ")|");
            value = value.Replace("(", "");
            value = value.Replace(")", "");
            if (value.Equals(""))
                return new string[0];
            string[] arrays;
            if (isClass)
                arrays = value.Split('|');
            else
                arrays = value.Split(',');
            return arrays;
        }
    }

}
