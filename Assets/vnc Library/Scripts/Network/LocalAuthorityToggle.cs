using UnityEngine.Events;
using UnityEngine.Networking;

namespace vnc.Network
{
    /// <summary>
    /// Enable GameObjects if this entity is local
    /// </summary>
    public class LocalAuthorityToggle : NetworkBehaviour
    {
        public ToggleEvent OnLocalEnable;
        public ToggleEvent OnLocalDisable;

        void Start()
        {
            OnLocalEnable.Invoke(isLocalPlayer);
            OnLocalDisable.Invoke(!isLocalPlayer);
        }
    }

    [System.Serializable]
    public sealed class ToggleEvent : UnityEvent<bool> { }
}
