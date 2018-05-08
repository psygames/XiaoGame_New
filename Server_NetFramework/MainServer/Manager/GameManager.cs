using System.Collections;
using System.Timers;
using Plugins;
using Core;
namespace RedStone
{
    public class GameManager : Core.Singleton<GameManager>, Core.IUpdateable
    {
        public EventManager eventManager { get; private set; }
        private Updater m_updater = new Updater();

        public void Start()
        {
            eventManager = new EventManager();

            DBManager.CreateInstance().Init("mongodb://localhost:27017", "xiao_game");
            NetworkManager.CreateInstance().Init();
            ProxyManager.CreateInstance().Init();

            // 
            NetworkManager.instance.serverForBattle.Start();
            Logger.LogInfo("网络监听（战场） 已启动");
            NetworkManager.instance.serverForClient.Start();
            Logger.LogInfo("网络监听（客户端） 已启动");


            m_updater.Start();
            m_updater.Add(this);
            m_updater.Add(ProxyManager.instance);

            Time.SetUpdater(m_updater);
        }

        private void OnDestroy()
        {
            m_updater.Clear();
        }

        private void OnApplicationQuit()
        {
        }

        public void Update()
        {
        }
    }
}