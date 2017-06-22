using UnityEngine;
using UnityEngine.Events;

namespace vnc.Utilities
{
				/// <summary>
				/// Trigger UnityEvents when entering, staying or exiting trigger colliders
				/// (not for 2D colliders)
				/// </summary>
				public class TriggerEvent : MonoBehaviour
				{
								public UnityEvent OnTriggerEnterEvent;
								public UnityEvent OnTriggerStayEvent;
								public UnityEvent OnTriggerExitEvent;

								private void OnTriggerEnter(Collider other)
								{
												OnTriggerEnterEvent.Invoke();
								}

								private void OnTriggerStay(Collider other)
								{
												OnTriggerStayEvent.Invoke();
								}

								private void OnTriggerExit(Collider other)
								{
												OnTriggerExitEvent.Invoke();
								}
				}
}
