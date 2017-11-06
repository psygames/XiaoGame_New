using System;
using UnityEngine;

namespace Hotfire
{
    public class CommonConfig
    {
        private static int s_fund;
        private static bool b_fund;
        /// <summary>
        /// 军费 (9000)
        /// </summary>
        public static int fund
        {
            get
            {
                if (!b_fund)
                {
                    b_fund = true;
                    s_fund = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(1).value));
                }
                return s_fund;
            }
        }

        private static int s_diamond;
        private static bool b_diamond;
        /// <summary>
        /// 钻石 (200)
        /// </summary>
        public static int diamond
        {
            get
            {
                if (!b_diamond)
                {
                    b_diamond = true;
                    s_diamond = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(2).value));
                }
                return s_diamond;
            }
        }

        private static int[] s_professionid;
        private static bool b_professionid;
        /// <summary>
        /// 创建角色初始职业 ([1])
        /// </summary>
        public static int[] professionid
        {
            get
            {
                if (!b_professionid)
                {
                    b_professionid = true;
                    s_professionid = (int[])TableManager.ParseValue("int[]", (TableManager.instance.GetData<TableConst>(3).value));
                }
                return s_professionid;
            }
        }

        private static int s_medal;
        private static bool b_medal;
        /// <summary>
        /// 狗牌 (900)
        /// </summary>
        public static int medal
        {
            get
            {
                if (!b_medal)
                {
                    b_medal = true;
                    s_medal = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(4).value));
                }
                return s_medal;
            }
        }

        private static int s_primaryGunId;
        private static bool b_primaryGunId;
        /// <summary>
        /// 主枪械id (1)
        /// </summary>
        public static int primaryGunId
        {
            get
            {
                if (!b_primaryGunId)
                {
                    b_primaryGunId = true;
                    s_primaryGunId = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(5).value));
                }
                return s_primaryGunId;
            }
        }

        private static int s_secondaryGunId;
        private static bool b_secondaryGunId;
        /// <summary>
        /// 副枪械id (8)
        /// </summary>
        public static int secondaryGunId
        {
            get
            {
                if (!b_secondaryGunId)
                {
                    b_secondaryGunId = true;
                    s_secondaryGunId = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(6).value));
                }
                return s_secondaryGunId;
            }
        }

        private static int[] s_equipLevel;
        private static bool b_equipLevel;
        /// <summary>
        /// 装备等级 ([1,1,1])
        /// </summary>
        public static int[] equipLevel
        {
            get
            {
                if (!b_equipLevel)
                {
                    b_equipLevel = true;
                    s_equipLevel = (int[])TableManager.ParseValue("int[]", (TableManager.instance.GetData<TableConst>(7).value));
                }
                return s_equipLevel;
            }
        }

        private static int[] s_consumableId;
        private static bool b_consumableId;
        /// <summary>
        /// 消耗品 ([2])
        /// </summary>
        public static int[] consumableId
        {
            get
            {
                if (!b_consumableId)
                {
                    b_consumableId = true;
                    s_consumableId = (int[])TableManager.ParseValue("int[]", (TableManager.instance.GetData<TableConst>(8).value));
                }
                return s_consumableId;
            }
        }

        private static int s_matchType;
        private static bool b_matchType;
        /// <summary>
        /// 赛事类型 (1)
        /// </summary>
        public static int matchType
        {
            get
            {
                if (!b_matchType)
                {
                    b_matchType = true;
                    s_matchType = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(9).value));
                }
                return s_matchType;
            }
        }

        private static int s_killScore;
        private static bool b_killScore;
        /// <summary>
        /// 击杀得分 (20)
        /// </summary>
        public static int killScore
        {
            get
            {
                if (!b_killScore)
                {
                    b_killScore = true;
                    s_killScore = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(10).value));
                }
                return s_killScore;
            }
        }

        private static int s_mutilKillCount;
        private static bool b_mutilKillCount;
        /// <summary>
        /// 连杀数 (10)
        /// </summary>
        public static int mutilKillCount
        {
            get
            {
                if (!b_mutilKillCount)
                {
                    b_mutilKillCount = true;
                    s_mutilKillCount = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(11).value));
                }
                return s_mutilKillCount;
            }
        }

        private static int s_mutilKillRate;
        private static bool b_mutilKillRate;
        /// <summary>
        /// 连杀得分增长率 (5)
        /// </summary>
        public static int mutilKillRate
        {
            get
            {
                if (!b_mutilKillRate)
                {
                    b_mutilKillRate = true;
                    s_mutilKillRate = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(12).value));
                }
                return s_mutilKillRate;
            }
        }

        private static int s_mutilKillScore;
        private static bool b_mutilKillScore;
        /// <summary>
        /// 连杀得分 (50)
        /// </summary>
        public static int mutilKillScore
        {
            get
            {
                if (!b_mutilKillScore)
                {
                    b_mutilKillScore = true;
                    s_mutilKillScore = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(13).value));
                }
                return s_mutilKillScore;
            }
        }

        private static int s_firstBloodScore;
        private static bool b_firstBloodScore;
        /// <summary>
        /// 一血得分 (20)
        /// </summary>
        public static int firstBloodScore
        {
            get
            {
                if (!b_firstBloodScore)
                {
                    b_firstBloodScore = true;
                    s_firstBloodScore = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(14).value));
                }
                return s_firstBloodScore;
            }
        }

        private static int s_assistsScore;
        private static bool b_assistsScore;
        /// <summary>
        /// 助攻得分 (10)
        /// </summary>
        public static int assistsScore
        {
            get
            {
                if (!b_assistsScore)
                {
                    b_assistsScore = true;
                    s_assistsScore = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(15).value));
                }
                return s_assistsScore;
            }
        }

        private static int s_meleeFirst;
        private static bool b_meleeFirst;
        /// <summary>
        /// 混战第一名得分 (100)
        /// </summary>
        public static int meleeFirst
        {
            get
            {
                if (!b_meleeFirst)
                {
                    b_meleeFirst = true;
                    s_meleeFirst = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(16).value));
                }
                return s_meleeFirst;
            }
        }

        private static int s_meleeSecond;
        private static bool b_meleeSecond;
        /// <summary>
        /// 混战第二名得分 (50)
        /// </summary>
        public static int meleeSecond
        {
            get
            {
                if (!b_meleeSecond)
                {
                    b_meleeSecond = true;
                    s_meleeSecond = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(17).value));
                }
                return s_meleeSecond;
            }
        }

        private static int s_meleeThird;
        private static bool b_meleeThird;
        /// <summary>
        /// 混战第三名得分 (30)
        /// </summary>
        public static int meleeThird
        {
            get
            {
                if (!b_meleeThird)
                {
                    b_meleeThird = true;
                    s_meleeThird = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(18).value));
                }
                return s_meleeThird;
            }
        }

        private static int s_winScore;
        private static bool b_winScore;
        /// <summary>
        /// 胜利得分 (30)
        /// </summary>
        public static int winScore
        {
            get
            {
                if (!b_winScore)
                {
                    b_winScore = true;
                    s_winScore = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(19).value));
                }
                return s_winScore;
            }
        }

        private static int s_drawScore;
        private static bool b_drawScore;
        /// <summary>
        /// 平局得分 (10)
        /// </summary>
        public static int drawScore
        {
            get
            {
                if (!b_drawScore)
                {
                    b_drawScore = true;
                    s_drawScore = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(20).value));
                }
                return s_drawScore;
            }
        }

        private static int s_revengeScore;
        private static bool b_revengeScore;
        /// <summary>
        /// 复仇得分 (10)
        /// </summary>
        public static int revengeScore
        {
            get
            {
                if (!b_revengeScore)
                {
                    b_revengeScore = true;
                    s_revengeScore = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(21).value));
                }
                return s_revengeScore;
            }
        }

        private static int s_itemNumber;
        private static bool b_itemNumber;
        /// <summary>
        /// 道具数量 (150)
        /// </summary>
        public static int itemNumber
        {
            get
            {
                if (!b_itemNumber)
                {
                    b_itemNumber = true;
                    s_itemNumber = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(22).value));
                }
                return s_itemNumber;
            }
        }

        private static int s_guildMemberMax;
        private static bool b_guildMemberMax;
        /// <summary>
        /// 军团人数上限 (50)
        /// </summary>
        public static int guildMemberMax
        {
            get
            {
                if (!b_guildMemberMax)
                {
                    b_guildMemberMax = true;
                    s_guildMemberMax = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(23).value));
                }
                return s_guildMemberMax;
            }
        }

        private static int s_vicePresidentMax;
        private static bool b_vicePresidentMax;
        /// <summary>
        /// 副团长人数上限 (3)
        /// </summary>
        public static int vicePresidentMax
        {
            get
            {
                if (!b_vicePresidentMax)
                {
                    b_vicePresidentMax = true;
                    s_vicePresidentMax = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(24).value));
                }
                return s_vicePresidentMax;
            }
        }

        private static int s_eliteMax;
        private static bool b_eliteMax;
        /// <summary>
        /// 精英人数上限 (20)
        /// </summary>
        public static int eliteMax
        {
            get
            {
                if (!b_eliteMax)
                {
                    b_eliteMax = true;
                    s_eliteMax = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(25).value));
                }
                return s_eliteMax;
            }
        }

        private static int s_reJoinTime;
        private static bool b_reJoinTime;
        /// <summary>
        /// 退团后重新加入时间 (24)
        /// </summary>
        public static int reJoinTime
        {
            get
            {
                if (!b_reJoinTime)
                {
                    b_reJoinTime = true;
                    s_reJoinTime = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(26).value));
                }
                return s_reJoinTime;
            }
        }

        private static int s_reJoinDonateTime;
        private static bool b_reJoinDonateTime;
        /// <summary>
        /// 重新加入军团后的捐赠时间 (3)
        /// </summary>
        public static int reJoinDonateTime
        {
            get
            {
                if (!b_reJoinDonateTime)
                {
                    b_reJoinDonateTime = true;
                    s_reJoinDonateTime = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(27).value));
                }
                return s_reJoinDonateTime;
            }
        }

        private static int s_askDonateTime;
        private static bool b_askDonateTime;
        /// <summary>
        /// 请求捐赠的时间间隔 (6)
        /// </summary>
        public static int askDonateTime
        {
            get
            {
                if (!b_askDonateTime)
                {
                    b_askDonateTime = true;
                    s_askDonateTime = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(28).value));
                }
                return s_askDonateTime;
            }
        }

        private static int s_openGuildLevel;
        private static bool b_openGuildLevel;
        /// <summary>
        /// 开启军团等级 (1)
        /// </summary>
        public static int openGuildLevel
        {
            get
            {
                if (!b_openGuildLevel)
                {
                    b_openGuildLevel = true;
                    s_openGuildLevel = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(29).value));
                }
                return s_openGuildLevel;
            }
        }

        private static int s_foundGuildCost;
        private static bool b_foundGuildCost;
        /// <summary>
        /// 创建军团花费 (300)
        /// </summary>
        public static int foundGuildCost
        {
            get
            {
                if (!b_foundGuildCost)
                {
                    b_foundGuildCost = true;
                    s_foundGuildCost = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(30).value));
                }
                return s_foundGuildCost;
            }
        }

        private static int s_consumableTime;
        private static bool b_consumableTime;
        /// <summary>
        /// 消耗品有效时间 (86400000)
        /// </summary>
        public static int consumableTime
        {
            get
            {
                if (!b_consumableTime)
                {
                    b_consumableTime = true;
                    s_consumableTime = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(31).value));
                }
                return s_consumableTime;
            }
        }

        private static int s_AccessoryBagSize;
        private static bool b_AccessoryBagSize;
        /// <summary>
        /// 枪械配件背包大小 (40)
        /// </summary>
        public static int AccessoryBagSize
        {
            get
            {
                if (!b_AccessoryBagSize)
                {
                    b_AccessoryBagSize = true;
                    s_AccessoryBagSize = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(32).value));
                }
                return s_AccessoryBagSize;
            }
        }

        private static int s_SkillImplantUnlockTier;
        private static bool b_SkillImplantUnlockTier;
        /// <summary>
        /// 技能植入体解锁Tier等级 (4)
        /// </summary>
        public static int SkillImplantUnlockTier
        {
            get
            {
                if (!b_SkillImplantUnlockTier)
                {
                    b_SkillImplantUnlockTier = true;
                    s_SkillImplantUnlockTier = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(33).value));
                }
                return s_SkillImplantUnlockTier;
            }
        }

        private static int s_PassiveSkillUnloLevel;
        private static bool b_PassiveSkillUnloLevel;
        /// <summary>
        /// 人物被动技能解锁等级 (26)
        /// </summary>
        public static int PassiveSkillUnloLevel
        {
            get
            {
                if (!b_PassiveSkillUnloLevel)
                {
                    b_PassiveSkillUnloLevel = true;
                    s_PassiveSkillUnloLevel = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(36).value));
                }
                return s_PassiveSkillUnloLevel;
            }
        }

        private static int s_ShieldRecoverInterval;
        private static bool b_ShieldRecoverInterval;
        /// <summary>
        /// 护盾回复间隔 (1000)
        /// </summary>
        public static int ShieldRecoverInterval
        {
            get
            {
                if (!b_ShieldRecoverInterval)
                {
                    b_ShieldRecoverInterval = true;
                    s_ShieldRecoverInterval = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(37).value));
                }
                return s_ShieldRecoverInterval;
            }
        }

        private static int s_characteristicsUnlockTier;
        private static bool b_characteristicsUnlockTier;
        /// <summary>
        /// 枪械特性解锁的tier (1)
        /// </summary>
        public static int characteristicsUnlockTier
        {
            get
            {
                if (!b_characteristicsUnlockTier)
                {
                    b_characteristicsUnlockTier = true;
                    s_characteristicsUnlockTier = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(38).value));
                }
                return s_characteristicsUnlockTier;
            }
        }

        private static int s_bornRandomRange;
        private static bool b_bornRandomRange;
        /// <summary>
        /// 出生点范围直径 (2)
        /// </summary>
        public static int bornRandomRange
        {
            get
            {
                if (!b_bornRandomRange)
                {
                    b_bornRandomRange = true;
                    s_bornRandomRange = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(44).value));
                }
                return s_bornRandomRange;
            }
        }

        private static int s_safeBornRange;
        private static bool b_safeBornRange;
        /// <summary>
        /// 扫描安全范围直径 (20)
        /// </summary>
        public static int safeBornRange
        {
            get
            {
                if (!b_safeBornRange)
                {
                    b_safeBornRange = true;
                    s_safeBornRange = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(45).value));
                }
                return s_safeBornRange;
            }
        }

        private static Color s_friendBatteryRobotColor;
        private static bool b_friendBatteryRobotColor;
        /// <summary>
        /// 同队炮台颜色 (#2E69CBFF)
        /// </summary>
        public static Color friendBatteryRobotColor
        {
            get
            {
                if (!b_friendBatteryRobotColor)
                {
                    b_friendBatteryRobotColor = true;
                    s_friendBatteryRobotColor = (Color)TableManager.ParseValue("Color", (TableManager.instance.GetData<TableConst>(46).value));
                }
                return s_friendBatteryRobotColor;
            }
        }

        private static Color s_enemyBatteryRobotColor;
        private static bool b_enemyBatteryRobotColor;
        /// <summary>
        /// 敌对炮台颜色 (#E52F37FF)
        /// </summary>
        public static Color enemyBatteryRobotColor
        {
            get
            {
                if (!b_enemyBatteryRobotColor)
                {
                    b_enemyBatteryRobotColor = true;
                    s_enemyBatteryRobotColor = (Color)TableManager.ParseValue("Color", (TableManager.instance.GetData<TableConst>(47).value));
                }
                return s_enemyBatteryRobotColor;
            }
        }

        private static int s_integralMax;
        private static bool b_integralMax;
        /// <summary>
        /// 积分上限 (20000)
        /// </summary>
        public static int integralMax
        {
            get
            {
                if (!b_integralMax)
                {
                    b_integralMax = true;
                    s_integralMax = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(48).value));
                }
                return s_integralMax;
            }
        }

        private static int s_integralWin;
        private static bool b_integralWin;
        /// <summary>
        /// 赢一次积分 (10)
        /// </summary>
        public static int integralWin
        {
            get
            {
                if (!b_integralWin)
                {
                    b_integralWin = true;
                    s_integralWin = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(49).value));
                }
                return s_integralWin;
            }
        }

        private static int s_integralLose;
        private static bool b_integralLose;
        /// <summary>
        /// 输一次的积分 (5)
        /// </summary>
        public static int integralLose
        {
            get
            {
                if (!b_integralLose)
                {
                    b_integralLose = true;
                    s_integralLose = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(50).value));
                }
                return s_integralLose;
            }
        }

        private static int s_integralDraw;
        private static bool b_integralDraw;
        /// <summary>
        /// 平局的积分 (5)
        /// </summary>
        public static int integralDraw
        {
            get
            {
                if (!b_integralDraw)
                {
                    b_integralDraw = true;
                    s_integralDraw = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(51).value));
                }
                return s_integralDraw;
            }
        }

        private static int s_escapeReconnectPoints;
        private static bool b_escapeReconnectPoints;
        /// <summary>
        /// 重连逃逸分 (1)
        /// </summary>
        public static int escapeReconnectPoints
        {
            get
            {
                if (!b_escapeReconnectPoints)
                {
                    b_escapeReconnectPoints = true;
                    s_escapeReconnectPoints = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(52).value));
                }
                return s_escapeReconnectPoints;
            }
        }

        private static int s_escapeNoReconnectPoints;
        private static bool b_escapeNoReconnectPoints;
        /// <summary>
        /// 不重连逃逸分 (3)
        /// </summary>
        public static int escapeNoReconnectPoints
        {
            get
            {
                if (!b_escapeNoReconnectPoints)
                {
                    b_escapeNoReconnectPoints = true;
                    s_escapeNoReconnectPoints = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(53).value));
                }
                return s_escapeNoReconnectPoints;
            }
        }

        private static int s_finishReducePoints;
        private static bool b_finishReducePoints;
        /// <summary>
        /// 完成比赛减少逃逸分 (1)
        /// </summary>
        public static int finishReducePoints
        {
            get
            {
                if (!b_finishReducePoints)
                {
                    b_finishReducePoints = true;
                    s_finishReducePoints = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(54).value));
                }
                return s_finishReducePoints;
            }
        }

        private static int s_defaultMapSelect;
        private static bool b_defaultMapSelect;
        /// <summary>
        /// 默认选择的MapID (5)
        /// </summary>
        public static int defaultMapSelect
        {
            get
            {
                if (!b_defaultMapSelect)
                {
                    b_defaultMapSelect = true;
                    s_defaultMapSelect = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(55).value));
                }
                return s_defaultMapSelect;
            }
        }

        private static int s_systemMailSenderId;
        private static bool b_systemMailSenderId;
        /// <summary>
        /// 系统邮件规定发送方id为-1 (-1)
        /// </summary>
        public static int systemMailSenderId
        {
            get
            {
                if (!b_systemMailSenderId)
                {
                    b_systemMailSenderId = true;
                    s_systemMailSenderId = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(56).value));
                }
                return s_systemMailSenderId;
            }
        }

        private static int s_mailWithRewardLast;
        private static bool b_mailWithRewardLast;
        /// <summary>
        /// 可以领奖的邮件保留14天 (14)
        /// </summary>
        public static int mailWithRewardLast
        {
            get
            {
                if (!b_mailWithRewardLast)
                {
                    b_mailWithRewardLast = true;
                    s_mailWithRewardLast = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(57).value));
                }
                return s_mailWithRewardLast;
            }
        }

        private static int s_mailRewardReceivedLast;
        private static bool b_mailRewardReceivedLast;
        /// <summary>
        /// 领完奖的邮件保留一天 (1)
        /// </summary>
        public static int mailRewardReceivedLast
        {
            get
            {
                if (!b_mailRewardReceivedLast)
                {
                    b_mailRewardReceivedLast = true;
                    s_mailRewardReceivedLast = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(58).value));
                }
                return s_mailRewardReceivedLast;
            }
        }

        private static int s_chestDeep;
        private static bool b_chestDeep;
        /// <summary>
        /// 宝箱中还可以配置另一个宝箱，这样存在开宝箱死循环的危险，限制循环开宝箱的次数 (5)
        /// </summary>
        public static int chestDeep
        {
            get
            {
                if (!b_chestDeep)
                {
                    b_chestDeep = true;
                    s_chestDeep = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(59).value));
                }
                return s_chestDeep;
            }
        }

        private static float s_hitRateReduceByMoveSpeed;
        private static bool b_hitRateReduceByMoveSpeed;
        /// <summary>
        /// 命中率受移动影响的比例 (1)
        /// </summary>
        public static float hitRateReduceByMoveSpeed
        {
            get
            {
                if (!b_hitRateReduceByMoveSpeed)
                {
                    b_hitRateReduceByMoveSpeed = true;
                    s_hitRateReduceByMoveSpeed = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(60).value));
                }
                return s_hitRateReduceByMoveSpeed;
            }
        }

        private static int s_randomModeEnergyCost;
        private static bool b_randomModeEnergyCost;
        /// <summary>
        /// 随机模式体力消耗 (2)
        /// </summary>
        public static int randomModeEnergyCost
        {
            get
            {
                if (!b_randomModeEnergyCost)
                {
                    b_randomModeEnergyCost = true;
                    s_randomModeEnergyCost = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(61).value));
                }
                return s_randomModeEnergyCost;
            }
        }

        private static int s_battleMatchMaxWait;
        private static bool b_battleMatchMaxWait;
        /// <summary>
        /// 战斗匹配时游戏服最多等待战场管理服5分钟 (1)
        /// </summary>
        public static int battleMatchMaxWait
        {
            get
            {
                if (!b_battleMatchMaxWait)
                {
                    b_battleMatchMaxWait = true;
                    s_battleMatchMaxWait = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(62).value));
                }
                return s_battleMatchMaxWait;
            }
        }

        private static int s_seasonRankMaxShow;
        private static bool b_seasonRankMaxShow;
        /// <summary>
        /// 赛季个人排行榜最多显示100人 (100)
        /// </summary>
        public static int seasonRankMaxShow
        {
            get
            {
                if (!b_seasonRankMaxShow)
                {
                    b_seasonRankMaxShow = true;
                    s_seasonRankMaxShow = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(63).value));
                }
                return s_seasonRankMaxShow;
            }
        }

        private static int s_seasonLegionRankMaxShow;
        private static bool b_seasonLegionRankMaxShow;
        /// <summary>
        /// 赛季军团排行榜最多显示50个军团 (50)
        /// </summary>
        public static int seasonLegionRankMaxShow
        {
            get
            {
                if (!b_seasonLegionRankMaxShow)
                {
                    b_seasonLegionRankMaxShow = true;
                    s_seasonLegionRankMaxShow = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(64).value));
                }
                return s_seasonLegionRankMaxShow;
            }
        }

        private static int s_seasonrRankMaxRetain;
        private static bool b_seasonrRankMaxRetain;
        /// <summary>
        /// 赛季个人排行榜保留上赛季前十的玩家 (10)
        /// </summary>
        public static int seasonrRankMaxRetain
        {
            get
            {
                if (!b_seasonrRankMaxRetain)
                {
                    b_seasonrRankMaxRetain = true;
                    s_seasonrRankMaxRetain = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(65).value));
                }
                return s_seasonrRankMaxRetain;
            }
        }

        private static int[] s_badgeApproach;
        private static bool b_badgeApproach;
        /// <summary>
        /// 徽章获取途径 (1,2,3)
        /// </summary>
        public static int[] badgeApproach
        {
            get
            {
                if (!b_badgeApproach)
                {
                    b_badgeApproach = true;
                    s_badgeApproach = (int[])TableManager.ParseValue("int[]", (TableManager.instance.GetData<TableConst>(66).value));
                }
                return s_badgeApproach;
            }
        }

        private static int[] s_blueprint;
        private static bool b_blueprint;
        /// <summary>
        /// 蓝图获取途径 (1)
        /// </summary>
        public static int[] blueprint
        {
            get
            {
                if (!b_blueprint)
                {
                    b_blueprint = true;
                    s_blueprint = (int[])TableManager.ParseValue("int[]", (TableManager.instance.GetData<TableConst>(67).value));
                }
                return s_blueprint;
            }
        }

        private static int s_initProfessionMedal;
        private static bool b_initProfessionMedal;
        /// <summary>
        /// 角色创建角色时拥有的各职业徽章数量 (100)
        /// </summary>
        public static int initProfessionMedal
        {
            get
            {
                if (!b_initProfessionMedal)
                {
                    b_initProfessionMedal = true;
                    s_initProfessionMedal = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(68).value));
                }
                return s_initProfessionMedal;
            }
        }

        private static int s_refreshTime;
        private static bool b_refreshTime;
        /// <summary>
        /// 游戏内任务等刷新的时间点，暂定为凌晨4点钟 (4)
        /// </summary>
        public static int refreshTime
        {
            get
            {
                if (!b_refreshTime)
                {
                    b_refreshTime = true;
                    s_refreshTime = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(69).value));
                }
                return s_refreshTime;
            }
        }

        private static int s_commomShopRefreshPeriod;
        private static bool b_commomShopRefreshPeriod;
        /// <summary>
        /// 普通商店每6小时刷新一次 (6)
        /// </summary>
        public static int commomShopRefreshPeriod
        {
            get
            {
                if (!b_commomShopRefreshPeriod)
                {
                    b_commomShopRefreshPeriod = true;
                    s_commomShopRefreshPeriod = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(70).value));
                }
                return s_commomShopRefreshPeriod;
            }
        }

        private static int s_missionResetCost;
        private static bool b_missionResetCost;
        /// <summary>
        /// 每日任务刷新消耗钻石 (10)
        /// </summary>
        public static int missionResetCost
        {
            get
            {
                if (!b_missionResetCost)
                {
                    b_missionResetCost = true;
                    s_missionResetCost = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(71).value));
                }
                return s_missionResetCost;
            }
        }

        private static int[] s_dispatchMissionRefreshCost;
        private static bool b_dispatchMissionRefreshCost;
        /// <summary>
        /// 外派任务刷新消耗 (10,20,30)
        /// </summary>
        public static int[] dispatchMissionRefreshCost
        {
            get
            {
                if (!b_dispatchMissionRefreshCost)
                {
                    b_dispatchMissionRefreshCost = true;
                    s_dispatchMissionRefreshCost = (int[])TableManager.ParseValue("int[]", (TableManager.instance.GetData<TableConst>(72).value));
                }
                return s_dispatchMissionRefreshCost;
            }
        }

        private static int s_dispatchMissionMaxRefresh;
        private static bool b_dispatchMissionMaxRefresh;
        /// <summary>
        /// 外派任务最大重置次数 (3)
        /// </summary>
        public static int dispatchMissionMaxRefresh
        {
            get
            {
                if (!b_dispatchMissionMaxRefresh)
                {
                    b_dispatchMissionMaxRefresh = true;
                    s_dispatchMissionMaxRefresh = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(73).value));
                }
                return s_dispatchMissionMaxRefresh;
            }
        }

        private static int s_dispatchMissionTimeCardId;
        private static bool b_dispatchMissionTimeCardId;
        /// <summary>
        /// 外派任务时间卡Id (20004)
        /// </summary>
        public static int dispatchMissionTimeCardId
        {
            get
            {
                if (!b_dispatchMissionTimeCardId)
                {
                    b_dispatchMissionTimeCardId = true;
                    s_dispatchMissionTimeCardId = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(74).value));
                }
                return s_dispatchMissionTimeCardId;
            }
        }

        private static float s_energyRecoverInterval;
        private static bool b_energyRecoverInterval;
        /// <summary>
        /// 能量恢复周期 90秒 (900)
        /// </summary>
        public static float energyRecoverInterval
        {
            get
            {
                if (!b_energyRecoverInterval)
                {
                    b_energyRecoverInterval = true;
                    s_energyRecoverInterval = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(75).value));
                }
                return s_energyRecoverInterval;
            }
        }

        private static int s_chatRepeatTime;
        private static bool b_chatRepeatTime;
        /// <summary>
        /// 1分钟内不允许重复的聊天条数 (1)
        /// </summary>
        public static int chatRepeatTime
        {
            get
            {
                if (!b_chatRepeatTime)
                {
                    b_chatRepeatTime = true;
                    s_chatRepeatTime = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(76).value));
                }
                return s_chatRepeatTime;
            }
        }

        private static int s_chatRepeatMax;
        private static bool b_chatRepeatMax;
        /// <summary>
        /// 1分钟内重复聊天>=3时，禁言处理 (3)
        /// </summary>
        public static int chatRepeatMax
        {
            get
            {
                if (!b_chatRepeatMax)
                {
                    b_chatRepeatMax = true;
                    s_chatRepeatMax = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(77).value));
                }
                return s_chatRepeatMax;
            }
        }

        private static int s_chatRepeatForbid;
        private static bool b_chatRepeatForbid;
        /// <summary>
        /// 重复聊天禁言时间1分钟 (1)
        /// </summary>
        public static int chatRepeatForbid
        {
            get
            {
                if (!b_chatRepeatForbid)
                {
                    b_chatRepeatForbid = true;
                    s_chatRepeatForbid = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(78).value));
                }
                return s_chatRepeatForbid;
            }
        }

        private static int s_chatRateTime;
        private static bool b_chatRateTime;
        /// <summary>
        /// 1分钟内聊天频率检查 (1)
        /// </summary>
        public static int chatRateTime
        {
            get
            {
                if (!b_chatRateTime)
                {
                    b_chatRateTime = true;
                    s_chatRateTime = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(79).value));
                }
                return s_chatRateTime;
            }
        }

        private static int s_chatRateMax;
        private static bool b_chatRateMax;
        /// <summary>
        /// 1分钟内聊天条数>=5时，禁言处理 (5)
        /// </summary>
        public static int chatRateMax
        {
            get
            {
                if (!b_chatRateMax)
                {
                    b_chatRateMax = true;
                    s_chatRateMax = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(80).value));
                }
                return s_chatRateMax;
            }
        }

        private static int s_chatRateForbid;
        private static bool b_chatRateForbid;
        /// <summary>
        /// 聊天频率禁言1分钟 (1)
        /// </summary>
        public static int chatRateForbid
        {
            get
            {
                if (!b_chatRateForbid)
                {
                    b_chatRateForbid = true;
                    s_chatRateForbid = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(81).value));
                }
                return s_chatRateForbid;
            }
        }

        private static string s_chatKeyWords;
        private static bool b_chatKeyWords;
        /// <summary>
        /// 聊天内容关键词替换 ("**")
        /// </summary>
        public static string chatKeyWords
        {
            get
            {
                if (!b_chatKeyWords)
                {
                    b_chatKeyWords = true;
                    s_chatKeyWords = (string)TableManager.ParseValue("string", (TableManager.instance.GetData<TableConst>(82).value));
                }
                return s_chatKeyWords;
            }
        }

        private static int s_chatNeedLevel;
        private static bool b_chatNeedLevel;
        /// <summary>
        /// 聊天需要玩家等级 (1)
        /// </summary>
        public static int chatNeedLevel
        {
            get
            {
                if (!b_chatNeedLevel)
                {
                    b_chatNeedLevel = true;
                    s_chatNeedLevel = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(83).value));
                }
                return s_chatNeedLevel;
            }
        }

        private static int s_chatMaxChars;
        private static bool b_chatMaxChars;
        /// <summary>
        /// 聊天最大字符数 (200)
        /// </summary>
        public static int chatMaxChars
        {
            get
            {
                if (!b_chatMaxChars)
                {
                    b_chatMaxChars = true;
                    s_chatMaxChars = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(84).value));
                }
                return s_chatMaxChars;
            }
        }

        private static int s_RushPowerRecoverInterval;
        private static bool b_RushPowerRecoverInterval;
        /// <summary>
        /// 体力回复间隔 (60)
        /// </summary>
        public static int RushPowerRecoverInterval
        {
            get
            {
                if (!b_RushPowerRecoverInterval)
                {
                    b_RushPowerRecoverInterval = true;
                    s_RushPowerRecoverInterval = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(85).value));
                }
                return s_RushPowerRecoverInterval;
            }
        }

        private static string s_buttonPosConfigVersion;
        private static bool b_buttonPosConfigVersion;
        /// <summary>
        /// 自定义按钮位置版本号，不同时重置按钮位置。 (0.0.3)
        /// </summary>
        public static string buttonPosConfigVersion
        {
            get
            {
                if (!b_buttonPosConfigVersion)
                {
                    b_buttonPosConfigVersion = true;
                    s_buttonPosConfigVersion = (string)TableManager.ParseValue("string", (TableManager.instance.GetData<TableConst>(86).value));
                }
                return s_buttonPosConfigVersion;
            }
        }

        private static float s_reviveTotalTime;
        private static bool b_reviveTotalTime;
        /// <summary>
        /// 复活所需要的时间 (8)
        /// </summary>
        public static float reviveTotalTime
        {
            get
            {
                if (!b_reviveTotalTime)
                {
                    b_reviveTotalTime = true;
                    s_reviveTotalTime = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(87).value));
                }
                return s_reviveTotalTime;
            }
        }

        private static int s_stageMaxWin;
        private static bool b_stageMaxWin;
        /// <summary>
        /// 战斗匹配时玩家所选职业stage大于上次战斗stage时，上次战斗时的stage累计胜场超过N时职业等级取值blablabla… (1)
        /// </summary>
        public static int stageMaxWin
        {
            get
            {
                if (!b_stageMaxWin)
                {
                    b_stageMaxWin = true;
                    s_stageMaxWin = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(88).value));
                }
                return s_stageMaxWin;
            }
        }

        private static float s_aimLockCritDelayTime;
        private static bool b_aimLockCritDelayTime;
        /// <summary>
        /// 锁定后持续一定时间后，发送暴击消息 (1)
        /// </summary>
        public static float aimLockCritDelayTime
        {
            get
            {
                if (!b_aimLockCritDelayTime)
                {
                    b_aimLockCritDelayTime = true;
                    s_aimLockCritDelayTime = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(89).value));
                }
                return s_aimLockCritDelayTime;
            }
        }

    }
}
