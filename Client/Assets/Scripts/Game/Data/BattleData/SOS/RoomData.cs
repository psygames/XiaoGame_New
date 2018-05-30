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
        public int leftCardCount { get; private set; }
        public int playedCardCount { get { return Math.Max(0, m_cards.Count - leftCardCount - m_players.Count); } }
        private CardData m_defaultCard = new CardData();


        public void SetData(Message.BattleRoomInfo info)
        {
            id = info.Id;
            name = info.Name;
            state = State.WaitJoin;
        }

        public void SetState(int whosTurn, int state, int leftCardCount)
        {
            this.whosTurn = null;
            foreach (var p in m_players)
            {
                if (p.id == whosTurn)
                {
                    this.whosTurn = p;
                    p.SetTurned(true);
                }
                else
                {
                    p.SetTurned(false);
                }
            }

            this.state = (State)state;
            this.leftCardCount = leftCardCount;
        }

        public void RoomSync(Message.CBRoomSync sync)
        {
            SetState(sync.WhoseTurn, sync.State, sync.LeftCardCount);
            //TODO: 新回合玩家清除EFFECT
            var whos = m_players.First(a => a.id == sync.WhoseTurn);
            if (whos != null)
                whos.SetEffect(PlayerData.Effect.None);
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
            if (cardID <= 0)
                return m_defaultCard;
            return m_cards.First(a => a.id == cardID);
        }

        public PlayerData mainPlayer { get { return m_players.First(a => a.isMain); } }
        public List<PlayerData> players { get { return m_players; } }


        public enum State
        {
            WaitJoin = 0,
            WaitReady = 1,
            Started = 2,
            End = 3,
            Dismiss = 4,
        }
    }
}
