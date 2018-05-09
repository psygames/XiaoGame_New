using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedStone.UI;

namespace RedStone
{
    public class SosChatMessageItem : MonoBehaviour
    {
        public Image headIcon;
        public Text nameText;
        public Text contentText;

        public void SetData(Data.SOS.PlayerData player, string content)
        {
            headIcon.SetSprite(player.headIcon);
            nameText.text = player.numTag;
            contentText.text = content;
        }
    }
}