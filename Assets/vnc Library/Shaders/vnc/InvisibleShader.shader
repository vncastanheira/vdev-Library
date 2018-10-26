Shader "vnc/Invisible " {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
	}
		SubShader{
			
			Tags { "RenderType" = "Opaque" "Queue" = "Transparent" }
			LOD 200
			Cull Back
			
			

			CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
			#pragma surface surf Standard fullforwardshadows

			// Use shader model 3.0 target, to get nicer looking lighting
			#pragma target 3.0

			sampler2D _MainTex;

			struct Input {
				float2 uv_MainTex;
			};

			half _Glossiness;
			half _Metallic;
			fixed4 _Color;

			// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
			// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
			// #pragma instancing_options assumeuniformscaling
			UNITY_INSTANCING_BUFFER_START(Props)
				// put more per-instance properties here
			UNITY_INSTANCING_BUFFER_END(Props)

			void surf(Input IN, inout SurfaceOutputStandard o) {
				// Albedo comes from a texture tinted by color
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
				o.Albedo = c.rgb;
				// Metallic and smoothness come from slider variables
				o.Metallic = _Metallic;
				o.Smoothness = _Glossiness;
				o.Alpha = c.a;
			}
			ENDCG

			GrabPass { "_GunTex" }

			Pass{
				Tags { "RenderType" = "Opaque" "Queue" = "Transparent" }
				ZWrite On
				ZTest Always

				CGPROGRAM
				#pragma fragment frag
				#pragma vertex vert
				#include "UnityCG.cginc"

				struct v2f
				{
					float4 grabPos : TEXCOORD0;
					float4 pos : SV_POSITION;
				};

				sampler2D _GunTex;

				v2f vert(appdata_base v) {
					v2f o;
					// use UnityObjectToClipPos from UnityCG.cginc to calculate 
					// the clip-space of the vertex
					o.pos = UnityObjectToClipPos(v.vertex);
					// use ComputeGrabScreenPos function from UnityCG.cginc
					// to get the correct texture coordinate
					o.grabPos = ComputeGrabScreenPos(o.pos);
					return o;
				}

				half4 frag(v2f i) : SV_Target
				{
					float4 col = tex2D(_GunTex, i.grabPos);
					return col;
				}

				ENDCG
			}
		}
			
		FallBack "Diffuse"
}
