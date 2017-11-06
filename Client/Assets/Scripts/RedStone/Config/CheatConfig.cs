using System;
using UnityEngine;

namespace Hotfire
{
    public class CheatConfig
    {
        private static string s_heroLvTo;
        private static bool b_heroLvTo;
        /// <summary>
        /// 英雄升级 (herolvto)
        /// </summary>
        public static string heroLvTo
        {
            get
            {
                if (!b_heroLvTo)
                {
                    b_heroLvTo = true;
                    s_heroLvTo = (string)TableManager.ParseValue("string", (TableManager.instance.GetData<TableConst>(3001).value));
                }
                return s_heroLvTo;
            }
        }

        private static string s_expTo;
        private static bool b_expTo;
        /// <summary>
        /// 英雄加经验 (expTo)
        /// </summary>
        public static string expTo
        {
            get
            {
                if (!b_expTo)
                {
                    b_expTo = true;
                    s_expTo = (string)TableManager.ParseValue("string", (TableManager.instance.GetData<TableConst>(3002).value));
                }
                return s_expTo;
            }
        }

        private static string s_addExp;
        private static bool b_addExp;
        /// <summary>
        /// 加经验 (addExp)
        /// </summary>
        public static string addExp
        {
            get
            {
                if (!b_addExp)
                {
                    b_addExp = true;
                    s_addExp = (string)TableManager.ParseValue("string", (TableManager.instance.GetData<TableConst>(3003).value));
                }
                return s_addExp;
            }
        }

        private static string s_addFund;
        private static bool b_addFund;
        /// <summary>
        /// 加军费 (addFund)
        /// </summary>
        public static string addFund
        {
            get
            {
                if (!b_addFund)
                {
                    b_addFund = true;
                    s_addFund = (string)TableManager.ParseValue("string", (TableManager.instance.GetData<TableConst>(3004).value));
                }
                return s_addFund;
            }
        }

        private static string s_addEnergy;
        private static bool b_addEnergy;
        /// <summary>
        /// 修改体力 (addEnergy)
        /// </summary>
        public static string addEnergy
        {
            get
            {
                if (!b_addEnergy)
                {
                    b_addEnergy = true;
                    s_addEnergy = (string)TableManager.ParseValue("string", (TableManager.instance.GetData<TableConst>(3005).value));
                }
                return s_addEnergy;
            }
        }

        private static string s_levelUp;
        private static bool b_levelUp;
        /// <summary>
        /// 修改主角 以及所选职业等级 (levelUp)
        /// </summary>
        public static string levelUp
        {
            get
            {
                if (!b_levelUp)
                {
                    b_levelUp = true;
                    s_levelUp = (string)TableManager.ParseValue("string", (TableManager.instance.GetData<TableConst>(3006).value));
                }
                return s_levelUp;
            }
        }

        private static string s_addMail;
        private static bool b_addMail;
        /// <summary>
        /// 加邮件的gm命令，依赖TableMail表中的systemId,可以发放TableMail中配置的邮件 (addMail)
        /// </summary>
        public static string addMail
        {
            get
            {
                if (!b_addMail)
                {
                    b_addMail = true;
                    s_addMail = (string)TableManager.ParseValue("string", (TableManager.instance.GetData<TableConst>(3007).value));
                }
                return s_addMail;
            }
        }

        private static string s_addElo;
        private static bool b_addElo;
        /// <summary>
        /// 添加elo值 (addElo)
        /// </summary>
        public static string addElo
        {
            get
            {
                if (!b_addElo)
                {
                    b_addElo = true;
                    s_addElo = (string)TableManager.ParseValue("string", (TableManager.instance.GetData<TableConst>(3008).value));
                }
                return s_addElo;
            }
        }

        private static string s_addTechnologies;
        private static bool b_addTechnologies;
        /// <summary>
        /// 添加科技 (addtech)
        /// </summary>
        public static string addTechnologies
        {
            get
            {
                if (!b_addTechnologies)
                {
                    b_addTechnologies = true;
                    s_addTechnologies = (string)TableManager.ParseValue("string", (TableManager.instance.GetData<TableConst>(3009).value));
                }
                return s_addTechnologies;
            }
        }

        private static string s_addProfessionMedal;
        private static bool b_addProfessionMedal;
        /// <summary>
        /// 添加职业徽章 (addbadge)
        /// </summary>
        public static string addProfessionMedal
        {
            get
            {
                if (!b_addProfessionMedal)
                {
                    b_addProfessionMedal = true;
                    s_addProfessionMedal = (string)TableManager.ParseValue("string", (TableManager.instance.GetData<TableConst>(3010).value));
                }
                return s_addProfessionMedal;
            }
        }

        private static string s_addItem;
        private static bool b_addItem;
        /// <summary>
        /// 添加Item（用法：additem id count） (additem)
        /// </summary>
        public static string addItem
        {
            get
            {
                if (!b_addItem)
                {
                    b_addItem = true;
                    s_addItem = (string)TableManager.ParseValue("string", (TableManager.instance.GetData<TableConst>(3011).value));
                }
                return s_addItem;
            }
        }

        private static string s_doMission;
        private static bool b_doMission;
        /// <summary>
        /// 完成每日任务（doMission 任务槽id） (doMission)
        /// </summary>
        public static string doMission
        {
            get
            {
                if (!b_doMission)
                {
                    b_doMission = true;
                    s_doMission = (string)TableManager.ParseValue("string", (TableManager.instance.GetData<TableConst>(3012).value));
                }
                return s_doMission;
            }
        }

        private static string s_chooseMission;
        private static bool b_chooseMission;
        /// <summary>
        /// 选择每日任务(chooseMission containerId missionId) (chooseMission)
        /// </summary>
        public static string chooseMission
        {
            get
            {
                if (!b_chooseMission)
                {
                    b_chooseMission = true;
                    s_chooseMission = (string)TableManager.ParseValue("string", (TableManager.instance.GetData<TableConst>(3013).value));
                }
                return s_chooseMission;
            }
        }

        private static string s_resetMission;
        private static bool b_resetMission;
        /// <summary>
        /// 重置每日任务 (resetMission)
        /// </summary>
        public static string resetMission
        {
            get
            {
                if (!b_resetMission)
                {
                    b_resetMission = true;
                    s_resetMission = (string)TableManager.ParseValue("string", (TableManager.instance.GetData<TableConst>(3014).value));
                }
                return s_resetMission;
            }
        }

    }
}
