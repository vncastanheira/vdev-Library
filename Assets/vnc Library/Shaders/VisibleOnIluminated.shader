Shader "vnc/VisibleOnIluminated" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Transparency("Global Transparency", Range(0.0, 1.0)) = 1.0
	}
	SubShader {
		Tags 
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
		}
		LOD 200
		Cull Back
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf SimpleLambert alpha:fade

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		half4 LightingSimpleLambert(SurfaceOutput s, half3 lightDir, half atten) {
			half NdotL = dot(s.Normal, lightDir);
			half4 c;
			half4 result;
			result.rgb = s.Albedo * _LightColor0.rgb * (NdotL * atten);
			if (NdotL > 0.7) {
				c.rgb = result.rgb;
				c.a = s.Alpha;			
			}
			else {
				c.rgb = 0;
				c.a = 0;
			}
			return c;
		}

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		float _Transparency;
		half _Metallic;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutput o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a * _Transparency;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
