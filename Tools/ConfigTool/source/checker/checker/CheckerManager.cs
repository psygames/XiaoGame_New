using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace checker
{
    class CheckerManager
    {
        private Checker m_checker = new Checker();

        public CheckResult CheckDirectory(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] files = dir.GetFiles("*.xml");
            foreach (FileInfo fi in files)
            {
                CheckResult result = CheckFile(fi.FullName);
                if (!result.isSucceed)
                    return result;
            }
            return new CheckResult(true);
        }

        public CheckResult CheckFile(string path)
        {
            m_checker.CheckFile(path);
            return m_checker.result;
        }
    }
}
