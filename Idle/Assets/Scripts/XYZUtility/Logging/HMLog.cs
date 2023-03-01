using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HM
{
    public static class HMLog
    {
        public enum LogLevel
        {
            Error,
            Warning,
            Info,
            Debug,
            Verbose,
        }
        #region FIELDS
        private static LogLevel _logLevel = LogLevel.Debug;

#endregion

        #region PROPERTIES
        private static bool Enabled { get; set; } = true;

        public static LogType filterLogType { get; set; }
        public static ILogHandler logHandler { get; set; }
        public static LogLevel logLevel
        {
            get { return _logLevel; }
            set { _logLevel = value; }
        }
        #endregion

        #region PUBLIC METHODS
        [System.Diagnostics.Conditional("HMLOGERROR")]
        [System.Diagnostics.Conditional("HMLOGWARNING")]
        [System.Diagnostics.Conditional("HMLOGINFO")]
        [System.Diagnostics.Conditional("HMLOGDEBUG")]
        [System.Diagnostics.Conditional("HMLOGVERBOSE")]
        public static void LogError(object message, params object[] objects)
        {
            LogError(null,message,objects);
        }

        [System.Diagnostics.Conditional("HMLOGERROR")]
        [System.Diagnostics.Conditional("HMLOGWARNING")]
        [System.Diagnostics.Conditional("HMLOGINFO")]
        [System.Diagnostics.Conditional("HMLOGDEBUG")]
        [System.Diagnostics.Conditional("HMLOGVERBOSE")]
        public static void LogError(Object context,object message, params object[] objects)
        {
            if (!Enabled)
                return;
            if (logLevel < LogLevel.Error)
                return;
            _LogError(context,"<color=red>" + (message as string) + "</color>", objects);
        }

        [System.Diagnostics.Conditional("HMLOGWARNING")]
        [System.Diagnostics.Conditional("HMLOGINFO")]
        [System.Diagnostics.Conditional("HMLOGDEBUG")]
        [System.Diagnostics.Conditional("HMLOGVERBOSE")]
        public static void LogWarning(object message, params object[] objects)
        {
            LogWarning(null, message, objects);
        }

        [System.Diagnostics.Conditional("HMLOGWARNING")]
        [System.Diagnostics.Conditional("HMLOGINFO")]
        [System.Diagnostics.Conditional("HMLOGDEBUG")]
        [System.Diagnostics.Conditional("HMLOGVERBOSE")]
        public static void LogWarning(Object context, object message, params object[] objects)
        {
            if (!Enabled)
                return;
            if (logLevel < LogLevel.Warning)
                return;
            _LogWarning(context, "<color=#FF9933>" + (message as string) + "</color>", objects);
        }

        [System.Diagnostics.Conditional("HMLOGINFO")]
        [System.Diagnostics.Conditional("HMLOGDEBUG")]
        [System.Diagnostics.Conditional("HMLOGVERBOSE")]
        public static void LogInfo(object message, params object[] objects)
        {
            LogInfo(null, message, objects);
        }

        [System.Diagnostics.Conditional("HMLOGINFO")]
        [System.Diagnostics.Conditional("HMLOGDEBUG")]
        [System.Diagnostics.Conditional("HMLOGVERBOSE")]
        public static void LogInfo(Object context, object message, params object[] objects)
        {
            if (!Enabled)
                return;
            if (logLevel < LogLevel.Info)
                return;
            Log(context, message, objects);
        }

        [System.Diagnostics.Conditional("HMLOGDEBUG")]
        [System.Diagnostics.Conditional("HMLOGVERBOSE")]
        public static void LogDebug(object message, params object[] objects)
        {
            LogDebug(null, message, objects);
        }

        [System.Diagnostics.Conditional("HMLOGDEBUG")]
        [System.Diagnostics.Conditional("HMLOGVERBOSE")]
        public static void LogDebug(Object context, object message, params object[] objects)
        {
            if (!Enabled)
                return;
            if (logLevel < LogLevel.Debug)
                return;
            Log(context, message, objects);
        }

        [System.Diagnostics.Conditional("HMLOGVERBOSE")]
        public static void LogVerbose(object message, params object[] objects)
        {
            LogVerbose(null, message, objects);
        }

        [System.Diagnostics.Conditional("HMLOGVERBOSE")]
        public static void LogVerbose(Object context, object message, params object[] objects)
        {
            if (!Enabled)
                return;
            if (logLevel < LogLevel.Debug)
                return;
            Log(context, message, objects);
        }

        private static void _LogError(Object context, object message, params object[] objects)
        {
            if (objects.Length > 0)
            {
                Debug.LogErrorFormat(context,message as string, objects);
            } else
            {
                Debug.LogError(message, context);
            }
        }

        private static void _LogWarning(Object context, object message, params object[] objects)
        {
            if (objects.Length > 0)
            {
                Debug.LogWarningFormat(context, message as string, objects);
            }
            else
            {
                Debug.LogWarning(message, context);
            }
        }

        private static void Log(Object context,object message, params object[] objects)
        {
            var msg = $"{DateTime.Now:HH:mm:ss.fff}|{message}";
            if (objects.Length > 0)
            {
                Debug.LogFormat(context, msg, objects);
            }
            else
            {
                Debug.Log(msg, context);
            }
        }

        [System.Diagnostics.Conditional("DEBUG")]
        public static void Assert(bool condition, object message = null, params object[] objects)
        {
            if (objects.Length > 0)
            {
                Debug.AssertFormat(condition, message as string, objects);
            }
            else
            {
                Debug.Assert(condition, message);
            }
        }
        #endregion

        #region PROTECTED METHODS
        #endregion

        #region PRIVATE METHODS
        #endregion

        #region STATIC METHODS
        #endregion

        #region PROFILER

        [System.Diagnostics.Conditional("ENABLE_PROFILER")]
        public static void BeginSample(string name)
        {
            UnityEngine.Profiling.Profiler.BeginSample(name);
        }

        [System.Diagnostics.Conditional("ENABLE_PROFILER")]
        public static void EndSample()
        {
            UnityEngine.Profiling.Profiler.EndSample();
        }
        #endregion
    }
}