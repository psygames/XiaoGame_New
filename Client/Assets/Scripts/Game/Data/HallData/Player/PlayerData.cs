using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RedStone.Data
{
    public class PlayerData
    {
        public long uid { get; private set; }
        public string name { get; private set; }
        public int level { get; private set; }
        public int exp { get; private set; }
        public int levelUpExp { get { return (int)(Mathf.Pow(1.5f, level) * 100); } }
        public int gold { get; private set; }

        public void SetData(Message.PlayerInfo info)
        {
            uid = info.Uid;
            name = info.Name;
            level = info.Level;
            exp = info.Exp;
            gold = info.Gold;
        }

        public void IncrExp(int exp)
        {
            this.exp += exp;
        }

        public void IncrGold(int gold)
        {
            this.gold += gold;
        }

        public void SetName(string name)
        {
            this.name = name;
        }
    }
}
