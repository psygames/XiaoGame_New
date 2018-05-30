using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedStone.UI;

namespace RedStone
{
    public class BattleView : ViewBase
    {
        public SosRoom room;
        public Data.SOS.RoomData roomData { get { return GF.GetProxy<SosProxy>().room; } }

        private void Start()
        {
            transform.SetSiblingIndex(1);
        }

        public override void OnInit()
        {
            base.OnInit();
            room.Init();

            Register(EventDef.SOS.Joined, room.OnJoined);
            Register(EventDef.SOS.Reconnected, room.OnReconnected);
            Register<int>(EventDef.SOS.Ready, room.OnReady);
            Register(EventDef.SOS.RoomSync, room.OnRoomSync);
            Register<Message.CBSendCardSync>(EventDef.SOS.SendCard, room.OnSendCard);
            Register<Message.CBPlayCardSync>(EventDef.SOS.PlayCard, room.OnPlayCard);
            Register<Message.CBBattleResultSync>(EventDef.SOS.BattleResult, room.OnBattleResult);
            Register<Message.CBCardEffectSync>(EventDef.SOS.CardEffect, room.OnCardEffect);
            Register<Message.CBPlayerDropCardSync>(EventDef.SOS.DropCard, room.OnDropCard);
            Register<Message.CBPlayerOutSync>(EventDef.SOS.PlayerOut, room.OnPlayerOut);
            Register<Message.CBSendMessageSync>(EventDef.SOS.SendMessageSync, room.OnSendMessageSync);
        }

        public override void OnOpen()
        {
            base.OnOpen();
        }

        void OnClickExit()
        {
            if (roomData.state != Data.SOS.RoomData.State.End)
            {
                Toast.instance.Show("当前状态不能退出房间");
                return;
            }
            GF.ChangeState<HallState>();
        }

        void OnClickSendMessage()
        {
            room.OnClickSendMessage();
        }
    }
}
