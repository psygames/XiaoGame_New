using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedStone
{
    public class BattleLoginState : AbstractState
    {
        public override void Enter(params object[] param)
        {
            Connect();
        }

        private void Connect()
        {
            GF.Send(EventDef.HallLoading, new LoadingStatus(LTKey.LOADING_WAIT_RESPONSE, 50));
            GF.GetProxy<BattleProxy>().Connect();
            Task.WaitFor(3, () =>
            {
                if (!GF.GetProxy<BattleProxy>().isConnected)
                {
                    MessageBox.Show("连接失败", "连接战场失败，是否重新连接？", MessageBoxStyle.OKClose
                    , (result) =>
                    {
                        if (result.result == MessageBoxResultType.OK)
                        {
                            Connect();
                        }
                    });
                }
            });
        }

        public override void Leave()
        {

        }

        public override void Update()
        {
            if (GF.GetProxy<BattleProxy>().isConnected)
            {
                GF.Send(EventDef.HallLoading, new LoadingStatus(LTKey.LOADING_WAIT_LOGIN, 70));
            }

            if (GF.GetProxy<BattleProxy>().isLogin)
            {
                GF.Send(EventDef.HallLoading, new LoadingStatus(LTKey.GENRAL_START, 98));
                Task.WaitFor(1f, () =>
                {
                    GF.ChangeState<BattleState>();
                });
            }
        }
    }
}
