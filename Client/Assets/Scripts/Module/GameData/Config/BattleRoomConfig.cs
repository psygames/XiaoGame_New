using System;
using UnityEngine;

 namespace RedStone
{
    public class BattleRoomConfig
    {
        private static int s_battleWaitLoadTime;
        private static bool b_battleWaitLoadTime;
        /// <summary>
        /// 战斗开始前等待加载时间 (10)
        /// </summary>
        public static int battleWaitLoadTime
        {
            get
            {
                if (!b_battleWaitLoadTime)
                {
                    b_battleWaitLoadTime = true;
                    s_battleWaitLoadTime = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(5001).value));
                }
                return s_battleWaitLoadTime;
            }
        }

        private static int s_battleReadyBeginCountdown;
        private static bool b_battleReadyBeginCountdown;
        /// <summary>
        /// 战斗开始倒计时 (3)
        /// </summary>
        public static int battleReadyBeginCountdown
        {
            get
            {
                if (!b_battleReadyBeginCountdown)
                {
                    b_battleReadyBeginCountdown = true;
                    s_battleReadyBeginCountdown = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(5002).value));
                }
                return s_battleReadyBeginCountdown;
            }
        }

        private static int s_deadReduceResourcePointNormal;
        private static bool b_deadReduceResourcePointNormal;
        /// <summary>
        /// 正常情况下资源模式死亡掉落分数 (20)
        /// </summary>
        public static int deadReduceResourcePointNormal
        {
            get
            {
                if (!b_deadReduceResourcePointNormal)
                {
                    b_deadReduceResourcePointNormal = true;
                    s_deadReduceResourcePointNormal = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(5003).value));
                }
                return s_deadReduceResourcePointNormal;
            }
        }

        private static int s_deadReduceResourcePointRecover;
        private static bool b_deadReduceResourcePointRecover;
        /// <summary>
        /// 回收状态时资源模式死亡掉落分数 (5)
        /// </summary>
        public static int deadReduceResourcePointRecover
        {
            get
            {
                if (!b_deadReduceResourcePointRecover)
                {
                    b_deadReduceResourcePointRecover = true;
                    s_deadReduceResourcePointRecover = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(5004).value));
                }
                return s_deadReduceResourcePointRecover;
            }
        }

        private static int s_summonedMaxStayTime;
        private static bool b_summonedMaxStayTime;
        /// <summary>
        /// 召唤物最大停留时间，单位ms (60000)
        /// </summary>
        public static int summonedMaxStayTime
        {
            get
            {
                if (!b_summonedMaxStayTime)
                {
                    b_summonedMaxStayTime = true;
                    s_summonedMaxStayTime = (int)TableManager.ParseValue("int", (TableManager.instance.GetData<TableConst>(5005).value));
                }
                return s_summonedMaxStayTime;
            }
        }

    }
}
