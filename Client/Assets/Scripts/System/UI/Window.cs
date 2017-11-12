using System;
using UnityEngine;

 namespace RedStone.UI
{
    /// <summary>
    /// Image is a textured element in the UI hierarchy.
    /// </summary>
    
    public abstract class Window : UnityEngine.UI.Image, IEventHandler
    {
        private static readonly string[] EmptyList = new string[0];
        private string[] m_Dependencies;
        private UIRoot m_Root;
        public UIRoot root
        {
            get { return m_Root; }
        }
        
        public virtual string storageData
        {
            get { return string.Empty; }
            set { }
        }
        public virtual string[] dependencies
        {
            get { return m_Dependencies != null ? m_Dependencies : EmptyList; }
            set { m_Dependencies = value; }
        }
        /// <summary>
        /// This function is always called before any Start functions and also just after a prefab is instantiated. 
        /// (If a GameObject is inactive during start up Awake is not called until it is made active.)
        /// </summary>
        protected override void Awake()
        {
        }
        /// <summary>
        /// (only called if the Object is active): This function is called just after the object is enabled. This happens when a MonoBehaviour instance is created, such as when a level is loaded or a GameObject with the script component is instantiated.
        /// </summary>
        protected override void OnEnable()
        {
        }

        /// <summary>
        /// Start is called before the first frame update only if the script instance is enabled.
        /// </summary>
        protected override void Start()
        {
            base.Start();
            m_Root = GetComponentInParent<UIRoot>();
        }

        /// <summary>
        /// Update is called once per frame. It is the main workhorse function for frame updates.
        /// </summary>
        protected virtual void Update()
        {
        }

        /// <summary>
        /// This function is called when the behaviour becomes disabled or inactive.
        /// </summary>
        protected override void OnDisable()
        {
        }

        /// <summary>
        /// This function is called after all frame updates for the last frame of the object’s existence (the object might be destroyed in response to Object.Destroy or at the closure of a scene).
        /// </summary>
        protected override void OnDestroy()
        {
        }

        /// <summary>
        /// 依赖的资源装载完成
        /// </summary>
        protected virtual void OnResourceWasLoaded()
        {

        }

        /// <summary>
        /// 处理来自控件的事件
        /// </summary>
        /// <param name="sender">事件字符串</param>
        /// <param name="args">事件参数</param>
        public virtual void OnEvent(string sender, params object[] args)
        {
            Debug.LogWarning("GOT:" + sender);
        }

        /// <summary>
        /// 更新多囯语言文本
        /// </summary>
        protected void UpdateComponents(params object[] dataObjects)
        {
            foreach (Text text in gameObject.GetComponentsInChildren<Text>(true))
            {
                text.text = LT.Get(text.textKey, dataObjects);
            }
        }
    }
}
