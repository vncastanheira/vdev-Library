// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "vnc/Heat"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		_Refraction("Refraction", Range(0.0, 1.0)) = 0.1
		_HeatTex("Heat Texture", 2D) = "white" {}
		_DistortSpeed("Distort Speed", Range(0.0, 10.0)) = 1.0
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
		[HideInInspector] _RendererColor("RendererColor", Color) = (1,1,1,1)
		[HideInInspector] _Flip("Flip", Vector) = (1,1,1,1)
		[PerRendererData] _AlphaTex("External Alpha", 2D) = "white" {}
		[PerRendererData] _EnableExternalAlpha("Enable External Alpha", Float) = 0
	}

		SubShader
	{
		Tags
	{
		"Queue" = "Transparent"
		"IgnoreProjector" = "True"
		"RenderType" = "Transparent"
		"PreviewType" = "Plane"
		"CanUseSpriteAtlas" = "True"
	}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
	{
		CGPROGRAM
		#pragma vertex SpriteVert
		#pragma fragment frag
		#pragma target 2.0
		#pragma multi_compile_instancing
		#pragma multi_compile _ PIXELSNAP_ON
		#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
		#include "UnitySprites.cginc"
		
		sampler2D _HeatTex;
		float _Refraction;
		float _DistortSpeed;

		fixed4 frag(v2f IN) : SV_Target
		{
			float2 uvHeat = IN.texcoord;
			uvHeat.y -= _Time * _DistortSpeed;
			float4 heatColor = tex2D(_HeatTex, uvHeat);

			float heatvalue = ((heatColor.r + heatColor.g + heatColor.b) / 3) - 0.5;
			float2 uv = IN.texcoord;
			uv.x += heatvalue * _Refraction;
			fixed4 c = SampleSpriteTexture(uv) * IN.color;
			// full white offset to right
			// full black offset to left

			c.rgb *= c.a;
			return c;
		}

		ENDCG
	}
	}
}
