using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RedStone
{
    public class HallLoadingState : AbstractState
    {
        public override void Enter(params object[] param)
        {
            GF.StartCoroutine(ResLoad());
        }

        private void OnLoadFinished()
        {
            GF.ChangeState<HallLoginState>();
        }

        IEnumerator ResLoad()
        {
            GF.ShowView<LoadingView>();

            GF.Send(EventDef.HallLoading, new LoadingStatus(LTKey.LOADING_UI, 0));
            yield return new WaitForSeconds(0.3f);

            GF.Send(EventDef.HallLoading, new LoadingStatus(LTKey.LOADING_UI, 15));
            yield return new WaitForSeconds(0.3f);

            GF.Send(EventDef.HallLoading, new LoadingStatus(LTKey.LOADING_UI, 30));
            yield return new WaitForSeconds(0.3f);

            GF.Send(EventDef.HallLoading, new LoadingStatus(LTKey.LOADING_UI, 45));
            yield return new WaitForSeconds(0.3f);

            OnLoadFinished();
        }

        public override void Leave()
        {
        }

        public override void Update()
        {
        }
    }
}
