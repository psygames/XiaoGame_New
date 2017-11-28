using UnityEngine;
using System.Collections;
namespace RedStone
{
    public class GameManager : Core.SingletonBehaviour<GameManager>
    {
        protected override void Awake()
        {
            base.Awake();

            CreateInstance();

            Init();
        }

        private void Start()
        {
            ChangeGameState<LoginState>();
        }

        private void CreateInstance()
        {
            NetworkManager.CreateInstance();
            ProxyManager.CreateInstance();
            EventManager.CreateInstance();
            UIManager.CreateInstance();
        }

        private void Init()
        {
            DeviceID.Instance.Init();
            NetworkManager.instance.Init();
            ProxyManager.instance.Init();
            UIManager.instance.Init();
        }

        private void OnGUI()
        {
            return;
            GUIStyle fontStyle = new GUIStyle();
            fontStyle.normal.background = null;    //设置背景填充  
            fontStyle.normal.textColor = new Color(1, 0, 0);   //设置字体颜色  
            fontStyle.fontSize = 20;       //字体大小  
            GUI.Label(new Rect(0, 0, 200, 200), UUID.DEVICE, fontStyle);
        }

        private void Update()
        {
            ProxyManager.instance.Update();
            NetworkManager.instance.Update();


            if (gameState != null)
                gameState.Update();
        }

        private void OnDestroy()
        {
            ProxyManager.instance.Destroy();
            EventManager.instance.ClearAll();
        }

        private void OnApplicationQuit()
        {
            NetworkManager.instance.Close();
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