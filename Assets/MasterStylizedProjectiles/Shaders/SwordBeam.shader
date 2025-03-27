// Made with Amplify Shader Editor v1.9.3.2
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "SwordBeam"
{
	Properties
	{
		_TextureSample2("Texture Sample 2", 2D) = "white" {}
		_TextureSample3("Texture Sample 3", 2D) = "white" {}
		_Noise1Scale("Noise1Scale", Vector) = (0,0,0,0)
		_Scroll1m("Scroll1m", Vector) = (0,0,0,0)
		_Noise2Scale("Noise2Scale", Vector) = (0,0,0,0)
		_Scroll2("Scroll2", Vector) = (0,0,0,0)
		_MaskLength("MaskLength", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#undef TRANSFORM_TEX
		#define TRANSFORM_TEX(tex,name) float4(tex.xy * name##_ST.xy + name##_ST.zw, tex.z, tex.w)
		struct Input
		{
			float4 uv_texcoord;
			float4 vertexColor : COLOR;
		};

		uniform float _MaskLength;
		uniform sampler2D _TextureSample2;
		uniform float2 _Noise1Scale;
		uniform float2 _Scroll1m;
		uniform sampler2D _TextureSample3;
		uniform float2 _Noise2Scale;
		uniform float2 _Scroll2;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float clampResult44 = clamp( ( ( 1.0 - i.uv_texcoord.xy.x ) - _MaskLength ) , 0.0 , 1.0 );
			float2 uvs_TexCoord20 = i.uv_texcoord;
			uvs_TexCoord20.xy = i.uv_texcoord.xy * _Noise1Scale + ( _Scroll1m * _Time.y );
			float4 tex2DNode18 = tex2D( _TextureSample2, uvs_TexCoord20.xy );
			float2 uvs_TexCoord21 = i.uv_texcoord;
			uvs_TexCoord21.xy = i.uv_texcoord.xy * _Noise2Scale + ( _Time.y * _Scroll2 );
			float4 tex2DNode19 = tex2D( _TextureSample3, uvs_TexCoord21.xy );
			float4 temp_output_37_0 = ( clampResult44 * ( tex2DNode18 + tex2DNode19 ) );
			o.Emission = ( ( clampResult44 + temp_output_37_0 ) * i.vertexColor * ( i.uv_texcoord.z + 1.0 ) ).rgb;
			o.Alpha = temp_output_37_0.r;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard alpha:fade keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float4 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				half4 color : COLOR0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.customPack1.xyzw = customInputData.uv_texcoord;
				o.customPack1.xyzw = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				o.color = v.color;
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xyzw;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.vertexColor = IN.color;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=19302
Node;AmplifyShaderEditor.CommentaryNode;35;-1693.362,-1542.68;Inherit;False;1435.68;806.5784;Comment;14;18;20;21;19;23;24;25;22;27;28;29;26;36;54;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;53;-1124.816,-2039.015;Inherit;False;930.8447;395.108;Head;5;43;44;32;34;42;;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector2Node;23;-1553.205,-912.3234;Inherit;False;Property;_Scroll2;Scroll2;5;0;Create;True;0;0;0;False;0;False;0,0;0,-0.35;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;22;-1533.918,-1340.71;Inherit;False;Property;_Scroll1m;Scroll1m;3;0;Create;True;0;0;0;False;0;False;0,0;0,0.19;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;28;-1643.362,-1129.749;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;24;-1313.206,-1492.68;Inherit;False;Property;_Noise1Scale;Noise1Scale;2;0;Create;True;0;0;0;False;0;False;0,0;1,0.61;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;25;-1349.826,-1058.74;Inherit;False;Property;_Noise2Scale;Noise2Scale;4;0;Create;True;0;0;0;False;0;False;0,0;1,1.42;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-1298.008,-1272.3;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;-1271.555,-871.1016;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;32;-1074.816,-1989.015;Inherit;True;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;20;-1064.194,-1395.639;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;21;-1047.715,-998.3181;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;34;-809.744,-1834.472;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;42;-887.5039,-1759.907;Inherit;False;Property;_MaskLength;MaskLength;8;0;Create;True;0;0;0;False;0;False;0;0.32;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;19;-701.9302,-1119.563;Inherit;True;Property;_TextureSample3;Texture Sample 3;1;0;Create;True;0;0;0;False;0;False;-1;None;571680247c3e66c469fa986bac4d7a98;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;18;-691.0176,-1387.101;Inherit;True;Property;_TextureSample2;Texture Sample 2;0;0;Create;True;0;0;0;False;0;False;-1;None;571680247c3e66c469fa986bac4d7a98;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;43;-574.2996,-1800.828;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;44;-430.6752,-1879.102;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;54;-371.1372,-1261.624;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;52;231.2267,-294.7523;Inherit;False;Constant;_Float1;Float 1;13;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;50;184.034,-525.2714;Inherit;False;0;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;37;-144.5754,-1412.188;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.VertexColorNode;47;358.8044,-770.9969;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;51;538.289,-545.8557;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;45;173.2222,-1642.567;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;36;-618.8582,-887.6759;Inherit;False;Property;_NoiseStrength;NoiseStrength;6;0;Create;True;0;0;0;False;0;False;0;4.66;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;55;532.4962,-1065.141;Inherit;True;COLOR;1;0;COLOR;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.ColorNode;39;-105.7598,-937.8659;Inherit;False;Property;_Color0;Color 0;7;0;Create;True;0;0;0;False;0;False;0,0,0,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;-351.103,-1068.896;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;40;860.9203,-1064.732;Inherit;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;46;176.7129,-1260.14;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1368.833,-835.7646;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;SwordBeam;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;;0;False;;False;0;False;;0;False;;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;True;2;5;False;;10;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;17;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;16;FLOAT4;0,0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;27;0;22;0
WireConnection;27;1;28;0
WireConnection;29;0;28;0
WireConnection;29;1;23;0
WireConnection;20;0;24;0
WireConnection;20;1;27;0
WireConnection;21;0;25;0
WireConnection;21;1;29;0
WireConnection;34;0;32;1
WireConnection;19;1;21;0
WireConnection;18;1;20;0
WireConnection;43;0;34;0
WireConnection;43;1;42;0
WireConnection;44;0;43;0
WireConnection;54;0;18;0
WireConnection;54;1;19;0
WireConnection;37;0;44;0
WireConnection;37;1;54;0
WireConnection;51;0;50;3
WireConnection;51;1;52;0
WireConnection;45;0;44;0
WireConnection;45;1;37;0
WireConnection;55;0;37;0
WireConnection;26;0;18;0
WireConnection;26;1;19;0
WireConnection;26;2;36;0
WireConnection;40;0;45;0
WireConnection;40;1;47;0
WireConnection;40;2;51;0
WireConnection;46;0;37;0
WireConnection;0;2;40;0
WireConnection;0;9;55;0
ASEEND*/
//CHKSM=BCFFC38E49C59AFBDFF4AECFD4A37ACB66E9FCD3