using System.Collections.Generic;
using HM.Interface;

namespace HM
{
    [System.Serializable]
    public class StoreMem : IStorage
    {
        #region FIELDS
        [UnityEngine.SerializeField]
        protected Dictionary<string, int> _ints = new Dictionary<string, int>();
        [UnityEngine.SerializeField]
        protected Dictionary<string, bool> _bools = new Dictionary<string, bool>();
        [UnityEngine.SerializeField]
        protected Dictionary<string, string> _strings = new Dictionary<string, string>();
        [UnityEngine.SerializeField]
        protected Dictionary<string, float> _floats = new Dictionary<string, float>();
        #endregion

        #region PROPERTIES
        public string UniqueId { get; set; }
        public bool IsDirty { get; set; }
        #endregion

        #region PUBLIC METHODS

        public void SetInt(string key, int v)
        {
            _ints.Remove(key);
            _ints.Add(key, v);
            IsDirty = true;
        }

        public int GetInt(string key, int defaultValue = 0)
        {
            if (_ints.ContainsKey(key))
            {
                return _ints[key];
            }
            return defaultValue;
        }

        public int AccumGetInt(string key, int start = 0, int step = 1)
        {
            var cur = GetInt(key, start);
            SetInt(key, cur + step);
            IsDirty = true;
            return cur;
        }

        public void SetFloat(string key, float v)
        {
            _floats.Remove(key);
            _floats.Add(key, v);
            IsDirty = true;
        }

        public float GetFloat(string key, float defaultValue = 0.0f)
        {
            if (_floats.ContainsKey(key))
            {
                return _floats[key];
            }
            return defaultValue;
        }

        public void SetString(string key, string v)
        {
            if (!string.IsNullOrEmpty(v))
            {
                _strings.Remove(key);
                _strings.Add(key, v);
            }
            else
            {
                _strings.Remove(key);
            }
            IsDirty = true;
        }

        public string GetString(string key, string defaultV = "")
        {
            if (_strings.ContainsKey(key))
            {
                return _strings[key];
            }
            else
            {
                return defaultV;
            }
        }

        public bool HasString(string key)
        {
            return _strings.ContainsKey(key);
        }

        public void SetBool(string key, bool v)
        {
            if (_bools.ContainsKey(key))
            {
                _bools.Remove(key);
            }
            _bools.Add(key, v);
            IsDirty = true;
        }

        public bool GetBool(string key, bool defaultV = false)
        {
            if (_bools.ContainsKey(key))
            {
                return _bools[key];
            }
            return defaultV;
        }

        public bool HasBool(string key)
        {
            if (_bools.ContainsKey(key))
            {
                return true;
            }
            return false;
        }

        public void ClearAll()
        {
            _bools.Clear();
            _ints.Clear();
            _floats.Clear();
            _strings.Clear();
            IsDirty = true;
        }

        #endregion

        #region PROTECTED METHODS
        #endregion

        #region PRIVATE METHODS
        #endregion

        #region STATIC METHODS
        #endregion
    }
}