using System;
using UnityEngine;

 namespace RedStone
{
    public class TutorialConfig
    {
        private static Vector3 s_tutorialStartPos;
        private static bool b_tutorialStartPos;
        /// <summary>
        /// 拖动屏幕教学玩家起始位置 ((50.97,0.53,-8.46))
        /// </summary>
        public static Vector3 tutorialStartPos
        {
            get
            {
                if (!b_tutorialStartPos)
                {
                    b_tutorialStartPos = true;
                    s_tutorialStartPos = (Vector3)TableManager.ParseValue("Vector3", (TableManager.instance.GetData<TableConst>(8001).value));
                }
                return s_tutorialStartPos;
            }
        }

        private static Vector3 s_tutorialStartDir;
        private static bool b_tutorialStartDir;
        /// <summary>
        /// 拖动屏幕教学玩家朝向 ((0.01633,0,-0.9998667))
        /// </summary>
        public static Vector3 tutorialStartDir
        {
            get
            {
                if (!b_tutorialStartDir)
                {
                    b_tutorialStartDir = true;
                    s_tutorialStartDir = (Vector3)TableManager.ParseValue("Vector3", (TableManager.instance.GetData<TableConst>(8002).value));
                }
                return s_tutorialStartDir;
            }
        }

        private static Vector3[] s_tutorialDragPoints;
        private static bool b_tutorialDragPoints;
        /// <summary>
        /// 拖动屏幕教学拖动点 ([(51.894,2.33,-11.847),(50.826,1.275,-11.847),(49.256,2.441,-11.847),(50.598,2.101,-11.847)])
        /// </summary>
        public static Vector3[] tutorialDragPoints
        {
            get
            {
                if (!b_tutorialDragPoints)
                {
                    b_tutorialDragPoints = true;
                    s_tutorialDragPoints = (Vector3[])TableManager.ParseValue("Vector3[]", (TableManager.instance.GetData<TableConst>(8003).value));
                }
                return s_tutorialDragPoints;
            }
        }

        private static Vector3 s_tutorialMoveStartDir;
        private static bool b_tutorialMoveStartDir;
        /// <summary>
        /// 移动教学玩家起始朝向 ((0,0.01633,-0.9998667))
        /// </summary>
        public static Vector3 tutorialMoveStartDir
        {
            get
            {
                if (!b_tutorialMoveStartDir)
                {
                    b_tutorialMoveStartDir = true;
                    s_tutorialMoveStartDir = (Vector3)TableManager.ParseValue("Vector3", (TableManager.instance.GetData<TableConst>(8004).value));
                }
                return s_tutorialMoveStartDir;
            }
        }

        private static Vector3[] s_tutorialMovePoints;
        private static bool b_tutorialMovePoints;
        /// <summary>
        /// 移动教学移动点 ([(50.47755,0.4312,-19.9486),(52.50982,0.4311252,-28.65359)])
        /// </summary>
        public static Vector3[] tutorialMovePoints
        {
            get
            {
                if (!b_tutorialMovePoints)
                {
                    b_tutorialMovePoints = true;
                    s_tutorialMovePoints = (Vector3[])TableManager.ParseValue("Vector3[]", (TableManager.instance.GetData<TableConst>(8005).value));
                }
                return s_tutorialMovePoints;
            }
        }

        private static Vector3 s_tutorialFireStartDir;
        private static bool b_tutorialFireStartDir;
        /// <summary>
        /// 射击教学玩家起始朝向 ((0,0.967,12.68))
        /// </summary>
        public static Vector3 tutorialFireStartDir
        {
            get
            {
                if (!b_tutorialFireStartDir)
                {
                    b_tutorialFireStartDir = true;
                    s_tutorialFireStartDir = (Vector3)TableManager.ParseValue("Vector3", (TableManager.instance.GetData<TableConst>(8006).value));
                }
                return s_tutorialFireStartDir;
            }
        }

        private static Vector3 s_tutorialSprintStartDir;
        private static bool b_tutorialSprintStartDir;
        /// <summary>
        /// 冲刺教学玩家起始朝向 ((0.0, 0.0, 1.0))
        /// </summary>
        public static Vector3 tutorialSprintStartDir
        {
            get
            {
                if (!b_tutorialSprintStartDir)
                {
                    b_tutorialSprintStartDir = true;
                    s_tutorialSprintStartDir = (Vector3)TableManager.ParseValue("Vector3", (TableManager.instance.GetData<TableConst>(8007).value));
                }
                return s_tutorialSprintStartDir;
            }
        }

        private static Vector3 s_tutorialSprintPoint;
        private static bool b_tutorialSprintPoint;
        /// <summary>
        /// 冲刺教学目标点 ((34.68085,-0.15275,-28.5477))
        /// </summary>
        public static Vector3 tutorialSprintPoint
        {
            get
            {
                if (!b_tutorialSprintPoint)
                {
                    b_tutorialSprintPoint = true;
                    s_tutorialSprintPoint = (Vector3)TableManager.ParseValue("Vector3", (TableManager.instance.GetData<TableConst>(8008).value));
                }
                return s_tutorialSprintPoint;
            }
        }

        private static Vector3 s_tutorialSightStartDir;
        private static bool b_tutorialSightStartDir;
        /// <summary>
        /// 使用瞄准镜教学玩家起始朝向 ((-5.28,1.47,-36.1))
        /// </summary>
        public static Vector3 tutorialSightStartDir
        {
            get
            {
                if (!b_tutorialSightStartDir)
                {
                    b_tutorialSightStartDir = true;
                    s_tutorialSightStartDir = (Vector3)TableManager.ParseValue("Vector3", (TableManager.instance.GetData<TableConst>(8009).value));
                }
                return s_tutorialSightStartDir;
            }
        }

    }
}
