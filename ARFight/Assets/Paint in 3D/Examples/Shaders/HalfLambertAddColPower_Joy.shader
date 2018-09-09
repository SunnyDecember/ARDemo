// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "JoyShader/HalfLambertAddColPower"
{
	Properties
	{		
		_MainColor("MainColor",Color)= (1,1,1,1)
		_MainTex("Texture", 2D) = "white" {}
		_ColPower("ColPower",Range(0,3))=0.5
		
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			Tags{"LightMode"="ForwardBase"}
			
			//Lighting On
			
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"
			#include "AutoLight.cginc"  
			#include "Lighting.cginc"
			
			uniform float4 _MainColor;
			uniform float _SpecPower;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _ColPower;
			
			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL; 
				float4 tangent : TANGENT;  
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				//float3 lightDirection : TEXCOORD1;
				float3 normal1 : TEXCOORD2; 
				float Vetexcol :TEXCOORD3;
				LIGHTING_COORDS(3,4)
			};

			
			
			v2f vert (appdata v)
			{
				v2f o;
				//
				//TANGENT_SPACE_ROTATION;
				//float3 tempDir = mul(rotation,ObjSpaceLightDir(v.vertex));//
				//o.lightDirection = mul(rotation,ObjSpaceLightDir(v.vertex));
				


				o.normal1= v.normal;
				
				//o.diffuse = saturate(dot(v.normal, ObjSpaceLightDir(v.vertex)));
				//o.diffuse = ObjSpaceLightDir(v.vertex);
				float temp = (dot(normalize(v.normal), normalize(ObjSpaceLightDir(v.vertex))) + 1) / 2;//半兰伯特光照公式
				
				o.vertex = UnityObjectToClipPos(v.vertex);

				

				o.Vetexcol = temp;
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				
				/*half3 h = normalize(UnityWorldSpaceLightDir(i.vertex) + UnityWorldSpaceViewDir(i.vertex));
				float nh = saturate(dot(i.normal1, h));
				float spec = pow(nh, _SpecPower);*/
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);	
				
			   //float3 lightColor = UNITY_LIGHTMODEL_AMBIENT.xyz; //本项目暂时没有环境光，关闭计算

				col.rgb = _MainColor*i.Vetexcol*col.rgb*_LightColor0.rgb+ _ColPower*col.rgb*_MainColor;
			   return col;
			}
			ENDCG
		}
	}
}
