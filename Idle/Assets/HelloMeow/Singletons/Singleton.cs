/**********************************************************
// Author   : K.(k79k06k02k)
// FileName : Singleton.cs
// Reference: http://wiki.unity3d.com/index.php/Singleton
**********************************************************/

/*
 *  Usage Example
 *  ********************************************
    using UnityEngine;

    public class TestClass : Singleton<TestClass>
    {
        public void Hello()
        {
            Debug.Log("Hello");
        }
    }
 *  ********************************************
 *  TestClass.Instance.Hello();
 *  ********************************************
 */

namespace HM {
    public class Singleton<T> where T : class, new() {
        private static T _instance;

        private static object _lock = new object();

        public static T Instance {
            get {

                lock (_lock) {
                    if (_instance == null) {
                        _instance = new T();
                    }

                    return _instance;
                }
            }
        }

        public static void SetInstance(T ins)
        {
            lock (_lock)
            {
                _instance = ins;
            }
        }
    }
}
