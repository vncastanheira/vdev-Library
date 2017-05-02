// Scrolling texture with background texture blending
Shader "vnc/ScrollingGridShader" {
	Properties {
		_MainTint ("Diffuse Tint", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Background ("Background (RGB)", 2D) = "white" {}
		_Cutoff ("Alpha cutoff", Range (0,1)) = 0.1
		_ScrollXSpeed ("X Scroll Speed", Range(0,100)) = 2
		_ScrollYSpeed ("Y Scroll Speed", Range(0,100)) = 2
	}
	SubShader {
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		LOD 200
		Cull Off
		Blend One SrcAlpha

		Pass {
			SetTexture[_Background] { combine texture }
		}

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard alpha:fade

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		fixed4 _MainTint;
		fixed _ScrollXSpeed;
		fixed _ScrollYSpeed;
		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		
		void surf (Input IN, inout SurfaceOutputStandard o) 
		{
			// Create a separate variable to store our UVs
			// before we pass them to the text2d() function
			fixed2 scrolledUV = IN.uv_MainTex;
				
			fixed xScrollValue = _ScrollXSpeed * _Time;
			fixed yScrollValue = _ScrollYSpeed * _Time;

			// Apply the final UV offset
			scrolledUV += fixed2(xScrollValue, yScrollValue);

			// Apply textures and tint
			half4 c = tex2D(_MainTex, scrolledUV);

			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
}
