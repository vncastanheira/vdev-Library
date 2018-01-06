Shader "vnc/Texture Blend"
{
	Properties
	{
		_UpperTex("Upper Texture", 2D) = "white" {}
		_LowerTex ("Lower Texture", 2D) = "white" {}
		_Blend("Blend", Range(0.0, 1.0)) = 0.0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _UpperTex;
			sampler2D _LowerTex;
			float _Blend;

			float4 _UpperTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _UpperTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 upper = tex2D(_UpperTex, i.uv);
				fixed4 lower = tex2D(_LowerTex, i.uv);

				//float4 upper = float4(col_up.r, col_up.g, col_up.b, col_up.a - _AlphaBlend);
				//float4 lower = float4(col_low.r, col_low.g, col_low.b, _AlphaBlend);

				float4 result = (upper * (1 - _Blend)) + (lower * _Blend);
			
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col_up);
				return result;
			}
			ENDCG
		}
	}
}
