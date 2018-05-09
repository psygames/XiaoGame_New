using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedStone.UI;
using RedStone.Data.SOS;
using System;
using Message;

namespace RedStone
{
    public class SosResultPanel : EventHandleItem
    {
        public Transform itemRoot;
        public SosResultItem template;

        public Text title;
        public RoomData room { get { return GF.GetProxy<SosProxy>().room; } }

        private void Awake()
        {
            template.gameObject.SetActive(false);
        }

        private List<SosResultItem> items = new List<SosResultItem>();
        public void Show(CBBattleResultSync data)
        {
            gameObject.SetActive(true);
            uTools.uTweenScale.Begin(gameObject, Vector3.zero, Vector3.one, 0.2f, 0);

            var winner = data.ResultInfos.First(a => a.IsWin);
            if (winner != null)
                title.text = "恭喜 {0} 获得最终胜利！".FormatStr(room.GetPlayer(winner.PlayrID).numTag);
            else
                title.text = "没有获胜玩家";

            GameObjectHelper.SetListContent(template, itemRoot, items, data.ResultInfos
                , (index, item, info) =>
                {
                    item.SetData(info);
                });
        }

        public void OnClickClose()
        {
            uTools.uTweenScale.Begin(gameObject, Vector3.one, Vector3.zero, 0.3f, 0);
        }
    }
}