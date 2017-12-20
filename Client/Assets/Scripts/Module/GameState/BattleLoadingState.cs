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
            Task.WaitFor(0.5f, () =>
            {
                GF.Send(EventDef.HallLoading, new LoadingStatus(LTKey.LOADING_UI, 50));
                Connect();
            });
        }

        private void Connect()
        {
            GF.Send(EventDef.HallLoading, new LoadingStatus(LTKey.LOADING_UI, 70));

            GF.GetProxy<BattleProxy>().Connect();
        }

        public override void Leave()
        {
        }

        public override void Update()
        {
            if (GF.GetProxy<BattleProxy>().isLogin)
            {
                GF.ChangeState<BattleState>();
            }
        }
    }
}
