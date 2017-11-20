using System.Collections;
using System.Timers;
using Plugins;
using Core;
namespace RedStone
{
    public class GameManager : Core.Singleton<GameManager>
    {
        public EventManager eventManager { get; private set; }
        private Updater m_updater = new Updater();

        public void Start()
        {
            eventManager = new EventManager();
            m_updater.Start();

            DBManager.CreateInstance().Init("mongodb://localhost:27017", "xiao_game");
            NetworkManager.CreateInstance().Init();
            ProxyManager.CreateInstance().Init();

            // 
            NetworkManager.instance.serverForBattle.server.Start();
            Debug.LogInfo("网络监听（战场） 已启动");
            NetworkManager.instance.serverForClient.server.Start();
            Debug.LogInfo("网络监听（客户端） 已启动");
        }

        private void Update()
        {
        }

        private void OnDestroy()
        {
            m_updater.Clear();
        }

        private void OnApplicationQuit()
        {
        }
    }
}