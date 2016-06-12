Shader "Custom/GravityShader" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB) Trans (A)", 2D) = "white" {}
		_CutTex ("Cutout (A)", 2D) = "white" {}
		_Cutoff("Alpha cutoff", Range(0,1)) = 0.5
		_Speed("Speed", Range(0.1, 3)) = 1
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent" "IgnoreProjector"="True"}
		LOD 200

		Cull Off

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert alpha

		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _CutTex;
		fixed4 _Color;
		float _Cutoff;
		float _Speed;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {

			float speed = _Time.y * _Speed;
			speed = fmod(speed, 2);
			//IN.uv_MainTex += 0.5*speed;
			IN.uv_MainTex /= speed;
			IN.uv_MainTex += 0.5;


			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			float ca = tex2D(_CutTex, IN.uv_MainTex).a;
			o.Albedo = c.rgb;
			if (ca > _Cutoff)
				o.Alpha = c.a-speed/2;
			else
				o.Alpha = 0;
		}
		ENDCG
	}
}
