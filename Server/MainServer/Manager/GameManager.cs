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
            ServerManager.CreateInstance().Init();
            ProxyManager.CreateInstance().Init();

            //
            ServerManager.instance.MB.server.Start();
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