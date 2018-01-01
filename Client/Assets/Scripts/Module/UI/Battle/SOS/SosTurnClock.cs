using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedStone.UI;

namespace RedStone
{
    public class SosTurnClock : MonoBehaviour
    {
        public Text countDownText;
        private float m_countDown = 0;

        public void Reset()
        {
            //TODO:CONST TIME
            m_countDown = 35;
            lastCD = 0;
        }

        int lastCD = 0;
        private void Update()
        {
            m_countDown = Mathf.Max(0, m_countDown - Time.deltaTime);
            int cur = (int)m_countDown;
            if (lastCD != cur)
            {
                countDownText.text = cur.ToString();
                lastCD = cur;
            }
        }
    }
}