using UnityEngine;

namespace vnc.Effects
{
				[ExecuteInEditMode, RequireComponent(typeof(Camera))]
				public class CrosslinesEffect : CameraEffect
				{
								public override void OnRenderImage(RenderTexture source, RenderTexture destination)
								{
												material.SetFloat("_HorizFac", Screen.height);
												material.SetFloat("_VertFac", Screen.width);

												base.OnRenderImage(source, destination);
								}
				}
}
