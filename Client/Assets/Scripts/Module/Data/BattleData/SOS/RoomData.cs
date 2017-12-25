using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedStone.Data.SOS
{
    public class RoomData
    {
        public int id { get; private set; }
        public string name { get; private set; }
        public State state { get; private set; }

        private List<PlayerData> m_players = new List<PlayerData>();
        public PlayerData whosTurn { get; private set; }

        public void SetData(Message.BattleRoomInfo info)
        {
            id = info.Id;
            name = info.Name;
        }

        public void RoomSync(Message.CBRoomSync sync)
        {
            whosTurn = m_players.First(a => a.id == sync.WhoseTurn);
            state = (State)sync.State;
        }

        public void SetPlayers(IList<Message.BattlePlayerInfo> infos)
        {
            m_players.Clear();
            foreach (var info in infos)
            {
                PlayerData data = new PlayerData();
                data.SetData(info);
                m_players.Add(data);
            }
        }

        public PlayerData GetPlayer(int id)
        {
            return m_players.First(a => a.id == id);
        }


        public enum State
        {
            WaitJoin,
            WaitReady,
            Started,
            End,
        }

    }
}
