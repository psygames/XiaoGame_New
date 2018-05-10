using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RedStone
{
    public class GameManager : Core.SingletonBehaviour<GameManager>
    {
        private EventManager m_eventMgr = new EventManager();
        public EventManager eventManager { get { return m_eventMgr; } }
        public GameObject UIRoot;

        public string serverAddress { get; private set; }
        public string UUIDSuffix { get; private set; }

        public const string PREFS_UUID_SUFFIX = "GLOBAL_UUID_SUFFIX";
        public const string PREFS_SERVER_ADDRESS = "GLOBAL_SERVER_ADDRESS";
        public const string DEFAULT_SERVER_ADDRESS = "47.100.28.149";

        protected override void Awake()
        {
            base.Awake();

            serverAddress = DEFAULT_SERVER_ADDRESS;
            UUIDSuffix = "";
#if UNITY_EDITOR
            var customAddress = LocalDataUtil.Get(PREFS_SERVER_ADDRESS, "");
            if (!string.IsNullOrEmpty(customAddress))
                serverAddress = customAddress;

            var uuidSuffix = LocalDataUtil.Get(PREFS_UUID_SUFFIX, "");
            if (!string.IsNullOrEmpty(uuidSuffix))
                UUIDSuffix = uuidSuffix;

#endif
            return;
        }

        private void Start()
        {
            CreateInstance();
            Init();

            ChangeGameState<HallLoadingState>();
        }

        private void CreateInstance()
        {
            m_eventMgr = new EventManager();
            NetworkManager.CreateInstance();
            ProxyManager.CreateInstance();
            UIManager.CreateInstance();
            TaskManger.CreateInstance();

            TableManager.CreateInstance();
            Localization.CreateInstance();
        }

        private void Init()
        {
            TableManager.instance.Init();
            Localization.instance.SetLanguage(2);
            DeviceID.Instance.Init();
            NetworkManager.instance.Init();
            ProxyManager.instance.Init();
            UIManager.instance.Init();
            TaskManger.instance.Init();

        }

        private void Update()
        {
            ProxyManager.instance.Update();
            NetworkManager.instance.Update();
            TaskManger.instance.Update();

            if (gameState != null)
                gameState.Update();
        }

        private void OnDestroy()
        {
            ProxyManager.instance.Destroy();
            eventManager.ClearAll();
        }

        private void OnApplicationQuit()
        {
            NetworkManager.instance.Dispose();
        }



        // GameState
        public AbstractState gameState { get; private set; }

        public void ChangeGameState<T>() where T : AbstractState, new()
        {
            if (gameState != null)
                gameState.Leave();
            gameState = null;
            gameState = new T();
            gameState.Enter();
        }
    }
}