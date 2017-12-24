using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedStone.Data
{
    public class CardData
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
}
