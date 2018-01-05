using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedStone.UI;

namespace RedStone
{
    public class BattleView : ViewBase
    {
        public SosRoom room;

        private void Start()
        {
            transform.SetSiblingIndex(1);
        }

        public override void OnInit()
        {
            base.OnInit();
            room.Init();

            Register(EventDef.SOS.Joined, room.OnJoined);
            Register<int>(EventDef.SOS.Ready, room.OnReady);
            Register(EventDef.SOS.RoomSync, room.OnRoomSync);
            Register<Message.CBSendCardSync>(EventDef.SOS.SendCard, room.OnSendCard);
            Register<Message.CBPlayCardSync>(EventDef.SOS.PlayCard, room.OnPlayCard);
            Register<Message.CBBattleResultSync>(EventDef.SOS.BattleResult, room.OnBattleResult);
            Register<Message.CBCardEffectSync>(EventDef.SOS.CardEffect, room.OnCardEffect);
            Register<Message.CBPlayerDropCardSync>(EventDef.SOS.DropCard, room.OnDropCard);
            Register<Message.CBPlayerOutSync>(EventDef.SOS.PlayerOut, room.OnPlayerOut);
        }



        public override void OnOpen()
        {
            base.OnOpen();
        }

        void OnClickReady()
        {
            GetProxy<SosProxy>().Ready();
        }

        void OnClickPlayCard()
        {
            room.OnClickPlayCard();
        }

        void OnClickBack()
        {
            GF.ChangeState<HallState>();
        }
    }
}
