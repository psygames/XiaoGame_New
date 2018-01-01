using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RedStone
{
    public class UIRepeat : MonoBehaviour
    {
        public enum ChildAlignment
        {
            Left,
            Center,
            Right,
        }

        public GameObject template;
        public Vector2 space;
        public ChildAlignment align = ChildAlignment.Left;
        public bool useTemplate = false;

        private List<GameObject> m_items = new List<GameObject>();
        private int m_repeatCount = 0;
        public int repeatCount
        {
            get
            {
                return m_repeatCount;
            }

            set
            {
                m_repeatCount = value;
                Refresh();
            }
        }

        void Awake()
        {
            template.SetActive(false);

            if (template.transform.parent != transform)
                useTemplate = false;

            if(useTemplate)
                m_items.Add(template);
        }

        void CreateItem()
        {
            GameObject go = Instantiate(template);
            UIHelper.SetParent(transform, go.transform);
            m_items.Add(go);
        }

        void Refresh()
        {
            while (m_repeatCount > m_items.Count)
            {
                CreateItem();
            }

            for (int i = 0; i < m_items.Count; i++)
            {
                if (i >= m_repeatCount)
                    m_items[i].SetActive(false);
                else
                {
                    m_items[i].SetActive(true);
                    if (align == ChildAlignment.Left)
                    {
                        m_items[i].transform.localPosition = i * space;
                    }
                    else if (align == ChildAlignment.Right)
                    {
                        m_items[i].transform.localPosition = i * -space;
                    }
                    else if (align == ChildAlignment.Center)
                    {
                        m_items[i].transform.localPosition = (m_repeatCount - 1) * -space * 0.5f + i * space;
                    }

                }
            }
        }
    }
}