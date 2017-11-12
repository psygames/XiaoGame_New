using System;
using UnityEngine;

 namespace RedStone
{
    public class LockConfig
    {
        private static bool s_lockHomeShop;
        private static bool b_lockHomeShop;
        /// <summary>
        /// 无 (true)
        /// </summary>
        public static bool lockHomeShop
        {
            get
            {
                if (!b_lockHomeShop)
                {
                    b_lockHomeShop = true;
                    s_lockHomeShop = (bool)TableManager.ParseValue("bool", (TableManager.instance.GetData<TableConst>(7001).value));
                }
                return s_lockHomeShop;
            }
        }

        private static bool s_lockHomeMission;
        private static bool b_lockHomeMission;
        /// <summary>
        /// 无 (true)
        /// </summary>
        public static bool lockHomeMission
        {
            get
            {
                if (!b_lockHomeMission)
                {
                    b_lockHomeMission = true;
                    s_lockHomeMission = (bool)TableManager.ParseValue("bool", (TableManager.instance.GetData<TableConst>(7002).value));
                }
                return s_lockHomeMission;
            }
        }

        private static bool s_lockHomeMail;
        private static bool b_lockHomeMail;
        /// <summary>
        /// 无 (false)
        /// </summary>
        public static bool lockHomeMail
        {
            get
            {
                if (!b_lockHomeMail)
                {
                    b_lockHomeMail = true;
                    s_lockHomeMail = (bool)TableManager.ParseValue("bool", (TableManager.instance.GetData<TableConst>(7003).value));
                }
                return s_lockHomeMail;
            }
        }

        private static bool s_lockHomeLegion;
        private static bool b_lockHomeLegion;
        /// <summary>
        /// 无 (true)
        /// </summary>
        public static bool lockHomeLegion
        {
            get
            {
                if (!b_lockHomeLegion)
                {
                    b_lockHomeLegion = true;
                    s_lockHomeLegion = (bool)TableManager.ParseValue("bool", (TableManager.instance.GetData<TableConst>(7004).value));
                }
                return s_lockHomeLegion;
            }
        }

        private static bool s_lockHomeGift;
        private static bool b_lockHomeGift;
        /// <summary>
        /// 无 (true)
        /// </summary>
        public static bool lockHomeGift
        {
            get
            {
                if (!b_lockHomeGift)
                {
                    b_lockHomeGift = true;
                    s_lockHomeGift = (bool)TableManager.ParseValue("bool", (TableManager.instance.GetData<TableConst>(7005).value));
                }
                return s_lockHomeGift;
            }
        }

        private static bool s_lockHomeSetting;
        private static bool b_lockHomeSetting;
        /// <summary>
        /// 无 (false)
        /// </summary>
        public static bool lockHomeSetting
        {
            get
            {
                if (!b_lockHomeSetting)
                {
                    b_lockHomeSetting = true;
                    s_lockHomeSetting = (bool)TableManager.ParseValue("bool", (TableManager.instance.GetData<TableConst>(7006).value));
                }
                return s_lockHomeSetting;
            }
        }

        private static bool s_lockShopPageFund;
        private static bool b_lockShopPageFund;
        /// <summary>
        /// 无 (true)
        /// </summary>
        public static bool lockShopPageFund
        {
            get
            {
                if (!b_lockShopPageFund)
                {
                    b_lockShopPageFund = true;
                    s_lockShopPageFund = (bool)TableManager.ParseValue("bool", (TableManager.instance.GetData<TableConst>(7007).value));
                }
                return s_lockShopPageFund;
            }
        }

        private static bool s_lockShopPageDiamond;
        private static bool b_lockShopPageDiamond;
        /// <summary>
        /// 无 (true)
        /// </summary>
        public static bool lockShopPageDiamond
        {
            get
            {
                if (!b_lockShopPageDiamond)
                {
                    b_lockShopPageDiamond = true;
                    s_lockShopPageDiamond = (bool)TableManager.ParseValue("bool", (TableManager.instance.GetData<TableConst>(7008).value));
                }
                return s_lockShopPageDiamond;
            }
        }

        private static bool s_lockShopPageCommon;
        private static bool b_lockShopPageCommon;
        /// <summary>
        /// 无 (false)
        /// </summary>
        public static bool lockShopPageCommon
        {
            get
            {
                if (!b_lockShopPageCommon)
                {
                    b_lockShopPageCommon = true;
                    s_lockShopPageCommon = (bool)TableManager.ParseValue("bool", (TableManager.instance.GetData<TableConst>(7009).value));
                }
                return s_lockShopPageCommon;
            }
        }

        private static bool s_lockShopPageBlack;
        private static bool b_lockShopPageBlack;
        /// <summary>
        /// 无 (true)
        /// </summary>
        public static bool lockShopPageBlack
        {
            get
            {
                if (!b_lockShopPageBlack)
                {
                    b_lockShopPageBlack = true;
                    s_lockShopPageBlack = (bool)TableManager.ParseValue("bool", (TableManager.instance.GetData<TableConst>(7010).value));
                }
                return s_lockShopPageBlack;
            }
        }

        private static bool s_lockShopPageCompetition;
        private static bool b_lockShopPageCompetition;
        /// <summary>
        /// 无 (true)
        /// </summary>
        public static bool lockShopPageCompetition
        {
            get
            {
                if (!b_lockShopPageCompetition)
                {
                    b_lockShopPageCompetition = true;
                    s_lockShopPageCompetition = (bool)TableManager.ParseValue("bool", (TableManager.instance.GetData<TableConst>(7011).value));
                }
                return s_lockShopPageCompetition;
            }
        }

        private static bool s_lockShopCurrency;
        private static bool b_lockShopCurrency;
        /// <summary>
        /// 货币商店 (true)
        /// </summary>
        public static bool lockShopCurrency
        {
            get
            {
                if (!b_lockShopCurrency)
                {
                    b_lockShopCurrency = true;
                    s_lockShopCurrency = (bool)TableManager.ParseValue("bool", (TableManager.instance.GetData<TableConst>(7012).value));
                }
                return s_lockShopCurrency;
            }
        }

        private static bool s_lockShopGoods;
        private static bool b_lockShopGoods;
        /// <summary>
        /// 物品商店 (true)
        /// </summary>
        public static bool lockShopGoods
        {
            get
            {
                if (!b_lockShopGoods)
                {
                    b_lockShopGoods = true;
                    s_lockShopGoods = (bool)TableManager.ParseValue("bool", (TableManager.instance.GetData<TableConst>(7013).value));
                }
                return s_lockShopGoods;
            }
        }

        private static bool s_lockProfessionPromote;
        private static bool b_lockProfessionPromote;
        /// <summary>
        /// 无 (true)
        /// </summary>
        public static bool lockProfessionPromote
        {
            get
            {
                if (!b_lockProfessionPromote)
                {
                    b_lockProfessionPromote = true;
                    s_lockProfessionPromote = (bool)TableManager.ParseValue("bool", (TableManager.instance.GetData<TableConst>(7014).value));
                }
                return s_lockProfessionPromote;
            }
        }

        private static bool s_lockGunPromote;
        private static bool b_lockGunPromote;
        /// <summary>
        /// 无 (true)
        /// </summary>
        public static bool lockGunPromote
        {
            get
            {
                if (!b_lockGunPromote)
                {
                    b_lockGunPromote = true;
                    s_lockGunPromote = (bool)TableManager.ParseValue("bool", (TableManager.instance.GetData<TableConst>(7015).value));
                }
                return s_lockGunPromote;
            }
        }

        private static bool s_lockGunPartInstall;
        private static bool b_lockGunPartInstall;
        /// <summary>
        /// 无 (true)
        /// </summary>
        public static bool lockGunPartInstall
        {
            get
            {
                if (!b_lockGunPartInstall)
                {
                    b_lockGunPartInstall = true;
                    s_lockGunPartInstall = (bool)TableManager.ParseValue("bool", (TableManager.instance.GetData<TableConst>(7016).value));
                }
                return s_lockGunPartInstall;
            }
        }

        private static bool s_lockGunUpgrade;
        private static bool b_lockGunUpgrade;
        /// <summary>
        /// 无 (true)
        /// </summary>
        public static bool lockGunUpgrade
        {
            get
            {
                if (!b_lockGunUpgrade)
                {
                    b_lockGunUpgrade = true;
                    s_lockGunUpgrade = (bool)TableManager.ParseValue("bool", (TableManager.instance.GetData<TableConst>(7017).value));
                }
                return s_lockGunUpgrade;
            }
        }

        private static bool s_lockGunTraitUpgrade;
        private static bool b_lockGunTraitUpgrade;
        /// <summary>
        /// 无 (true)
        /// </summary>
        public static bool lockGunTraitUpgrade
        {
            get
            {
                if (!b_lockGunTraitUpgrade)
                {
                    b_lockGunTraitUpgrade = true;
                    s_lockGunTraitUpgrade = (bool)TableManager.ParseValue("bool", (TableManager.instance.GetData<TableConst>(7018).value));
                }
                return s_lockGunTraitUpgrade;
            }
        }

        private static bool s_lockSkillUpgrade;
        private static bool b_lockSkillUpgrade;
        /// <summary>
        /// 无 (true)
        /// </summary>
        public static bool lockSkillUpgrade
        {
            get
            {
                if (!b_lockSkillUpgrade)
                {
                    b_lockSkillUpgrade = true;
                    s_lockSkillUpgrade = (bool)TableManager.ParseValue("bool", (TableManager.instance.GetData<TableConst>(7019).value));
                }
                return s_lockSkillUpgrade;
            }
        }

        private static bool s_lockSkillPromote;
        private static bool b_lockSkillPromote;
        /// <summary>
        /// 无 (true)
        /// </summary>
        public static bool lockSkillPromote
        {
            get
            {
                if (!b_lockSkillPromote)
                {
                    b_lockSkillPromote = true;
                    s_lockSkillPromote = (bool)TableManager.ParseValue("bool", (TableManager.instance.GetData<TableConst>(7020).value));
                }
                return s_lockSkillPromote;
            }
        }

        private static bool s_lockGunDecompose;
        private static bool b_lockGunDecompose;
        /// <summary>
        /// 无 (true)
        /// </summary>
        public static bool lockGunDecompose
        {
            get
            {
                if (!b_lockGunDecompose)
                {
                    b_lockGunDecompose = true;
                    s_lockGunDecompose = (bool)TableManager.ParseValue("bool", (TableManager.instance.GetData<TableConst>(7021).value));
                }
                return s_lockGunDecompose;
            }
        }

        private static bool s_lockGunCompare;
        private static bool b_lockGunCompare;
        /// <summary>
        /// 无 (true)
        /// </summary>
        public static bool lockGunCompare
        {
            get
            {
                if (!b_lockGunCompare)
                {
                    b_lockGunCompare = true;
                    s_lockGunCompare = (bool)TableManager.ParseValue("bool", (TableManager.instance.GetData<TableConst>(7022).value));
                }
                return s_lockGunCompare;
            }
        }

        private static bool s_lockGunEquip;
        private static bool b_lockGunEquip;
        /// <summary>
        /// 无 (false)
        /// </summary>
        public static bool lockGunEquip
        {
            get
            {
                if (!b_lockGunEquip)
                {
                    b_lockGunEquip = true;
                    s_lockGunEquip = (bool)TableManager.ParseValue("bool", (TableManager.instance.GetData<TableConst>(7023).value));
                }
                return s_lockGunEquip;
            }
        }

        private static bool s_lockProfessionUpgrade;
        private static bool b_lockProfessionUpgrade;
        /// <summary>
        /// 无 (true)
        /// </summary>
        public static bool lockProfessionUpgrade
        {
            get
            {
                if (!b_lockProfessionUpgrade)
                {
                    b_lockProfessionUpgrade = true;
                    s_lockProfessionUpgrade = (bool)TableManager.ParseValue("bool", (TableManager.instance.GetData<TableConst>(7024).value));
                }
                return s_lockProfessionUpgrade;
            }
        }

        private static bool s_lockQuickPlaySeason;
        private static bool b_lockQuickPlaySeason;
        /// <summary>
        /// 无 (false)
        /// </summary>
        public static bool lockQuickPlaySeason
        {
            get
            {
                if (!b_lockQuickPlaySeason)
                {
                    b_lockQuickPlaySeason = true;
                    s_lockQuickPlaySeason = (bool)TableManager.ParseValue("bool", (TableManager.instance.GetData<TableConst>(7025).value));
                }
                return s_lockQuickPlaySeason;
            }
        }

        private static bool s_lockQuickPlayDispatchMission;
        private static bool b_lockQuickPlayDispatchMission;
        /// <summary>
        /// 无 (true)
        /// </summary>
        public static bool lockQuickPlayDispatchMission
        {
            get
            {
                if (!b_lockQuickPlayDispatchMission)
                {
                    b_lockQuickPlayDispatchMission = true;
                    s_lockQuickPlayDispatchMission = (bool)TableManager.ParseValue("bool", (TableManager.instance.GetData<TableConst>(7026).value));
                }
                return s_lockQuickPlayDispatchMission;
            }
        }

    }
}
