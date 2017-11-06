using System;
using System.Collections.Generic;
using System.Text;

namespace Coolfish.System
{
    public class GameLoopEntry : SingletonBehaviour<GameLoopEntry>
    {
        public delegate void EventHandler();
        public static event EventHandler ApplicatinQuit;
        public static event EventHandler LevelWasLoaded;

        public static event EventHandler OnUpdate;
        public static event EventHandler OnFixedUpdate;
        public static event EventHandler OnLateUpdate;

        private void FixedUpdate()
        {
            if (OnFixedUpdate != null) OnFixedUpdate();
        }
        private void Update()
        {
            if (OnUpdate != null) OnUpdate();
        }

        private void LateUpdate()
        {
            if (OnLateUpdate != null) OnLateUpdate();
        }

        private void OnLevelWasLoaded(int level)
        {
            if (LevelWasLoaded != null) LevelWasLoaded();
        }
        private void OnApplicationQuit()
        {
            if (ApplicatinQuit != null) ApplicatinQuit();
        }

    }
}
