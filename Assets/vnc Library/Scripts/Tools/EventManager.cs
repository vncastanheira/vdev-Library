using UnityEngine.Events;
using System.Collections.Generic;
using vnc.Core;

namespace vnc.Tools
{
				public class EventManager : Manager<EventManager>
				{
								private Dictionary<string, UnityEvent> eventDictionary;

								void Start()
								{
												eventDictionary = new Dictionary<string, UnityEvent>();
								}

								public static void StartListening(string eventName, UnityAction listener)
								{
												UnityEvent thisEvent = null;
												if (Singleton.eventDictionary.TryGetValue(eventName, out thisEvent))
												{
																thisEvent.AddListener(listener);
												}
												else
												{
																thisEvent = new UnityEvent();
																thisEvent.AddListener(listener);
																Singleton.eventDictionary.Add(eventName, thisEvent);
												}
								}

								public static void StopListening(string eventName, UnityAction listener)
								{
												if (Singleton == null) return;
												UnityEvent thisEvent = null;
												if (Singleton.eventDictionary.TryGetValue(eventName, out thisEvent))
												{
																thisEvent.RemoveListener(listener);
												}
								}

								public static void TriggerEvent(string eventName)
								{
												UnityEvent thisEvent = null;
												if (Singleton.eventDictionary.TryGetValue(eventName, out thisEvent))
												{
																thisEvent.Invoke();
												}
								}
				}
}
