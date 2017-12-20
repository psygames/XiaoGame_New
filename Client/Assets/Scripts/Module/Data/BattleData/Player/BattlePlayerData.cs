using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedStone.Data
{
    public class BattlePlayerData
    {
        public int id { get; private set; }
        public string name { get; private set; }
        public int level { get; private set; }
        public int gold { get; private set; }
        public bool isMain { get; private set; }
        public int seat { get; private set; }
        public bool isReady { get; private set; }

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
    }
}
