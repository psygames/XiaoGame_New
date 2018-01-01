using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedStone.UI;
using System;
using RedStone.Data.SOS;

namespace RedStone
{
    public class SosCard : EventHandleItem
    {
        public CardData data { get; private set; }

        public Text point;
        public Text title;
        public Text description;
        public Text effect;
        public Image bg;
        public Image image;
        public Image titleBg;
        public RectTransform descPosTrans;
        public RectTransform descSizeTrans;
        public UnityEngine.UI.LayoutElement layoutElement;

        public Action<CardData> onClickCallback { get; set; }
        public bool isSelected { get; private set; }

        private void Awake()
        {
            HideDetailInfoImmdiately();
        }

        public void SetData(CardData card)
        {
            this.data = card;
            if (card.point >= 10)
            {
                this.point.text = "∞";
            }
            else
            {
                this.point.text = card.point.ToString();
            }
            this.title.text = card.name;
            this.description.text = card.table.description;
            this.effect.text = card.table.effect;

            image.SetSprite(card.image);
            bg.SetSprite(card.bg);
            titleBg.SetSprite(card.titleBg, false);
        }

        private float m_holdCounter = 0;
        private bool m_curPressShowedDetailInfo = false;
        private float m_dragDist = 0;
        private bool m_isDown = false;

        public void OnDown()
        {
            m_holdCounter = 0;
            m_dragDist = 0;
            m_curPressShowedDetailInfo = false;
            m_isDown = true;
        }

        public void OnUp()
        {
            m_isDown = false;
            if (m_curPressShowedDetailInfo)
                HideDetailInfo();
        }

        public void OnDrag(Vector2 delta)
        {
            m_dragDist += delta.magnitude;
        }

        public void OnClick()
        {
            if (m_curPressShowedDetailInfo)
                return;

            if (onClickCallback != null)
                onClickCallback.Invoke(data);
        }

        private void UpdateLayout()
        {
            if (isSelected)
            {
                transform.localScale = Vector3.one * 0.66f;
                layoutElement.minWidth = 500 * 0.66f;
            }
            else
            {
                transform.localScale = Vector3.one * 0.6f;
                layoutElement.minWidth = 500 * 0.6f;
            }
        }

        private void Update()
        {
            m_holdCounter += Time.deltaTime;

            if (m_isDown &&
                m_dragDist <= 5 && m_holdCounter >= 0.5f
                && m_holdCounter - Time.deltaTime < 0.5f)
            {
                m_curPressShowedDetailInfo = true;
                ShowDetailInfo();
            }

            UpdateLayout();
        }

        private void ShowDetailInfo()
        {
            uTools.uTweenScale.Begin(descPosTrans.gameObject, descPosTrans.localScale, Vector3.one, 0.1f);
            float h = descSizeTrans.sizeDelta.y;
            uTools.uTweenPosition.Begin(descPosTrans.gameObject, descPosTrans.localPosition, Vector3.up * h * 0.5f, 0.1f);
        }

        void HideDetailInfo()
        {
            uTools.uTweenScale.Begin(descPosTrans.gameObject, descPosTrans.localScale, Vector3.zero, 0.1f);
            uTools.uTweenPosition.Begin(descPosTrans.gameObject, descPosTrans.localPosition, Vector3.zero, 0.1f);
        }

        void HideDetailInfoImmdiately()
        {
            descPosTrans.localScale = Vector3.zero;
        }

        public void BeSelected(bool isSelected)
        {
            this.isSelected = isSelected;
        }
    }
}