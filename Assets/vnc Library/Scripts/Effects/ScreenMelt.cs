using UnityEngine;
using UnityEngine.Events;

namespace vnc.Effects
{
    [ExecuteInEditMode]
    public class ScreenMelt : MonoBehaviour
    {
        public Texture frontTexture;
        public Material melt;
        [Range(0.01f, 2.0f)] public float m_speed;
        [Range(0.01f, 1.0f)] public float m_noiseScale;
        public UnityEvent OnMeltingEnd;

        float melting;
        float curtain;
        Texture2D noiseTex;

        public virtual void Start()
        {
            melting = 0;
            curtain = 0;
            noiseTex = Noise(scale: m_noiseScale);
        }

        public virtual void Update()
        {
            melt.SetFloat("_Melting", melting);
            melt.SetFloat("_Curtain", curtain);
            melt.SetTexture("_FrontTex", frontTexture);
            melt.SetTexture("_NoiseTex", noiseTex);

            melting = Mathf.Clamp01(melting + Time.deltaTime * m_speed);
            curtain = Mathf.Clamp01(curtain + Time.deltaTime * m_speed / 2);

            if (curtain >= 1.0f)
            {
                OnMeltingEnd.Invoke();

#if UNITY_EDITOR
                // repeat for debug
                melting = 0;
                curtain = 0;
                noiseTex = Noise(scale: m_noiseScale);
#endif
            }
        }

        public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (melt)
                Graphics.Blit(source, destination, melt);
        }

        /// <summary>
        /// Creates a random noise textures
        /// </summary>
        /// <param name="scale">256 * scale texture width</param>
        /// <returns>The randomized noise texture</returns>
        public Texture2D Noise(float scale)
        {
            var noiseTex = new Texture2D((int)(256 * scale), 1);
            Color[] pix = new Color[noiseTex.width * noiseTex.height];

            float y = 0.0F;
            while (y < noiseTex.height)
            {
                float x = 0.0F;
                while (x < noiseTex.width)
                {
                    pix[(int)(y * noiseTex.width + x)] = Color.white * Random.value;
                    x++;
                }
                y++;
            }
            noiseTex.SetPixels(pix);
            noiseTex.Apply();

            return noiseTex;
        }
    }
}
