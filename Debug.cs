﻿using System;

namespace KRPG2
{
    public static class Debug
    {
        public static void LogError(this KRPG2 krpg2, object message)
        {
            krpg2.Logger.Error(message);
            Console.Write(message);
        }

        public static void LogWarning(this KRPG2 krpg2, object message)
        {
            krpg2.Logger.Warn(message);
            Console.Write(message);
        }

        public static void Log(this KRPG2 krpg2, object message)
        {
            krpg2.Logger.Info(message);
            Console.Write(message);
        }
    }
}
