using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Message;
using RedStone.Data;


namespace RedStone
{
    // TODO: GAME MODE, GAME ID, GROUP
    public class MatchPoolProxy : MCProxyBase
    {
        const int NEED_PLAYERS = 4;
        const int WAIT_TIMES = 20;

        public override void OnInit()
        {
            base.OnInit();
        }

        public List<long> m_users = new List<long>();
        private float m_waitTime = 0;

        public void Add(long uid, int gameID, int gameMode, int groupID)
        {
            m_waitTime = WAIT_TIMES;
            if (!m_users.Contains(uid))
                m_users.Add(uid);
            else
                Debug.LogError($"duplicated match user : {uid}");
        }

        public void Remove(long uid)
        {
            if (m_users.Contains(uid))
                m_users.Remove(uid);
        }

        public List<long> GetMatched()
        {
            if (m_users.Count >= NEED_PLAYERS
                || m_users.Count > 0 && m_waitTime <= 0)
            {
                List<long> result = new List<long>();
                while (result.Count < NEED_PLAYERS
                    && m_users.Count > 0)
                {
                    result.Add(m_users[0]);
                    m_users.RemoveAt(0);
                }
                return result;
            }
            return null;
        }

        //TODO: Use check
        private void CheckMatch()
        {

        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            m_waitTime -= Time.deltaTime;

            CheckMatch();
        }
    }
}
