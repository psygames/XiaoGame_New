using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace RedStone
{
    public class Toast : ViewBase
    {
        public GameObject template;
        public GameObject standloneTemplate;

        private static Toast m_instance;
        public static Toast instance
        {
            get { return m_instance; }
        }

        private List<ToastItem> m_toastList = new List<ToastItem>();
        private ToastItemStandalone m_toastStandalone;

        public Transform itemRoot;
        public const float TOAST_INTERVAL_MAX = 0.3f;
        public const float TOAST_INTERVAL_MIN = 0.05f;
        private float m_toastInterval = 0;

        void Awake()
        {
            m_instance = this;
        }

        public override void OnInit()
        {
            m_toastList.Clear();
            for (int i = 0; i < 5; i++)
            {
                GameObject go = Instantiate(template) as GameObject;
                UIHelper.SetParent(itemRoot, go.transform);
                ToastItem item = go.GetComponent<ToastItem>();
                m_toastList.Add(item);
                go.SetActive(false);
            }


            GameObject st_go = Instantiate(standloneTemplate) as GameObject;
            UIHelper.SetParent(itemRoot, st_go.transform);
            m_toastStandalone = st_go.GetComponent<ToastItemStandalone>();
        }


        //TODO: Android Fix
        public void ModuleChanged()
        {
            if (m_toastStandalone.isShow)
            {
                m_toastStandalone.Hide(0.1f);
            }
        }

        /// <summary>
        /// default message type fail
        /// </summary>
        /// <param name="msg"></param>
        public void Show(string msg, params object[] args)
        {
            msg = LT.GetText(msg, args);
            ShowStandalone(msg, true);
        }

        public void ShowNormal(string msg, params object[] args)
        {
            msg = LT.GetText(msg, args);
            ShowStandalone(msg, false);
        }

        private void ShowStandalone(string msg, bool isFailTag)
        {
            m_toastStandalone.Show(msg, isFailTag);
        }

        /*
        public void ShowGains(Data.CostDataItem item)
        {
            ShowWithFailTag(item.costInfoWithIcon, false);
        }

        public void ShowGains(message.GainModel gainModel)
        {
            var data = new Data.CostDataItem();
            data.SetData(gainModel);
            ShowGains(data);
        }

        public void ShowGains(Data.CostData data)
        {
            foreach (Data.CostDataItem item in data.costs)
            {
                ShowGains(item);
            }
        }

        public void ShowGains(IList<message.GainModel> gains)
        {
            Data.CostData data = new Data.CostData();
            data.SetData(gains);
            ShowGains(data);
        }
        */

        private void ShowWithFailTag(string msg, bool isFailMsg)
        {
            ItemData item = new ItemData();
            item.text = msg;
            item.isFailMsg = isFailMsg;
            item.isWaiting = false;
            Enqueue(item);
        }

        private void Show(ToastItem item, ItemData data)
        {
            m_toastInterval = TOAST_INTERVAL_MAX;
            item.Show(data.text, data.isFailMsg);
            for (int i = 0; i < m_toastList.Count; i++)
            {
                if (!m_toastList[i].isShow)
                {
                    m_toastInterval = TOAST_INTERVAL_MIN;
                    continue;
                }
                if (m_toastList[i] != item)
                    m_toastList[i].Move(Vector2.up * 60f);
            }
        }

        private void Enqueue(ItemData item)
        {
            m_queue.Enqueue(item);
        }

        public void Update()
        {
            m_toastInterval = Mathf.Max(m_toastInterval - Time.deltaTime, 0);
            if (m_queue.Count <= 0 || m_toastInterval > 0)
                return;

            m_toastList.Sort((a, b) => { return a.cooldown.CompareTo(b.cooldown); });
            ItemData data = m_queue.Dequeue();
            Show(m_toastList[0], data);
        }

        public Queue<ItemData> m_queue = new Queue<ItemData>();

        public class ItemData
        {
            public string text;
            public bool isFailMsg;
            public bool isWaiting;
        }
    }
}