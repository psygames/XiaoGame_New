using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedStone.UI;

namespace RedStone
{
    public class BattleView : ViewBase
    {
        public override void OnInit()
        {
            base.OnInit();

            Register<int>(EventDef.PlayerReady, OnReady);
        }

        private void OnReady(int id)
        {
            GetProxy<BattleProxy>().Ready();
        }


        public override void OnOpen()
        {
            base.OnOpen();
        }

        void OnClickReady()
        {
            GetProxy<BattleProxy>().Ready();
        }
    }
}
