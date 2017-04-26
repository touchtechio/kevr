// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "XRay Shaders/ColoredOutlineWTexture"
{
	Properties
	{
		_EdgeColor("Edge Color", Color) = (1,1,1,1)
		_Color("Main Color", Color) = (0,0,0,0)
		_MainTex("Base (RGB)", 2D) = "white" {}
	} 

	SubShader
	{
//		Stencil
//		{
//			Ref 0
//			Comp NotEqual
//		}

		Tags
		{
			"Queue" = "Transparent"
			"RenderType" = "Opaque"
			"XRay" = "ColoredOutline"
		}

		ZWrite Off
		ZTest Always
		Blend One One

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
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
				float3 viewDir : TEXCOORD1;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				o.normal = UnityObjectToWorldNormal(v.normal);
				o.viewDir = normalize(_WorldSpaceCameraPos.xyz - mul(unity_ObjectToWorld, v.vertex).xyz);
				return o;
			}

			float4 _EdgeColor;
			sampler2D _MainTex;

			fixed4 frag (v2f i) : SV_Target
			{
				float NdotV = 1 - dot(i.normal, i.viewDir) * 1.5;
				float4 color = tex2D(_MainTex, i.uv);
				
				return color + _EdgeColor * NdotV;
			}

			ENDCG
		}
	}
}
