using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedStone
{
    public class HallState : AbstractState
    {
        public override void Enter(params object[] param)
        {
            GF.ShowView<HomeView>();
        }

        public override void Leave()
        {
        }

        public override void Update()
        {

        }
    }
}
