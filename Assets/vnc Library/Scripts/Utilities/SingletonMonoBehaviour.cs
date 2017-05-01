using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vnc.Utilities
{
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour {

        public static T Singleton;

        /// <summary> Define singleton creation</summary>
        public abstract void CreateSingleton();
    }
}

