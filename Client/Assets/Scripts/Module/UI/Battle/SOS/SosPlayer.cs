using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedStone.UI;
using RedStone.Data.SOS;

namespace RedStone
{
    public class SosPlayer : MonoBehaviour
    {
        public PlayerData data { get; private set; }
        public Transform cardRoot;
        public SosCard cardTemplate;

        public Text playerName;
        public Text effect;
        public Text state;

        protected List<SosCard> m_handCards = new List<SosCard>();

        private void Awake()
        {
            cardTemplate.gameObject.SetActive(false);
        }

        public void Init(PlayerData data)
        {
            this.data = data;
            RefreshUI();
        }

        void RefreshUI()
        {
            playerName.text = data.name;
            state.text = data.isReady ? "Ready" : "Not Ready";
        }

        public void TakeCard(int cardID)
        {
            var cardData = data.GetHandCard(cardID);
            var card = GameObjectHelper.AddChild(cardRoot, cardTemplate);
            card.gameObject.SetActive(true);
            card.SetData(cardData);

            //TODO: Do animation or something else

            m_handCards.Add(card);
        }

        public void PlayCard(int cardID, int targetID = -1)
        {

            Debug.LogError("{0} play card : {1} target : {2}".FormatStr(name, targetID));
        }

        public void SetReady(bool isReady)
        {
            RefreshUI();
        }

        public SosCard GetCard(int cardID)
        {
            return m_handCards.First(a => a.data.id == cardID);
        }

    }
}