// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Neon"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color("Light Color", Color) = (1, 1, 1, 1)
		_Mag("Magnitude", Range(0, 1)) = 1
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			/*struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};*/

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float3 normal : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Color;
			float _Mag;
			
			v2f vert (appdata_base v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.normal = mul(UNITY_MATRIX_MV, v.normal);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 Brighten(fixed4 color, fixed intensity) {
				return lerp(color, fixed4(1, 1, 1, 1), pow(intensity, 2));
			}

			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				col *= _Color;

				fixed angle2cam = (i.normal.b + 1) / 2;
				col = Brighten(col, angle2cam);

				col *= _Mag;
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
