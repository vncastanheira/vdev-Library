using UnityEngine;

namespace vnc.Effects
{
    [ExecuteInEditMode]
    public class CameraEffect : MonoBehaviour
    {
        public Material material;

        public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if(material == null)
            {
                Graphics.Blit(source, destination);
                return;
            }

            Graphics.Blit(source, destination, material);
        }
    }
}
