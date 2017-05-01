using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderScaling : MonoBehaviour {

    public Material modifier;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, modifier);
    }
}
