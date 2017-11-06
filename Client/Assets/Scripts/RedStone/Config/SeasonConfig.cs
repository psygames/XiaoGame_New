using System;
using UnityEngine;

namespace Hotfire
{
    public class SeasonConfig
    {
        private static int s_seasonLegendLevel;
        private static bool b_seasonLegendLevel;
        /// <summary>
        /// //传奇对应的天梯等级默认为0级 (0)
        /// </summary>
        public static int seasonLegendLevel
        {
            get
            {
                if (!b_seasonLegendLevel)
                {
                    b_seasonLegendLevel = true;
                    s_seasonLegendLevel = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(4005).value));
                }
                return s_seasonLegendLevel;
            }
        }

        private static int s_seasonWinningStreak;
        private static bool b_seasonWinningStreak;
        /// <summary>
        /// //赛季连赢三场会额外获得一个星星， 并且重新计算连赢场数 (3)
        /// </summary>
        public static int seasonWinningStreak
        {
            get
            {
                if (!b_seasonWinningStreak)
                {
                    b_seasonWinningStreak = true;
                    s_seasonWinningStreak = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(4006).value));
                }
                return s_seasonWinningStreak;
            }
        }

        private static int s_seasonPunishMinMinute;
        private static bool b_seasonPunishMinMinute;
        /// <summary>
        /// //赛季逃逸比赛时间大于1分钟会被处罚 (1)
        /// </summary>
        public static int seasonPunishMinMinute
        {
            get
            {
                if (!b_seasonPunishMinMinute)
                {
                    b_seasonPunishMinMinute = true;
                    s_seasonPunishMinMinute = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(4007).value));
                }
                return s_seasonPunishMinMinute;
            }
        }

        private static int s_seasonEscape1Minute;
        private static bool b_seasonEscape1Minute;
        /// <summary>
        /// //比赛逃逸大于一分钟扣1分 (1)
        /// </summary>
        public static int seasonEscape1Minute
        {
            get
            {
                if (!b_seasonEscape1Minute)
                {
                    b_seasonEscape1Minute = true;
                    s_seasonEscape1Minute = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(4008).value));
                }
                return s_seasonEscape1Minute;
            }
        }

        private static int s_seasonEscape;
        private static bool b_seasonEscape;
        /// <summary>
        /// //比赛逃逸大于一分钟并且没有重连扣3分 (3)
        /// </summary>
        public static int seasonEscape
        {
            get
            {
                if (!b_seasonEscape)
                {
                    b_seasonEscape = true;
                    s_seasonEscape = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(4009).value));
                }
                return s_seasonEscape;
            }
        }

        private static int s_seasonBttleRewardNeeds;
        private static bool b_seasonBttleRewardNeeds;
        /// <summary>
        /// //赛季场次奖励需要参加1比赛才能领奖 (1)
        /// </summary>
        public static int seasonBttleRewardNeeds
        {
            get
            {
                if (!b_seasonBttleRewardNeeds)
                {
                    b_seasonBttleRewardNeeds = true;
                    s_seasonBttleRewardNeeds = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(4011).value));
                }
                return s_seasonBttleRewardNeeds;
            }
        }

        private static int s_medalFundExchangeRate;
        private static bool b_medalFundExchangeRate;
        /// <summary>
        /// //一个赛季奖章对应10军费 (10)
        /// </summary>
        public static int medalFundExchangeRate
        {
            get
            {
                if (!b_medalFundExchangeRate)
                {
                    b_medalFundExchangeRate = true;
                    s_medalFundExchangeRate = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(4012).value));
                }
                return s_medalFundExchangeRate;
            }
        }

        private static int s_initLevel ;
        private static bool b_initLevel ;
        /// <summary>
        /// //初始天梯等级 (11)
        /// </summary>
        public static int initLevel 
        {
            get
            {
                if (!b_initLevel )
                {
                    b_initLevel  = true;
                    s_initLevel  = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(4013).value));
                }
                return s_initLevel ;
            }
        }

        private static int s_initElo;
        private static bool b_initElo;
        /// <summary>
        /// //初始Elo积分 (1500)
        /// </summary>
        public static int initElo
        {
            get
            {
                if (!b_initElo)
                {
                    b_initElo = true;
                    s_initElo = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(4014).value));
                }
                return s_initElo;
            }
        }

        private static int s_recordRankInterval;
        private static bool b_recordRankInterval;
        /// <summary>
        /// //记录排行榜的时间间隔,一分钟为单位 (1)
        /// </summary>
        public static int recordRankInterval
        {
            get
            {
                if (!b_recordRankInterval)
                {
                    b_recordRankInterval = true;
                    s_recordRankInterval = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(4015).value));
                }
                return s_recordRankInterval;
            }
        }

    }
}
