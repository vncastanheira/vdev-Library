﻿Shader "vnc/Screen Melt"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_FrontTex("Texture", 2D) = "white" {}
		_NoiseTex ("Texture", 2D) = "white" {}
		_Melting("Melting", Range(0.0, 1.0)) = 0.5
		_Curtain("Curtain", Range(0.0, 1.0)) = 0.0
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

			sampler2D _MainTex;
			sampler2D _FrontTex;
			sampler2D _NoiseTex;
			float _Melting;
			float _Curtain;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);

				fixed4 noise = tex2D(_NoiseTex, i.uv);

				float value = (noise.r + noise.g + noise.b) / 3.0;
				float2 meltUV = i.uv + float2(0.0, (value * _Melting) + _Curtain);
				fixed4 front = tex2D(_FrontTex, meltUV);

				if (meltUV.y < 1)
				{
					front.rgb += float3(_Melting, 0.0, 0.0);
				}

				if (meltUV.y > 1)
				{
					front.rgb = col.rgb;
				}
				col = front;

				return col;
			}
			ENDCG
		}
	}
}
