using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedStone.UI;
using RedStone.Data.SOS;
using System;
using Message;

namespace RedStone
{
    public class SosResultItem : MonoBehaviour
    {
        public SosCard card;
        public Text playerName;
        public GameObject outObj;
        public GameObject emptyObj;

        public void SetData(Message.BattleResultPlayerInfo info)
        {
            var room = GF.GetProxy<SosProxy>().room;
            var player = room.GetPlayer(info.PlayrID);
            playerName.text = player.numTag;
            if (info.State == (int)PlayerData.State.Out)
            {
                emptyObj.SetActive(false);
                card.gameObject.SetActive(false);
                outObj.SetActive(true);
            }
            else
            {
                outObj.SetActive(false);
                if (info.Cards.Count <= 0)
                {
                    card.gameObject.SetActive(false);
                    emptyObj.SetActive(true);
                }
                else
                {
                    emptyObj.SetActive(false);
                    card.gameObject.SetActive(true);
                    var cardData = room.GetCard(info.Cards[0]);
                    card.SetData(cardData);
                }
            }
        }
    }
}