using UnityEngine;
using vnc.Utilities;

namespace vnc.Core
{
    public abstract class Manager<T> : SingletonMonoBehaviour<T> where T : MonoBehaviour
    {
        [Tooltip("Shold this manager be destroying on a new scene?")]
        public bool DestroyOnLoad = false;

        void Awake()
        {
            if (Singleton != null && Singleton != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Singleton = gameObject.GetComponent<T>();
            }

            if (!DestroyOnLoad)
                DontDestroyOnLoad(gameObject);

            OnAwake();
        }
        
        public virtual void OnAwake() { }
    }
}

