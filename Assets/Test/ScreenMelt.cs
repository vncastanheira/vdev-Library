using UnityEngine;

[ExecuteInEditMode]
public class ScreenMelt : MonoBehaviour
{
    public Texture frontTexture;
    public Material melt;
    [Range(0.01f, 2.0f)] public float m_speed;
    float melting;
    float curtain;

    private void Start()
    {
        melting = 0;
        curtain = 0;
    }

    private void Update()
    {
        melt.SetFloat("_Melting", melting);
        melt.SetFloat("_Curtain", curtain);
        melt.SetTexture("_FrontTex", frontTexture);

        melting = Mathf.Clamp01(melting + Time.deltaTime * m_speed);
        curtain = Mathf.Clamp01(curtain + Time.deltaTime * m_speed);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (melt)
            Graphics.Blit(source, destination, melt);
    }
}
