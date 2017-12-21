using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedStone
{
    public class BattleLoadingState : AbstractState
    {
        public override void Enter(params object[] param)
        {
            GF.Send(EventDef.HallLoading, new LoadingStatus(LTKey.LOADING_UI, 0));
            GF.Send(EventDef.HallLoading, new LoadingStatus(LTKey.LOADING_UI, 50));
            Task.WaitFor(2f, () =>
            {
                GF.ChangeState<BattleLoginState>();
            });
        }


        public override void Leave()
        {
        }

        public override void Update()
        {
        }
    }
}
