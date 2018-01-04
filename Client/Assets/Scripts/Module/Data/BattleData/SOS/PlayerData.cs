using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedStone.Data.SOS
{
    public class PlayerData
    {
        public int id { get; private set; }
        public string name { get; private set; }
        public int level { get; private set; }
        public int gold { get; private set; }
        public bool isMain { get; private set; }
        public int seat { get; private set; }
        public bool isReady { get; private set; }
        public bool isTurned { get; private set; }

        private List<CardData> m_handCards = new List<CardData>();
        public List<CardData> handCards { get { return m_handCards; } }

        public void SetData(Message.BattlePlayerInfo info)
        {
            id = info.Id;
            name = info.Name;
            level = info.Level;
            gold = info.Gold;
            isMain = info.IsSelf;
            seat = info.Seat;
        }

        public void IncrGold(int gold)
        {
            this.gold += gold;
        }

        public void SetName(string name)
        {
            this.name = name;
        }


        public void SetReady(bool isReady)
        {
            this.isReady = isReady;
        }

        public void SetTurned(bool isTurned)
        {
            this.isTurned = isTurned;
        }

        public void AddCard(CardData card)
        {
            m_handCards.Add(card);
        }

        public void RemoveCard(CardData card)
        {
            if (m_handCards.Contains(card))
                m_handCards.Remove(card);
            else
            {
                int index = m_handCards.QuickFindIndex(a => a.id <= 0);
                if (index >= 0)
                    m_handCards.RemoveAt(index);

            }
        }

        public CardData GetHandCard(int cardID)
        {
            return m_handCards.First(a => a.id == cardID);
        }
    }
}
