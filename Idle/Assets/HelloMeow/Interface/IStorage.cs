namespace HM.Interface
{
    public interface IStorage
    {
        void SetInt(string key, int value);
        int GetInt(string key, int defaultValue = 0);
        int AccumGetInt(string key, int start = 0, int step = 1);

        void SetFloat(string key, float value);
        float GetFloat(string key, float defaultValue = 0.0f);

        void SetString(string key, string value);
        string GetString(string key, string defaultValue = "");
        bool HasString(string key);

        void SetBool(string key, bool value);
        bool GetBool(string key, bool defaultValue = false);

        string UniqueId { get; set; }

        bool IsDirty{ get; set; }

        void ClearAll();
    }
}