// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/warudo"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Origin ("Origin Point", Vector) = (0, 0, 0)
		_Radius ("Radius", Float) = 0
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
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				//float4 origin : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float3 _Origin;
			float _Radius;
			static float PI = 3.14159;
			static float TwoThirdsRad = 2.09440;
			static float FourThirdsRad = 4.18879;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				//o.origin = mul(_World2Object, float4(_Origin, 1.0));
				//o.origin = mul(UNITY_MATRIX_VP, o.origin);
				return o;
			}

			float2 ScaleToAspect(float2 uv) 
			{
				float aspectRatio = _ScreenParams.x / _ScreenParams.y;
				float b = 0.5 - (0.5 * aspectRatio);
				return float2((aspectRatio * uv.x) + b, uv.y);
			}

			float4 HueShift(float x, float4 col) 
			{
				float4 color;
				float brightness = max(col.r, max(col.b, col.g));
				float saturation = min(col.r, min(col.b, col.g));

				float m = (brightness - saturation) / 2;
				float b = brightness - m;
				m = m * 3;

				color.r = (cos(x + col.r) * m) + b;
				color.g = (cos(x + TwoThirdsRad + col.g) * m) + b;
				color.b = (cos(x + FourThirdsRad + col.b) * m) + b;
				color.a = 1;
				return color;
			}

			float2 DistortUV (float2 uv, float2 centuv, float dist, float mag) 
			{
				mag = (log10(mag) + 1) * 0.5;
				return lerp(uv, centuv, (1 - dist) * mag);
			}

			float4 frag (v2f i) : SV_Target
			{
				//_Origin = mul(UNITY_MATRIX_P, float4(_Origin, 1.0));
				float2 scaledUV = ScaleToAspect(i.uv);
				float2 scaledOrigin = ScaleToAspect(_Origin.xy);
				float distanceFromOrigin = distance(scaledUV, scaledOrigin);
				float invertColors = step(distanceFromOrigin, _Radius);

				float4 col;
				if (invertColors && _Origin.z > 0)
				{
					//Distort UV
					float2 distuv = DistortUV(i.uv, scaledOrigin.xy, distanceFromOrigin, _Radius);
					col = tex2D(_MainTex, distuv);
					col = 1 - col;
					col = HueShift(_Time.w, col);
				}
				else 
				{
					col = tex2D(_MainTex, i.uv);
				}
				return col;
			}
			ENDCG
		}
	}
}
