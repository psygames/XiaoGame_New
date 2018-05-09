using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedStone.UI;
using RedStone.Data.SOS;
using System;

namespace RedStone
{
    public class SosNetPlayer : SosPlayer
    {
        public Text lastCardText;
        public Image headIcon;
        public Image numBg;
        public Text tagNum;
        public GameObject outObj;

        public Action<PlayerData> onClickCallback { get; set; }

        public override void Init(PlayerData data)
        {
            base.Init(data);
            headIcon.enableGrey = true;
            headIcon.SetSprite(data.headIcon);

            RefreshUI();
        }

        public override void RefreshUI()
        {
            base.RefreshUI();

            if (data.state == PlayerData.State.Out)
            {
                headIcon.Grey = true;
                outObj.SetActive(true);
            }
            else
            {
                headIcon.Grey = false;
                outObj.SetActive(false);
            }

            numBg.SetSprite("member_bg_" + data.seat.ToString());
            lastCardText.text = lastPlayedCard == null ? "空" : lastPlayedCard.name;
            tagNum.text = data.seat.ToString();
        }

        public void OnClick()
        {
            if (onClickCallback != null)
                onClickCallback.Invoke(data);
        }
    }
}