using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedStone.Data.SOS
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
        public string image { get { return table.image; } }
        public string bg { get { return table.image + "_bg"; } }
        public string titleBg { get { return table.image + "_title_bg"; } }

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
