using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text;
using Mono.Xml;
using xmlparser;

namespace checker
{
    public struct CheckResult
    {
        public CheckResult(bool isSucceed, string message = "")
        {
            this.isSucceed = isSucceed;
            this.message = message;
        }

        public void Set(bool isSucceed, string message = "")
        {
            this.isSucceed = isSucceed;
            this.message = message;
        }

        public override string ToString()
        {
            return isSucceed ? CHECK_SUCCEED : message;
        }
        public bool isSucceed;
        public string message;
        private const string CHECK_SUCCEED = " CHECK SUCCEED !!!";
    }

    class Checker
    {
        private SecurityParser m_parser = new SecurityParser();
        private CheckResult m_result = new CheckResult();
        public CheckResult result { get { return m_result; } }

        public void CheckFile(string path)
        {
            string tableName = Path.GetFileNameWithoutExtension(path);

            string xmlText = FileTool.ReadText(path);
            var parser = new XmlParser(100);
            var table = parser.GetTableData(tableName, xmlText);

            m_result = table.Check();
        }
    }
}
