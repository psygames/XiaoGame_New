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
        private List<CardData> m_cards = new List<CardData>();
        public PlayerData whosTurn { get; private set; }

        public void SetData(Message.BattleRoomInfo info)
        {
            id = info.Id;
            name = info.Name;
            state = State.WaitJoin;
        }

        public void RoomSync(Message.CBRoomSync sync)
        {
            whosTurn = m_players.First(a => a.id == sync.WhoseTurn);
            state = (State)sync.State;
        }

        public void SetCards(IList<Message.CardInfo> infos)
        {
            foreach (var info in infos)
            {
                CardData card = new CardData();
                card.SetData(info.Id, info.TableID);
                m_cards.Add(card);
            }
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

        public CardData GetCard(int cardID)
        {
            return m_cards.First(a => a.id == cardID);
        }

        public PlayerData mainPlayer { get { return m_players.First(a => a.isMain); } }


        public enum State
        {
            WaitJoin,
            WaitReady,
            Started,
            End,
        }

    }
}
