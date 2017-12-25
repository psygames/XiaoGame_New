using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedStone.UI;

namespace RedStone
{
    public class SosRoom : MonoBehaviour
    {
        public Transform cardRoot;
        public Text playerName;
        public Text effect;

        public List<SosCard> m_handCards = new List<SosCard>();

        public void Init()
        {
            GF.Register<int>(EventDef.SOS.Ready, OnReady);
        }

        void OnReady(int id)
        {

        }

        public void TakeCard(int cardID)
        {
            SosCard newCard = new SosCard();

        }

        public void PlayCard(int cardID)
        {
        }


        public SosPlayer GetPlayer(int playerID)
        {

        }
    }
}