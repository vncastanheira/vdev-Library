// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "vnc/Crosslines"
{
				Properties
				{
								_MainTex("Texture", 2D) = "white" {}
				_Color("Main Color", Color) = (1,1,1,1)
								_HorizFac("Horizontal Factor", Float) = 10000
								_VertFac("Vertical Factor", Float) = 10000
				}
								SubShader
				{
								// No culling or depth
								Cull Off ZWrite Off ZTest Always

								Pass
				{
								CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

								struct appdata
				{
								float4 vertex : POSITION;
								float2 uv : TEXCOORD0;
				};

				struct v2f
				{
								float2 uv : TEXCOORD0;
								float4 vertex : SV_POSITION;
				};

				v2f vert(appdata v)
				{
								v2f o;
								o.vertex = UnityObjectToClipPos(v.vertex);
								o.uv = v.uv;
								return o;
				}

				sampler2D _MainTex;
				float4 _Color;
				float _HorizFac;
				float _VertFac;

				fixed4 frag(v2f i) : SV_Target
				{
								fixed4 col = tex2D(_MainTex, i.uv);
				if ((fmod(floor(i.uv.x * _VertFac), 2) == 0) && (fmod(floor(i.uv.y * _HorizFac), 2) == 0))
				{
								col.r *= (_Color.r + (1 - _Color.a));
								col.g *= (_Color.g + (1 - _Color.a));
								col.b *= (_Color.b + (1 - _Color.a));
				}

				return col;
				}
								ENDCG
				}
				}
}
