using UnityEngine;
using System.Collections;

namespace vnc.Effects
{
    [ExecuteInEditMode]
    public class CameraEffect : MonoBehaviour
    {
	    public Material material;

	    void OnRenderImage(RenderTexture source, RenderTexture destination)
	    {
		    Graphics.Blit(source, destination, material);
	    }

    }
}

