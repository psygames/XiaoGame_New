using System;
using Core;
public class Time
{
    public static float deltaTime
    {
        get
        {
            if (s_updater == null)
                return 0;
            return s_updater.deltaTime;
        }
    }

    public static float time
    {
        get
        {
            if (s_updater == null)
                return 0;
            return s_updater.time;
        }
    }

    private static Updater s_updater = null;
    public static void SetUpdater(Updater updater)
    {
        s_updater = updater;
    }
}
