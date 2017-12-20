using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedStone
{
    public class HallLoginState : AbstractState
    {
        public override void Enter(params object[] param)
        {
            GF.ShowView<LoadingView>();
            Connect();
        }

        private void Connect()
        {
            GF.Send(EventDef.HallLoading, new LoadingStatus(LTKey.LOADING_WAIT_RESPONSE, 5));
            GF.GetProxy<HallProxy>().Connect(3, () =>
            {
                MessageBox.Show("连接失败", "连接服务器失败，是否重新连接？", MessageBoxStyle.OKClose
                   , (result) =>
                   {
                       if (result.result == MessageBoxResultType.OK)
                       {
                           Connect();
                       }
                   });
            });
        }

        public override void Leave()
        {
        }

        public override void Update()
        {
            if (GF.GetProxy<HallProxy>().isConnected)
            {
                GF.Send(EventDef.HallLoading, new LoadingStatus(LTKey.LOADING_WAIT_LOGIN, 20));
            }
        }
    }
}
