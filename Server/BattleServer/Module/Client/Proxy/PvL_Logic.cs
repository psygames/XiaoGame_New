using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedStone.Data;


namespace RedStone
{
    public class PvL_Logic 
    {
        RoomProxy roomProxy { get { return ProxyManager.instance.GetProxy<RoomProxy>(); } }
        private int m_roomID;
        RoomData room { get { return roomProxy.GetRoom(m_roomID); } }

        public void Init(int roomID)
        {
            m_roomID = roomID;

            RegisterMsg();
        }


        public void Update()
        {

        }


        private void RegisterMsg()
        {
            foreach()
        }

    }
}
