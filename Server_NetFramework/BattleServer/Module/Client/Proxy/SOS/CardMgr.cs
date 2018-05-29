using System;
using System.Collections.Generic;
using System.Text;
using Core;

namespace RedStone.SOS
{
    public class Card
    {
        public int id;
        public int tableID;
        public string name { get { return table.name; } }
        public int point { get { return table.point; } }
        public int star { get { return table.star; } }
        public CardType type { get { return (CardType)table.type; } }
        public TableSosCard table { get { return TableManager.instance.GetData<TableSosCard>(tableID); } }

        public void SetData(int id, int tableID)
        {
            this.id = id;
            this.tableID = tableID;
        }
    }

    public enum CardType
    {
        None,
        ForSelf,
        ForOneTarget,
        ForAll,
    }


    public class CardMgr
    {
        Random rand = new Random();

        private List<Card> m_allCards = new List<Card>();
        private List<Card> m_leftCards = new List<Card>();

        public List<Card> allCards { get { return m_allCards; } }
        public List<Card> leftCards { get { return m_leftCards; } }

        public bool isEmpty { get { return leftCards.Count <= 0; } }

        public void Reset()
        {
            m_allCards.Clear();

            var all = TableManager.instance.GetAllData<TableSosCard>();
            int incrID = 1;
            foreach (var tab in all)
            {
                for (int i = 0; i < tab.Value.star; i++)
                {
                    Card card = new Card();
                    card.SetData(incrID, tab.Value.id);
                    m_allCards.Add(card);
                    incrID++;
                }
            }

            m_leftCards.Clear();
            m_leftCards.AddRange(m_allCards);
            Shuffle();
        }

        public void Shuffle()
        {
            int n = m_leftCards.Count * 2;
            while (n-- > 0)
            {
                int i = rand.Next(m_leftCards.Count);
                int j = rand.Next(m_leftCards.Count);
                var tmp = m_leftCards[i];
                m_leftCards[i] = m_leftCards[j];
                m_leftCards[j] = tmp;
            }
        }

        public Card TakeCard()
        {
            if (isEmpty)
                return null;
            var card = m_leftCards[0];
            m_leftCards.RemoveAt(0);
            return card;
        }

        public void PutCard(Card card)
        {
            m_leftCards.Add(card);
        }
    }
}
