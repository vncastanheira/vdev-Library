Shader "Custom/CRTDiffuse" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_MaskTex ("Mask texture", 2D) = "white" {}
		_maskBlend ("Mask blending", Float) = 0.5
		_maskSize ("Mask Size", Float) = 1
	}
	SubShader {
		Pass {
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#include "UnityCG.cginc"
		
			uniform sampler2D _MainTex;
			uniform sampler2D _MaskTex;
			
			fixed _maskBlend;
			fixed _maskSize;

			fixed4 frag (v2f_img i) : COLOR {
				fixed4 mask = tex2D(_MaskTex, i.uv * _maskSize);
				fixed4 base = tex2D(_MainTex, i.uv);
				return lerp(base, mask, _maskBlend	);
			}
			ENDCG
		}
	}
}