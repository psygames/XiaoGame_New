using System;
using System.Collections.Generic;

using System.Text;

 namespace RedStone.UI
{
    public interface IEventHandler
    {
        bool OnEvent(string sender, params object[] args);
    }
}
