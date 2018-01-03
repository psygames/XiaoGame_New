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
            RegisterPreload();

            //TODO:这块需要放到状态机里面，并且需要处理Prefab的卸载or重新加载，事件的注销和注册
            PreLoad();

            //TODO: 由于MessageBox 的特殊性质，所以MessageBox放在此处
            PopShow<MessageBoxView>();
            PopShow<Toast>();
        }

        public void PreLoad()
        {
            foreach (var kv in m_prefabs)
            {
                Load(kv.Value);
            }
        }

        public void Load(string uiPath)
        {
            Object obj = Resources.Load(MyPath.RES_UI + uiPath);
            GameObject go = Object.Instantiate(obj) as GameObject;
            ViewBase _base = go.GetComponent<ViewBase>();
            if (_base == null || m_views.ContainsKey(_base.GetType().ToString()))
            {
                Debug.LogError(uiPath + " Get ViewBase is " + (_base == null ? "NULL" : "Repeated"));
            }
            else
            {
                m_views.Add(_base.GetType().ToString(), _base);
                go.transform.SetParent(uiRoot, false);
                go.SetActive(false);
                _base.OnInit();
            }
        }

        public void Unload<T>() where T : ViewBase
        {
            //TODO: Unload Not Clear
            m_stack.Clear();
            var view = GetView<T>();
            if (view == null)
                Debug.LogError(typeof(T).ToString() + " View is NULL");
            else
            {
                m_views.Remove(typeof(T).ToString());
                GameObject.DestroyImmediate(view.gameObject);
            }
        }

        public void RegisterPreload()
        {
            AddPreloadUI("Hall/Home");
            AddPreloadUI("Common/Loading");
            AddPreloadUI("Common/MessageBox");
            AddPreloadUI("Common/Toast");
        }

        public void AddPreloadUI(string name, string prefabPath = null)
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
            Debug.Log("show view: " + typeof(T));
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
