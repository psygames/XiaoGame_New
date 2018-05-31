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
            GF.GetProxy<SosProxy>().Reset();
            GF.GetProxy<SosProxy>().InitSocket();
            Connect();
        }

        private void Connect()
        {
            GF.Send(EventDef.HallLoading, new LoadingStatus(LTKey.LOADING_WAIT_RESPONSE, 50));
            if (GF.GetProxy<HallProxy>().needReconnectBattle)
            {
                GF.GetProxy<SosProxy>().Reconnect();
            }
            else
            {
                GF.GetProxy<SosProxy>().Connect();

            }
        }

        public override void Leave()
        {
            if (GF.GetProxy<SosProxy>().room.state == Data.SOS.RoomData.State.Dismiss)
            {
                GF.GetProxy<SosProxy>().Close();
                if (UIManager.instance.GetView<BattleView>() != null)
                {
                    UIManager.instance.Unload<BattleView>();
                    UIManager.instance.CloseAll();
                }
            }
        }


        private bool m_lastConnected = false;
        private bool m_lastLogin = false;
        public override void Update()
        {
            if (!m_lastConnected && GF.GetProxy<SosProxy>().isConnected)
            {
                GF.Send(EventDef.HallLoading, new LoadingStatus(LTKey.LOADING_WAIT_LOGIN, 70));
            }
            m_lastConnected = GF.GetProxy<SosProxy>().isConnected;

            if (!m_lastLogin && GF.GetProxy<SosProxy>().isLogin)
            {
                GF.Send(EventDef.HallLoading, new LoadingStatus(LTKey.GENRAL_START, 98));
                Task.WaitFor(1f, () =>
                {
                    GF.ChangeState<BattleState>();
                });
            }
            m_lastLogin = GF.GetProxy<SosProxy>().isLogin;
        }
    }
}
