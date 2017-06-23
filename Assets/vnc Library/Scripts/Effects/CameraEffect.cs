using UnityEngine;

namespace vnc.Effects
{
				[ExecuteInEditMode]
				public class CameraEffect : MonoBehaviour
				{
								public Material material;

								public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
								{
												Graphics.Blit(source, destination, material);
								}
				}
}
