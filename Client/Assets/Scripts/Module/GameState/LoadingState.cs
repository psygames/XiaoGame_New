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
            Task.WaitFor(0.5f, () =>
            {
                GF.Send(EventDef.HallLoading, new LoadingStatus(LTKey.LOADING_UI, 100));
            });

            Task.WaitFor(2, () =>
            {
                GF.ShowView<HomeView>();
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
