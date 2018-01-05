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
        public Effect effect { get; private set; }

        private List<Card> m_handCards = new List<Card>();
        public List<Card> handCards { get { return m_handCards; } }
        public Card oneCard { get { return m_handCards.Count > 0 ? m_handCards[0] : null; } }

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

        public void SetEffect(Effect effect)
        {
            this.effect = effect;
        }

        public enum State
        {
            None = 0,
            Joined = 1,
            Ready = 2,
            Turn = 3,
            NotTurn = 4,
            Out = 5,
        }

        public enum Effect
        {
            None,
            InvincibleOneRound,
        }

        public void AddCard(Card card)
        {
            m_handCards.Add(card);
        }

        public void ChangeCard(Card card)
        {
            if (m_handCards.Count != 1)
            {
                Debug.LogError("手牌数量不正确，不能换牌！");
                return;
            }
            m_handCards[0] = card;
        }

        public void RemoveCard(Card card)
        {
            m_handCards.Remove(card);
        }
    }

}
