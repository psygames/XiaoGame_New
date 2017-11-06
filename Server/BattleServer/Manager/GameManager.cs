using System.Collections;
using System.Timers;
using Plugins;
using Core;
namespace RedStone
{
    public class GameManager : Core.Singleton<GameManager>
    {
        private Updater m_updater = new Updater();
        public EventManager eventManager { get; private set; }

        public void Start()
        {
            m_updater.Start();
            eventManager = new EventManager();
            ServerManager.CreateInstance().Init();
            ProxyManager.CreateInstance().Init();

            // connect main server
            ProxyManager.instance.GetProxy<MainServerProxy>().Connenct();
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