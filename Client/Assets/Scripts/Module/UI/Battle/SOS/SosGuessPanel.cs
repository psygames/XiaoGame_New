using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedStone.UI;
using RedStone.Data.SOS;
using System;

namespace RedStone
{
    public class SosGuessPanel : EventHandleItem
    {
        public Transform itemRoot;
        public SosGuessCardItem template;

        public Action<int> onSelectedCallback { get; set; }

        public RoomData room { get { return GF.GetProxy<SosProxy>().room; } }
        private void Awake()
        {
            template.gameObject.SetActive(false);
        }

        private List<SosGuessCardItem> items = new List<SosGuessCardItem>();
        public void Show()
        {
            gameObject.SetActive(true);
            var allCards = TableManager.instance.GetAllData<TableSosCard>();
            GameObjectHelper.SetListContent(template, itemRoot, items, allCards
                , (index, item, data) =>
                 {
                     item.SetData(data.Value, (a) =>
                      {
                          Hide();
                          if (onSelectedCallback != null)
                              onSelectedCallback.Invoke(a.id);
                      });
                 });
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void OnClickCancel()
        {
            Hide();
        }
    }
}