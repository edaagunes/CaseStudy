using UnityEngine;

namespace Blended
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        // Static instance of the class
        private static volatile T _instance;

        private bool _isInitialized;

        // Property to access the singleton instance
        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = FindObjectOfType(typeof(T)) as T;
                if (_instance != null && !_instance._isInitialized) Instance.Initialize();
                return _instance;
            }
        }

        // Method to initialize the instance (can be overridden)
        protected virtual void Initialize()
        {
            _isInitialized = true;
        }
    }
}