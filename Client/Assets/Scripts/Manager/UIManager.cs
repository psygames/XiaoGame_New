using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RedStone
{
    public class UIManager : Core.Singleton<UIManager>
    {
        private Dictionary<string, string> m_prefabs = new Dictionary<string, string>();

        private Dictionary<string, ViewBase> m_views = new Dictionary<string, ViewBase>();

        private Stack m_stack = new Stack();

        public Transform uiRoot;

        public void Init()
        {
            uiRoot = GameObject.Find("UI Root").transform;
            RegisteAll();
            PreLoad();
            InitAll();
        }

        public void InitAll()
        {
            foreach (var kv in m_views)
            {
                kv.Value.OnInit();
            }

            PopShow<MessageBoxView>();
        }

        public void PreLoad()
        {
            foreach (var kv in m_prefabs)
            {
                Object obj = Resources.Load(MyPath.RES_UI + kv.Value);
                GameObject go = Object.Instantiate(obj) as GameObject;
                ViewBase _base = go.GetComponent<ViewBase>();
                if (_base == null)
                {
                    Debug.LogError(kv.Value + " Get ViewBase is NULL");
                }
                else
                {
                    m_views.Add(_base.GetType().ToString(), _base);
                    go.transform.SetParent(uiRoot, false);
                    go.SetActive(false);
                }
            }
        }

        public void RegisteAll()
        {
            AddUI("Hall/Home");

            AddUI("Battle/Battle");

            AddUI("Common/Loading");
            AddUI("Common/MessageBox");
            AddUI("Common/Toast");
        }

        public void AddUI(string name, string prefabPath = null)
        {
            if (prefabPath == null)
                prefabPath = name;
            m_prefabs.Add(name, prefabPath);
        }

        private ViewBase GetView<T>()
        {
            return m_views[typeof(T).ToString()];
        }

        public void Show<T>()
        {
            string name = typeof(T).ToString();
            if (GetView<T>().isBottom)
            {
                CloseAll();
            }
            UIContent content = new UIContent(name);
            m_stack.Push(content);
            ShowGameObject(name);
        }

        public void PopShow<T>()
        {
            string name = typeof(T).ToString();
            m_views[name].gameObject.SetActive(true);
            m_views[name].OnOpen();
        }

        public void CloseAll()
        {
            while (m_stack.Count > 0)
            {
                UIContent content = m_stack.Pop() as UIContent;
                m_views[content.name].gameObject.SetActive(false);
                m_views[content.name].OnClose();
            }
        }

        public void Back()
        {
            UIContent content = m_stack.Pop() as UIContent;
            m_views[content.name].gameObject.SetActive(false);
            m_views[content.name].OnClose();


            UIContent peek = m_stack.Peek() as UIContent;
            m_views[peek.name].gameObject.SetActive(true);
        }

        private void ShowGameObject(string name)
        {
            UIContent peek = m_stack.Peek() as UIContent;
            m_views[peek.name].gameObject.SetActive(true);
            m_views[peek.name].OnOpen();
            // hide others
            foreach (UIContent obj in m_stack)
            {
                if (obj != peek)
                {
                    m_views[obj.name].gameObject.SetActive(false);
                    m_views[obj.name].OnClose();
                }
            }
        }
    }

    public class UIContent
    {
        public string name;

        public UIContent(string name)
        {
            this.name = name;
        }
    }
}
