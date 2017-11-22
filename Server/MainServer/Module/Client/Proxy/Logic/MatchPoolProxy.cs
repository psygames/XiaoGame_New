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
        public override void OnInit()
        {
            base.OnInit();
        }

        public List<long> m_users = new List<long>();

        public void Add(long uid, int gameID, int gameMode, int groupID)
        {
            if (!m_users.Contains(uid))
                m_users.Add(uid);
            else
                Debug.LogError($"duplicated match user : {uid}");
        }

        public List<long> GetMatched()
        {
            // 3 Players for Peasants vs Landlord 
            int needSize = 1;
            if (m_users.Count >= needSize)
            {
                List<long> result = new List<long>();
                while (result.Count < needSize)
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

            CheckMatch();
        }
    }
}
