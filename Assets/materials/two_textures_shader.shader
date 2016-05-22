Shader "cubie/two_textures_shader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_UpperTex ("Albedo (RGB)", 2D) = "white" {}
		_LowerTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _UpperTex;
		sampler2D _LowerTex;

		struct Input {
			float2 uv_UpperTex;
			float2 uv_LowerTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_UpperTex, IN.uv_UpperTex) * _Color;
			fixed4 c2 = tex2D(_LowerTex, IN.uv_LowerTex) * _Color;
			o.Albedo = c.rgb*c.a + c2.rgb*(1.0f-c.a);
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
