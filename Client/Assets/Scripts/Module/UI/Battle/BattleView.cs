using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedStone.UI;

namespace RedStone
{
    public class BattleView : ViewBase
    {
        public SosRoom room;

        public override void OnInit()
        {
            base.OnInit();
            room.Init();
        }



        public override void OnOpen()
        {
            base.OnOpen();
        }

        void OnClickReady()
        {
            GetProxy<SosProxy>().Ready();
        }

        void OnClickPlayCard()
        {
            room.OnClickPlayCard();
        }
    }
}
