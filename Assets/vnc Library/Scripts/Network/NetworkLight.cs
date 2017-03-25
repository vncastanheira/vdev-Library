using UnityEngine;
using UnityEngine.Networking;

namespace vnc.Network
{
    public class NetworkLight : NetworkBehaviour
    {
        [SerializeField] Light flashLight;
        [SerializeField, SyncVar, HideInInspector] bool lightOn;
        public string ButtonCommand;

        void Update()
        {
            if (Input.GetButtonDown(ButtonCommand))
            {
                CmdToggleLight();
            }
        }

        [Command]
        public void CmdToggleLight()
        {
            RpcSwitchLight();
        }


        [ClientRpc]
        public void RpcSwitchLight()
        {
            lightOn = !lightOn;

            if (lightOn)
            {
                flashLight.intensity = 10.0f;
            }
            else
            {
                flashLight.intensity = 0;
            }
        }
    }
}
