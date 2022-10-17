using UnityEngine;

namespace HM.Extensions
{
    public static class PlayerPrefsEx
    {
        public static bool GetBool(string key, bool defaultValue = false)
        {
            return GetInt(key, defaultValue ? 1 : 0) == 1;
        }

        public static void SetBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }
        public static string GetString(string key, string fallback = "")
        {
            if (PlayerPrefs.HasKey(key))
            {
                return PlayerPrefs.GetString(key);
            }
            return fallback;
        }

        public static float GetFloat(string key, float fallback = 0f)
        {
            if (PlayerPrefs.HasKey(key))
            {
                return PlayerPrefs.GetFloat(key);
            }
            return fallback;
        }

        public static int GetInt(string key, int fallback = 0)
        {
            if (PlayerPrefs.HasKey(key))
            {
                return PlayerPrefs.GetInt(key);
            }
            return fallback;
        }

        public static int AccumGetInt(string key, int start = 0, int step = 1)
        {
            var cur = GetInt(key, start);
            PlayerPrefs.SetInt(key, cur + step);
            return cur;
        }
    }
}
