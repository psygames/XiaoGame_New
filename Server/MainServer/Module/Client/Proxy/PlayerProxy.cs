using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace RedStone
{
    public class PlayerProxy : MCProxyBase
    {
        public Dictionary<long, PlayerData> playerDict = new Dictionary<long, PlayerData>();
        public Dictionary<long, long> sessionPlayerDict = new Dictionary<long, long>();

        public long GetPlayerID(long sessionID)
        {
            return sessionPlayerDict[sessionID];
        }

        public PlayerData GetPlayer(long playerID)
        {
            PlayerData data;
            if (playerDict.TryGetValue(playerID, out data))
            {
                return data;
            }
            return null;
        }

        public long GetSessionID(long playerID)
        {
            return sessionPlayerDict.First(a => { return a.Value == playerID; }).Key;
        }

        public override void OnInit()
        {
            base.OnInit();

        }

        public void OnPlayerForceQuit(long pid)
        {
        }
    }
}
