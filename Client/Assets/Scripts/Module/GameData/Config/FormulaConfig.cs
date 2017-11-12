using System;
using UnityEngine;

 namespace RedStone
{
    public class FormulaConfig
    {
        private static float s_defRatio;
        private static bool b_defRatio;
        /// <summary>
        /// 伤害公式中的 防御*X中的X (1)
        /// </summary>
        public static float defRatio
        {
            get
            {
                if (!b_defRatio)
                {
                    b_defRatio = true;
                    s_defRatio = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1000).value));
                }
                return s_defRatio;
            }
        }

        private static float s_lvRatio;
        private static bool b_lvRatio;
        /// <summary>
        /// 伤害公式中的等级乘以的系数 (48)
        /// </summary>
        public static float lvRatio
        {
            get
            {
                if (!b_lvRatio)
                {
                    b_lvRatio = true;
                    s_lvRatio = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1001).value));
                }
                return s_lvRatio;
            }
        }

        private static float s_adjustRatio;
        private static bool b_adjustRatio;
        /// <summary>
        /// 伤害公式中的调节常量 (250)
        /// </summary>
        public static float adjustRatio
        {
            get
            {
                if (!b_adjustRatio)
                {
                    b_adjustRatio = true;
                    s_adjustRatio = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1002).value));
                }
                return s_adjustRatio;
            }
        }

        private static float s_defenseMax;
        private static bool b_defenseMax;
        /// <summary>
        /// 最大的免伤率 (0.85)
        /// </summary>
        public static float defenseMax
        {
            get
            {
                if (!b_defenseMax)
                {
                    b_defenseMax = true;
                    s_defenseMax = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1003).value));
                }
                return s_defenseMax;
            }
        }

        private static float s_initialHit;
        private static bool b_initialHit;
        /// <summary>
        /// 枪械初始命中率 (0.25)
        /// </summary>
        public static float initialHit
        {
            get
            {
                if (!b_initialHit)
                {
                    b_initialHit = true;
                    s_initialHit = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1004).value));
                }
                return s_initialHit;
            }
        }

        private static float s_hitRatio;
        private static bool b_hitRatio;
        /// <summary>
        /// 命中公式中的命中*x中的x (1.65)
        /// </summary>
        public static float hitRatio
        {
            get
            {
                if (!b_hitRatio)
                {
                    b_hitRatio = true;
                    s_hitRatio = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1005).value));
                }
                return s_hitRatio;
            }
        }

        private static float s_hitLvRatio;
        private static bool b_hitLvRatio;
        /// <summary>
        /// 命中公式中的等级乘以的系数 (25)
        /// </summary>
        public static float hitLvRatio
        {
            get
            {
                if (!b_hitLvRatio)
                {
                    b_hitLvRatio = true;
                    s_hitLvRatio = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1006).value));
                }
                return s_hitLvRatio;
            }
        }

        private static float s_hitAdjustRatio;
        private static bool b_hitAdjustRatio;
        /// <summary>
        /// 命中公式中的调节常量 (200)
        /// </summary>
        public static float hitAdjustRatio
        {
            get
            {
                if (!b_hitAdjustRatio)
                {
                    b_hitAdjustRatio = true;
                    s_hitAdjustRatio = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1007).value));
                }
                return s_hitAdjustRatio;
            }
        }

        private static float s_hitMax;
        private static bool b_hitMax;
        /// <summary>
        /// 允许的最大命中 (2)
        /// </summary>
        public static float hitMax
        {
            get
            {
                if (!b_hitMax)
                {
                    b_hitMax = true;
                    s_hitMax = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1008).value));
                }
                return s_hitMax;
            }
        }

        private static float s_hitMin;
        private static bool b_hitMin;
        /// <summary>
        /// 允许的最小命中 (0.25)
        /// </summary>
        public static float hitMin
        {
            get
            {
                if (!b_hitMin)
                {
                    b_hitMin = true;
                    s_hitMin = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1009).value));
                }
                return s_hitMin;
            }
        }

        private static float s_initialCrit;
        private static bool b_initialCrit;
        /// <summary>
        /// 枪械初始暴击率 (0)
        /// </summary>
        public static float initialCrit
        {
            get
            {
                if (!b_initialCrit)
                {
                    b_initialCrit = true;
                    s_initialCrit = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1010).value));
                }
                return s_initialCrit;
            }
        }

        private static float s_critRatio;
        private static bool b_critRatio;
        /// <summary>
        /// 暴击公式中的暴击*x中的x (1)
        /// </summary>
        public static float critRatio
        {
            get
            {
                if (!b_critRatio)
                {
                    b_critRatio = true;
                    s_critRatio = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1011).value));
                }
                return s_critRatio;
            }
        }

        private static float s_critLvRatio;
        private static bool b_critLvRatio;
        /// <summary>
        /// 暴击公式中的等级乘以的系数 (80)
        /// </summary>
        public static float critLvRatio
        {
            get
            {
                if (!b_critLvRatio)
                {
                    b_critLvRatio = true;
                    s_critLvRatio = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1012).value));
                }
                return s_critLvRatio;
            }
        }

        private static float s_critAdjustRatio;
        private static bool b_critAdjustRatio;
        /// <summary>
        /// 暴击公式中的调节常量 (500)
        /// </summary>
        public static float critAdjustRatio
        {
            get
            {
                if (!b_critAdjustRatio)
                {
                    b_critAdjustRatio = true;
                    s_critAdjustRatio = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1013).value));
                }
                return s_critAdjustRatio;
            }
        }

        private static float s_critMax;
        private static bool b_critMax;
        /// <summary>
        /// 允许的最大暴击 (1)
        /// </summary>
        public static float critMax
        {
            get
            {
                if (!b_critMax)
                {
                    b_critMax = true;
                    s_critMax = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1014).value));
                }
                return s_critMax;
            }
        }

        private static float s_critMin;
        private static bool b_critMin;
        /// <summary>
        /// 允许的最小暴击 (0)
        /// </summary>
        public static float critMin
        {
            get
            {
                if (!b_critMin)
                {
                    b_critMin = true;
                    s_critMin = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1015).value));
                }
                return s_critMin;
            }
        }

        private static float s_initialCritDamage;
        private static bool b_initialCritDamage;
        /// <summary>
        /// 枪械初始暴击伤害率 (0)
        /// </summary>
        public static float initialCritDamage
        {
            get
            {
                if (!b_initialCritDamage)
                {
                    b_initialCritDamage = true;
                    s_initialCritDamage = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1016).value));
                }
                return s_initialCritDamage;
            }
        }

        private static float s_critDamageRatio;
        private static bool b_critDamageRatio;
        /// <summary>
        /// 暴伤公式中的暴伤*x中的x (10)
        /// </summary>
        public static float critDamageRatio
        {
            get
            {
                if (!b_critDamageRatio)
                {
                    b_critDamageRatio = true;
                    s_critDamageRatio = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1017).value));
                }
                return s_critDamageRatio;
            }
        }

        private static float s_critDamageLvRatio;
        private static bool b_critDamageLvRatio;
        /// <summary>
        /// 暴伤公式中的等级乘以的系数 (40)
        /// </summary>
        public static float critDamageLvRatio
        {
            get
            {
                if (!b_critDamageLvRatio)
                {
                    b_critDamageLvRatio = true;
                    s_critDamageLvRatio = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1018).value));
                }
                return s_critDamageLvRatio;
            }
        }

        private static float s_critDamageAdjustRatio;
        private static bool b_critDamageAdjustRatio;
        /// <summary>
        /// 暴伤公式中的调节常量 (350)
        /// </summary>
        public static float critDamageAdjustRatio
        {
            get
            {
                if (!b_critDamageAdjustRatio)
                {
                    b_critDamageAdjustRatio = true;
                    s_critDamageAdjustRatio = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1019).value));
                }
                return s_critDamageAdjustRatio;
            }
        }

        private static float s_critDamageMax;
        private static bool b_critDamageMax;
        /// <summary>
        /// 允许的最大暴伤率 (4.5)
        /// </summary>
        public static float critDamageMax
        {
            get
            {
                if (!b_critDamageMax)
                {
                    b_critDamageMax = true;
                    s_critDamageMax = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1020).value));
                }
                return s_critDamageMax;
            }
        }

        private static float s_critDamageMin;
        private static bool b_critDamageMin;
        /// <summary>
        /// 允许的最小暴伤率 (1.2)
        /// </summary>
        public static float critDamageMin
        {
            get
            {
                if (!b_critDamageMin)
                {
                    b_critDamageMin = true;
                    s_critDamageMin = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1021).value));
                }
                return s_critDamageMin;
            }
        }

        private static float s_gunBattleRatio;
        private static bool b_gunBattleRatio;
        /// <summary>
        /// 枪械战斗力系数 (0.8)
        /// </summary>
        public static float gunBattleRatio
        {
            get
            {
                if (!b_gunBattleRatio)
                {
                    b_gunBattleRatio = true;
                    s_gunBattleRatio = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1022).value));
                }
                return s_gunBattleRatio;
            }
        }

        private static float s_shieldReplyRatio;
        private static bool b_shieldReplyRatio;
        /// <summary>
        /// 护盾回复速度计算系数 (0.8)
        /// </summary>
        public static float shieldReplyRatio
        {
            get
            {
                if (!b_shieldReplyRatio)
                {
                    b_shieldReplyRatio = true;
                    s_shieldReplyRatio = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1023).value));
                }
                return s_shieldReplyRatio;
            }
        }

        private static float s_professionBattleRatio;
        private static bool b_professionBattleRatio;
        /// <summary>
        /// 职业战斗力系数 (1)
        /// </summary>
        public static float professionBattleRatio
        {
            get
            {
                if (!b_professionBattleRatio)
                {
                    b_professionBattleRatio = true;
                    s_professionBattleRatio = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1024).value));
                }
                return s_professionBattleRatio;
            }
        }

        private static float s_battleAdjustRatio;
        private static bool b_battleAdjustRatio;
        /// <summary>
        /// 因为改成了等级匹配，所以这个值改成1了，只有匹配再用 (1)
        /// </summary>
        public static float battleAdjustRatio
        {
            get
            {
                if (!b_battleAdjustRatio)
                {
                    b_battleAdjustRatio = true;
                    s_battleAdjustRatio = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1025).value));
                }
                return s_battleAdjustRatio;
            }
        }

        private static float s_variationStandard;
        private static bool b_variationStandard;
        /// <summary>
        /// 变异系数进行放大的标准值 (0.35)
        /// </summary>
        public static float variationStandard
        {
            get
            {
                if (!b_variationStandard)
                {
                    b_variationStandard = true;
                    s_variationStandard = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1026).value));
                }
                return s_variationStandard;
            }
        }

        private static float s_varianceRatio;
        private static bool b_varianceRatio;
        /// <summary>
        /// 方差过大时对战斗力的修正计算系数 (10)
        /// </summary>
        public static float varianceRatio
        {
            get
            {
                if (!b_varianceRatio)
                {
                    b_varianceRatio = true;
                    s_varianceRatio = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1027).value));
                }
                return s_varianceRatio;
            }
        }

        private static float s_teamRatio;
        private static bool b_teamRatio;
        /// <summary>
        /// 组队人数的匹配系数 (0.4)
        /// </summary>
        public static float teamRatio
        {
            get
            {
                if (!b_teamRatio)
                {
                    b_teamRatio = true;
                    s_teamRatio = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1028).value));
                }
                return s_teamRatio;
            }
        }

        private static float s_teamMax;
        private static bool b_teamMax;
        /// <summary>
        /// 最大队伍人数 (6)
        /// </summary>
        public static float teamMax
        {
            get
            {
                if (!b_teamMax)
                {
                    b_teamMax = true;
                    s_teamMax = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1029).value));
                }
                return s_teamMax;
            }
        }

        private static float s_powerFactorAttack;
        private static bool b_powerFactorAttack;
        /// <summary>
        /// 枪械power计算公式中Attack影响的系数 (1)
        /// </summary>
        public static float powerFactorAttack
        {
            get
            {
                if (!b_powerFactorAttack)
                {
                    b_powerFactorAttack = true;
                    s_powerFactorAttack = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1030).value));
                }
                return s_powerFactorAttack;
            }
        }

        private static float s_powerFactorHitValue;
        private static bool b_powerFactorHitValue;
        /// <summary>
        /// 枪械Power计算公式中hitValue影响的系数 (0.56000000000000005)
        /// </summary>
        public static float powerFactorHitValue
        {
            get
            {
                if (!b_powerFactorHitValue)
                {
                    b_powerFactorHitValue = true;
                    s_powerFactorHitValue = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1031).value));
                }
                return s_powerFactorHitValue;
            }
        }

        private static float s_powerFactorFireRate;
        private static bool b_powerFactorFireRate;
        /// <summary>
        /// 枪械power计算公式中FireRate影响的系数 (4.5)
        /// </summary>
        public static float powerFactorFireRate
        {
            get
            {
                if (!b_powerFactorFireRate)
                {
                    b_powerFactorFireRate = true;
                    s_powerFactorFireRate = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1032).value));
                }
                return s_powerFactorFireRate;
            }
        }

        private static float s_powerFactorCritValue;
        private static bool b_powerFactorCritValue;
        /// <summary>
        /// 枪械power计算公式中CritValue影响的系数 (0.9)
        /// </summary>
        public static float powerFactorCritValue
        {
            get
            {
                if (!b_powerFactorCritValue)
                {
                    b_powerFactorCritValue = true;
                    s_powerFactorCritValue = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1033).value));
                }
                return s_powerFactorCritValue;
            }
        }

        private static float s_powerFactorCritDamage;
        private static bool b_powerFactorCritDamage;
        /// <summary>
        /// 枪械power计算公式中CritDamage影响的系数 (0.78)
        /// </summary>
        public static float powerFactorCritDamage
        {
            get
            {
                if (!b_powerFactorCritDamage)
                {
                    b_powerFactorCritDamage = true;
                    s_powerFactorCritDamage = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1034).value));
                }
                return s_powerFactorCritDamage;
            }
        }

        private static float s_powerFactorHp;
        private static bool b_powerFactorHp;
        /// <summary>
        /// 职业power计算公式中HP影响的系数 (0.22)
        /// </summary>
        public static float powerFactorHp
        {
            get
            {
                if (!b_powerFactorHp)
                {
                    b_powerFactorHp = true;
                    s_powerFactorHp = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1035).value));
                }
                return s_powerFactorHp;
            }
        }

        private static float s_powerFactorDefense;
        private static bool b_powerFactorDefense;
        /// <summary>
        /// 职业power计算公式中Defense影响的系数 (1.1200000000000001)
        /// </summary>
        public static float powerFactorDefense
        {
            get
            {
                if (!b_powerFactorDefense)
                {
                    b_powerFactorDefense = true;
                    s_powerFactorDefense = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1036).value));
                }
                return s_powerFactorDefense;
            }
        }

        private static float s_powerFactorHpRecover;
        private static bool b_powerFactorHpRecover;
        /// <summary>
        /// 职业power计算公式中HpRecover影响的系数 (0.85)
        /// </summary>
        public static float powerFactorHpRecover
        {
            get
            {
                if (!b_powerFactorHpRecover)
                {
                    b_powerFactorHpRecover = true;
                    s_powerFactorHpRecover = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1037).value));
                }
                return s_powerFactorHpRecover;
            }
        }

        private static float s_battleAdjustRatio2;
        private static bool b_battleAdjustRatio2;
        /// <summary>
        /// 显示和服务器那边用到的调节战力显示值的系数 (300)
        /// </summary>
        public static float battleAdjustRatio2
        {
            get
            {
                if (!b_battleAdjustRatio2)
                {
                    b_battleAdjustRatio2 = true;
                    s_battleAdjustRatio2 = (float)TableManager.ParseValue("float", (TableManager.instance.GetData<TableConst>(1038).value));
                }
                return s_battleAdjustRatio2;
            }
        }

    }
}
