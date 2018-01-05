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
        public bool isTurned { get { return state == State.Turn; } }
        public State state { get; private set; }
        public Effect effect { get; private set; }

        private List<CardData> m_handCards = new List<CardData>();
        public List<CardData> handCards { get { return m_handCards; } }
        public CardData oneCard { get { return m_handCards[0]; } }

        public void SetData(Message.BattlePlayerInfo info)
        {
            id = info.Id;
            name = info.Name;
            level = info.Level;
            gold = info.Gold;
            isMain = info.IsSelf;
            seat = info.Seat;
            state = info.Joined ? State.Joined : State.None;
        }

        public void IncrGold(int gold)
        {
            this.gold += gold;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public void SetReady()
        {
            if (this.state == State.Out)
                return;
            this.state = State.Ready;
        }

        public void SetTurned(bool isTurned)
        {
            if (this.state == State.Out)
                return;
            this.state = isTurned ? State.Turn : State.NotTurn;
        }

        public void Out()
        {
            this.state = State.Out;
        }

        public void SetEffect(Effect effect)
        {
            this.effect = effect;
        }

        public void AddCard(CardData card)
        {
            m_handCards.Add(card);
        }


        public void ChangeCard(CardData card)
        {
            if (m_handCards.Count != 1)
            {
                UnityEngine.Debug.LogError("手牌数量不正确，不能换牌！");
                return;
            }
            m_handCards[0] = card;
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
    }
}
