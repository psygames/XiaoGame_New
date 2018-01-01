using RedStone.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedStone.SOS
{
    public class Player
    {
        public long uid { get; private set; }
        public int id { get; private set; } //临时ID
        public int gold { get; private set; }
        public int seat { get; private set; }
        public State state { get; private set; }
        public UserData user { get; private set; }
        public string name { get { return user.name; } }
        public bool isAI { get { return user.isAI; } }

        private List<Card> m_handCards = new List<Card>();
        public List<Card> handCards { get { return m_handCards; } }

        public void Init(UserData user, int id)
        {
            this.uid = user.uid;
            this.id = id;
            this.user = user;
            this.seat = id;//TODO:Seat
            gold = user.gold;
            state = State.None;
            m_handCards.Clear();
        }

        public void IncrGold(int count)
        {
            gold += count;
        }

        public void SetState(State state)
        {
            this.state = state;
        }

        public enum State
        {
            None = 0,
            Joined = 1,
            Ready = 2,
            Turn = 3,
            Wait = 4,
            Out = 5,
        }

        public void AddCard(Card card)
        {
            m_handCards.Add(card);
        }

        public void RemoveCard(Card card)
        {
            m_handCards.Remove(card);
        }
    }

}
