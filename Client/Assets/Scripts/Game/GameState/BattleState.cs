using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedStone
{
    public class BattleState : AbstractState
    {
        public override void Enter(params object[] param)
        {
            GF.ShowView<BattleView>();
        }

        public override void Leave()
        {
            GF.GetProxy<SosProxy>().Close();
            UIManager.instance.Unload<BattleView>();
        }

        public override void Update()
        {

        }
    }
}
