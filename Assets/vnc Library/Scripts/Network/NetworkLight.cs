using UnityEngine;
using UnityEngine.Networking;

namespace vnc.Network
{
    public class NetworkLight : NetworkBehaviour
    {
        [SerializeField] Light flashLight;
        [SerializeField, SyncVar] bool lightOn;
        [Range(0.0f, 8.0f)] public float MaxIntensity;
        public string ButtonCommand;

        public bool isLightOn
        {
            get
            {
                return lightOn;
            }
        }

        public virtual void UpdateLight()
        {
            if (!isLocalPlayer)
                return;

            if (Input.GetButtonDown(ButtonCommand))
            {
                lightOn = !lightOn;
                CmdToggleLights(lightOn);
            }
        }

        [Command]
        public void CmdToggleLights(bool isOn)
        {
            RpcSwitchLight(isOn);
        }


        [ClientRpc]
        public void RpcSwitchLight(bool isOn)
        {
            if (isOn)
            {
                flashLight.intensity = MaxIntensity;
            }
            else
            {
                flashLight.intensity = 0;
            }
        }
    }
}
