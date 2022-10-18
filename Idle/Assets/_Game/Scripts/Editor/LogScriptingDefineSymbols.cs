/**
* @author [BoLuo]
* @email [tktetb@163.com]
* @create date 2021-01-5
* @modify date 2021-01-5
* @desc [日志脚本宏定义]
*/

using UnityEditor;

namespace NewLife.EditorTool
{
    /// <summary>
    /// 日志脚本宏定义。
    /// </summary>
    public static class LogScriptingDefineSymbols
    {
        private const string EnableVerboseLogScriptingDefineSymbol = "HMLOGVERBOSE";
        private const string EnableDebugLogScriptingDefineSymbol = "HMLOGDEBUG";
        private const string EnableInfoLogScriptingDefineSymbol = "HMLOGINFO";
        private const string EnableWarningLogScriptingDefineSymbol = "HMLOGWARNING";
        private const string EnableErrorLogScriptingDefineSymbol = "HMLOGERROR";

        private static readonly string[] SpecifyLogScriptingDefineSymbols = new string[]
        {
            EnableVerboseLogScriptingDefineSymbol,
            EnableDebugLogScriptingDefineSymbol,
            EnableInfoLogScriptingDefineSymbol,
            EnableWarningLogScriptingDefineSymbol,
            EnableErrorLogScriptingDefineSymbol
        };

        /// <summary>
        /// 禁用所有日志脚本宏定义。
        /// </summary>
        [MenuItem("Tools/Log Scripting Define Symbols/Disable All Logs", false, 30)]
        public static void DisableAllLogs()
        {
            foreach (string specifyLogScriptingDefineSymbol in SpecifyLogScriptingDefineSymbols)
            {
                ScriptingDefineSymbols.RemoveScriptingDefineSymbol(specifyLogScriptingDefineSymbol);
            }
        }

        /// <summary>
        /// 开启Verbose及以上级别的日志脚本宏定义。
        /// </summary>
        [MenuItem("Tools/Log Scripting Define Symbols/Enable Verbose And Above Logs", false, 32)]
        public static void EnableVerboseAndAboveLogs()
        {
            DisableAllLogs();
            ScriptingDefineSymbols.AddScriptingDefineSymbol(EnableVerboseLogScriptingDefineSymbol);
        }

        /// <summary>
        /// 开启调试及以上级别的日志脚本宏定义。
        /// </summary>
        [MenuItem("Tools/Log Scripting Define Symbols/Enable Debug And Above Logs", false, 33)]
        public static void EnableDebugAndAboveLogs()
        {
            DisableAllLogs();
            ScriptingDefineSymbols.AddScriptingDefineSymbol(EnableDebugLogScriptingDefineSymbol);
        }

        /// <summary>
        /// 开启信息及以上级别的日志脚本宏定义。
        /// </summary>
        [MenuItem("Tools/Log Scripting Define Symbols/Enable Info And Above Logs", false, 34)]
        public static void EnableInfoAndAboveLogs()
        {
            DisableAllLogs();
            ScriptingDefineSymbols.AddScriptingDefineSymbol(EnableInfoLogScriptingDefineSymbol);
        }

        /// <summary>
        /// 开启警告及以上级别的日志脚本宏定义。
        /// </summary>
        [MenuItem("Tools/Log Scripting Define Symbols/Enable Warning And Above Logs", false, 35)]
        public static void EnableWarningAndAboveLogs()
        {
            DisableAllLogs();
            ScriptingDefineSymbols.AddScriptingDefineSymbol(EnableWarningLogScriptingDefineSymbol);
        }

        /// <summary>
        /// 开启错误及以上级别的日志脚本宏定义。
        /// </summary>
        [MenuItem("Tools/Log Scripting Define Symbols/Enable Error And Above Logs", false, 36)]
        public static void EnableErrorAndAboveLogs()
        {
            DisableAllLogs();
            ScriptingDefineSymbols.AddScriptingDefineSymbol(EnableWarningLogScriptingDefineSymbol);
        }

    }
}
