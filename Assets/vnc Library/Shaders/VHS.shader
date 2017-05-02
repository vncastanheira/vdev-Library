Shader "Custom/VHS"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Magnitude ("Magnitude", Range (0.0001, 1)) = 0.0001
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

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			float rand(float2 co)
{
    float a = 12.9898;
     float b = 78.233;
     float c = 43758.5453;
     float dt= dot(co.xy ,float2(a,b));
     float sn= fmod(dt,3.14);
    return frac(sin(sn) * c);
}
			
			sampler2D _MainTex;
			float _Magnitude;

			fixed4 frag (v2f i) : SV_Target
			{
				
					float2 uv = i.uv;
    
	// Flip Y Axis
	//uv.y = -uv.y; This was causing issues
	
	float magnitude = _Magnitude;
	
	
	// Set up offset
	float2 offsetRedUV = uv;
	offsetRedUV.x = uv.x + rand(float2(_Time[1]*0.03,uv.y*0.42)) * 0.001;
	offsetRedUV.x += sin(rand(float2(_Time[1]*0.2, uv.y)))*magnitude;
	
	float2 offsetGreenUV = uv;
	offsetGreenUV.x = uv.x + rand(float2(_Time[1]*0.004,uv.y*0.002)) * 0.004;
	offsetGreenUV.x += sin(_Time[1]*9.0)*magnitude;
	
	float2 offsetBlueUV = uv;
	offsetBlueUV.x = uv.y;
	offsetBlueUV.x += rand(float2(cos(_Time[1]*0.01),sin(uv.y)));
	
	// Load Texture
	float r = tex2D(_MainTex, offsetRedUV).r;
	float g = tex2D(_MainTex, offsetGreenUV).g;
	float b = tex2D(_MainTex, uv).b;
	
	return float4(r,g,b,0);
			}
			ENDCG
		}
	}
}
