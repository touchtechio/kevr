Shader "Shaders101/alphaTexture"
{
//	Properties
//	{
//		_MainTex("Texture", 2D) = "white" {}
//	}
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_DetailTex("Texture", 2D) = "white" {}
		_ScanDistance("Scan Distance", float) = 0
		_ScanWidth("Scan Width", float) = 10
		_LeadSharp("Leading Edge Sharpness", float) = 10
		_LeadColor("Leading Edge Color", Color) = (1, 1, 1, 0)
		_MidColor("Mid Color", Color) = (1, 1, 1, 0)
		_TrailColor("Trail Color", Color) = (1, 1, 1, 0)
		_HBarColor("Horizontal Bar Color", Color) = (0.5, 0.5, 0.5, 0)
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"PreviewType" = "Plane"
		}
		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float4 ray : TEXCOORD1;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				float2 uv_depth : TEXCOORD1;
				float4 interpolatedRay : TEXCOORD2;
			};

			float4 _MainTex_TexelSize;
			float4 _CameraWS;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;

//				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
//				o.uv = v.uv.xy;
//				o.uv_depth = v.uv.xy;
//
//				#if UNITY_UV_STARTS_AT_TOP
//				if (_MainTex_TexelSize.y < 0)
//					o.uv.y = 1 - o.uv.y;
//				#endif				
//
//				o.interpolatedRay = v.ray;
//
//				return o;
			}


		sampler2D _MainTex;
				
		sampler2D _DetailTex;
			
		float4 _WorldSpaceScannerPos = 10;
		float _ScanDistance = 1;
		float _ScanWidth;
		float _LeadSharp = 10;
		float4 _LeadColor;
		float4 _MidColor;
		float4 _TrailColor;
		float4 _HBarColor;
		float3 wsPos = 10;


		float4 horizBars(float2 p)
			{
				return 1 - saturate(round(abs(frac(p.y * 100) * 2)));
			}

			float4 frag(v2f i) : SV_Target
			{
				
				float4 color = tex2D(_MainTex, i.uv);
				half4 scannerCol = half4(0, 0, 0, 0);
				//float4 color = float4(i.uv.r, i.uv.g, 0.5, 0.5);
				float dist = distance(wsPos, _WorldSpaceScannerPos);


				//float diff = 1 - (_ScanDistance - dist) / (_ScanWidth);
				float diff = 0.5;
				//half4 edge = lerp(_MidColor, _LeadColor, pow(diff, _LeadSharp));
				half4 edge = lerp(_MidColor, _LeadColor, diff);
				scannerCol = lerp(_TrailColor, edge, diff) + horizBars(i.uv/5) * _HBarColor;
				//scannerCol = horizBars(i.uv/5) * _HBarColor;
				scannerCol *= diff;

				return color+scannerCol;
			}
			ENDCG
		}
	}
}