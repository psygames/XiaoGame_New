using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedStone
{
    public class LoginState : AbstractState
    {
        public override void Enter(params object[] param)
        {
            GF.ShowView<LoadingView>();
            GF.GetProxy<HallProxy>().network.socket.Connect();
            GF.Send(MessageDefine.HallLoading, new LoadingStatus(LTKey.LOADING_WAIT_LOGIN, 20));
        }

        public override void Leave()
        {
        }

        public override void Update()
        {
        }
    }
}
