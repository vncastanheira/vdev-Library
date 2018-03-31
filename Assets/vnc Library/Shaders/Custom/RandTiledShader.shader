Shader "Custom/RandTiledShader"
{
	Properties
	{
		_LutTex("LUT", 2D) = "white" {}
		_Tex1("Tile 1", 2D) = "white" {}
		_Tex2("Tile 2", 2D) = "white" {}
		_Tex3 ("Tile 3", 2D) = "white" {}
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

			sampler2D _LutTex;
			float4 _LutTex_TexelSize;
			float _TileRepeats;

			sampler2D _Tex1;
			sampler2D _Tex2;
			sampler2D _Tex3;
			float4 _Tex1_ST;
			float4 _Tex2_ST;
			float4 _Tex3_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _Tex1);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// by KillaMaaki
				// note that Unity "magically" fills out this _LutTex_TexelSize property with ( 1/width, 1/height, width, height)
				float2 lut_uv = i.uv * _LutTex_TexelSize.xy; 

				float4 lutWeights = tex2D(_LutTex, lut_uv);

				float4 finalCol =
					(tex2D(_Tex1, i.uv) * lutWeights.r) +
					(tex2D(_Tex2, i.uv) * lutWeights.g) +
					(tex2D(_Tex3, i.uv) * lutWeights.b);

				return finalCol;
			}
			ENDCG
		}
	}
}
