using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedStone
{
    public abstract class AbstractState
    {
        public abstract void Enter(params object[] param);
        public abstract void Update();
        public abstract void Leave();
    }
}
