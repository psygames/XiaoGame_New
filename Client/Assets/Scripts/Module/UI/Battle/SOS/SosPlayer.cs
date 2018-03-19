using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedStone.UI;
using RedStone.Data.SOS;
using System;

namespace RedStone
{
    public class SosPlayer : EventHandleItem
    {
        public PlayerData data { get; private set; }
        public CardData lastPlayedCard { get; protected set; }

        public void Reset()
        {
            lastPlayedCard = null;
            RefreshUI();
        }

        public virtual void Init(PlayerData data)
        {
            this.data = data;
            Reset();
        }

        public virtual void TakeCard(CardData card)
        {
            RefreshUI();
        }

        public virtual void ChangeCard(CardData card)
        {
            RefreshUI();
        }

        public virtual void DropCard(CardData card)
        {
            //TODO: Drop Card
            PlayCard(card);
            RefreshUI();
        }

        public virtual void PlayCard(CardData card)
        {
            lastPlayedCard = card;

            RefreshUI();
        }

        public virtual void Out(CardData handCard)
        {
            if (handCard != null && handCard.id > 0)
                DropCard(handCard);
            RefreshUI();
        }

        public virtual void InvincibleOneRound()
        {
            RefreshUI();
        }

        public virtual void SetReady(bool isReady)
        {
            RefreshUI();
        }

        public virtual void RefreshUI()
        {

        }
    }
}