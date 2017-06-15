using UnityEngine;

/// <summary>
/// Stretch the render texture on the screen. 
/// Useful for creating pixelated imaes
/// </summary>
public class RenderTextureStretch : MonoBehaviour
{

    public RenderTexture tex;

    private void OnGUI()
    {
        var screenRect = new Rect(0, 0, Screen.width, Screen.height);
        Graphics.DrawTexture(screenRect, tex);
    }
}
