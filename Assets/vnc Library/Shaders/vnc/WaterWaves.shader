Shader "vnc/World/Water (Waves)"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_HeightTex("Height Map", 2D) = "white" {}
		_WaveSpeed("Wave Speed", Float) = 1.0
		_WaveIntensity("Wave Intensity", Float) = 1.0
		_Color("Color", Color) = (1,1,1,1)
		_WirlIntensity("Wirl Intensity", Range(0.0, 360.0)) = 20.0
		_WirlSpeed("Wirl Speed", Range(0, 3)) = 0.0
	}
		SubShader
	{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		LOD 100

		ZWrite Off
		Cull Off
		Blend SrcAlpha One

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
					// make fog work
			#pragma multi_compile_fog
			#pragma target 3.0

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
			float4 _MainTex_ST;

			sampler2D _HeightTex;
			float _WaveSpeed;
			float _WaveIntensity;

			float4 _Color;
			float _WirlIntensity;
			int _WirlSpeed;

			v2f vert(appdata v)
			{
				v2f o;
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				float2 heightUv = o.uv;
				heightUv.y += _Time * _WaveSpeed;
				float4 h = tex2Dlod(_HeightTex, float4(heightUv, 0.0, 0.0));
				float bump = (h.r + h.g + h.b) / 3;

				float4 vPos = v.vertex;
				vPos.y += bump * _WaveIntensity;
				o.vertex = UnityObjectToClipPos(vPos);

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				int speed = round(_WirlSpeed);
				float sine = sin((i.uv.y + abs(_Time[speed]))* _WirlIntensity);
				i.uv.x += sine * 0.01;

				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				col.rgb *= _Color.rgb;
				col.a = _Color.a;

				return col;
			}
			ENDCG
		}
	}
}
