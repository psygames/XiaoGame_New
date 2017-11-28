using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedStone
{
    public class LoadingState : AbstractState
    {
        public override void Enter(params object[] param)
        {
            GF.Send(MessageDefine.HallLoading, new LoadingStatus(LTKey.LOADING_UI, 50));
        }

        public override void Leave()
        {
        }

        public override void Update()
        {
        }
    }
}
