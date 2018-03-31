using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vnc.Core
{
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour {

        private static T singleton = null;

        public static T Singleton
        {
            get
            {
                if (singleton == null)
                    singleton = FindObjectOfType<T>();

                if (singleton == null)
                    throw new System.NullReferenceException();

                return singleton;
            }

            protected set { singleton = value; }
        }
    }
}

