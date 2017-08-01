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
																CreateSingleton();
												}

												if (!DestroyOnLoad)
																DontDestroyOnLoad(gameObject);

												OnAwake();
								}

								public override void CreateSingleton()
								{
												Singleton = gameObject.GetComponent<T>();
								}
								
								public virtual void OnAwake() { }
				}
}

