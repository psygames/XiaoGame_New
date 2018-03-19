using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedStone
{
    public class EventDef
    {
        public const string HallLoading = "HallLoading";
        public const string BattleLoading = "BattleLoading";

        public const string MessageBox = "MessageBox";
        public const string MatchBegin = "MatchBegin";
        public const string MatchCancel = "MatchCancel";
        public const string MatchSuccess = "MatchSuccess";



        public class SOS
        {
            public const string Joined = "SOS.Joined";
            public const string Ready = "SOS.Ready";
            public const string RoomSync = "SOS.RoomSync";
            public const string SendCard = "SOS.SendCard";
            public const string PlayCard = "SOS.PlayCard";
            public const string BattleResult = "SOS.BattleResult";
            public const string CardEffect = "SOS.CardEffect";
            public const string DropCard = "SOS.DropCard";
            public const string PlayerOut = "SOS.PlayerOut";
            public const string SendMessageSync = "SOS.SendMessage";
        }
    }
}
