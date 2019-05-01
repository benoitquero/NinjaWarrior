Shader "Custom/WaterSurfaceShader"
{
	Properties
	{
		_MainColor("Color", Color) = (1.0,0.0,0.0,1.0)
		_SpecularColor("Color", Color) = (1.0,1.0,1.0,1.0)
		_Kd("Kd", Float) = 3.5
		_Ks("Ks", Float) = 1.6
		_m("Shininess", Float) = 9.0

		_Amplitude("Amplitude", Float) = 0.5
		_Wavelength("Wavelength", Float) = 10
		_Speed("Speed", Float) = 15

		_Trans("Transparence", Range(0,1)) = 0.5

		_NoiseTex("Noise Texture", 2D) = "white" {}
		_DistortStrength("Distort Strength", Float) = 4
	}

		SubShader
	{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" "LightMode" = "ForwardBase" "Pass" = "ForwardBase" }
		LOD 200

		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		 
		GrabPass
		{
			"_BackgroundTexture"
		}

		//distortion du décor sous l'eau
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"


			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float3 texCoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 grabPos : TEXCOORD0;
			};

			sampler2D _BackgroundTexture;
			sampler2D _NoiseTex;
			float  _DistortStrength;
			float  _Speed;
			float  _Amplitude;
			float _Wavelength;

			v2f vert(appdata v)
			{
				v2f o;

				// convert input to world space
				o.vertex = UnityObjectToClipPos(v.vertex);
				float4 normal4 = float4(v.normal, 0.0);
				float3 normal = normalize(mul(normal4, unity_WorldToObject).xyz);

				// use ComputeGrabScreenPos function from UnityCG.cginc
				// to get the correct texture coordinate
				o.grabPos = ComputeGrabScreenPos(o.vertex);

				// distortion du decor sous l'eau
				float noiseSample = tex2Dlod(_NoiseTex, float4(v.texCoord.xy, 0, 0));
				float k = 2 * UNITY_PI / _Wavelength;
				float f = k * (_Time.y*_Speed*noiseSample);
				o.grabPos.y += sin(f)*_Amplitude * _DistortStrength;
				o.grabPos.x += cos(f)*_Amplitude * _DistortStrength;

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				return tex2Dproj(_BackgroundTexture, i.grabPos);
			}
			ENDCG
		}


		// couleur et mouvement de la surface de l'eau
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag 

			// Use shader model 3.0 target, to get nicer looking lighting
			#pragma target 3.0

			#include "UnityCG.cginc"
			#include "Lighting.cginc"


			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float3 texCoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
				float3 viewDirection : VIEWDIRECTION;
				float4 grabPos : TEXCOORD0;
			};

			float4 _MainColor;
			float4 _SpecularColor;
			float _Kd;
			float _Ks;
			float _m;
			float _Amplitude;
			float _Wavelength;
			float _Speed;
			float  _Trans;
			sampler2D _NoiseTex;
	

		

			v2f vert(appdata v)
			{
				v2f o;

				//vagues
				float noiseSample = tex2Dlod(_NoiseTex, float4(v.texCoord.xy, 0, 0));
				float k = 2 * UNITY_PI / _Wavelength;
				float f = k * (_Time.y*_Speed*noiseSample);
				v.vertex.y += sin(f)*_Amplitude;
				v.vertex.x += cos(f)*_Amplitude;
			
				
				// Normal
				float3 tangent = normalize(float3(1 - k * _Amplitude * sin(f), k * _Amplitude * cos(f), 0));
				float3 normal = float3(-tangent.y, tangent.x, 0);
				o.normal = normal;

				// Vertex transform (Model View Projection)
				o.vertex = UnityObjectToClipPos(v.vertex);
				
				
				// View direction
				o.viewDirection = _WorldSpaceCameraPos - mul(UNITY_MATRIX_M, v.vertex).xyz;
				o.viewDirection = normalize(o.viewDirection);


				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				// Light Direction
				float3 lightDirection = _WorldSpaceLightPos0.xyz;

				// Diffuse
				float diffuseWeight = max(dot(i.normal, lightDirection), 0.0);

				// Specular
				float3 reflection = reflect(-lightDirection, i.normal);
				reflection = normalize(reflection);

				float specularWeight = max(dot(reflection, i.viewDirection), 0.0);
				specularWeight = pow(specularWeight, _m);

				// Color
				fixed4 col = _LightColor0 * (_Kd*diffuseWeight*_MainColor + _Ks * specularWeight*_SpecularColor);
				col.a = _Trans;
			

				return col;
			}
			ENDCG
		}
	}
}


