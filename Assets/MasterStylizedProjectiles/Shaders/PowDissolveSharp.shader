// Made with Amplify Shader Editor v1.9.1.6
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "SharpDissolveSpike"
{
	Properties
	{
		_Base("Base", 2D) = "white" {}
		[NoScaleOffset]_Noise("Noise", 2D) = "white" {}
		_MaskTiling("MaskTiling", Vector) = (1,1,0,0)
		_MaskOffset("MaskOffset", Vector) = (0,0,0,0)
		[HideInInspector] _texcoord2( "", 2D ) = "white" {}
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
			float4 vertexColor : COLOR;
			float4 uv_texcoord;
			float2 uv2_texcoord2;
		};

		uniform sampler2D _Base;
		uniform float4 _Base_ST;
		uniform sampler2D _Noise;
		uniform float2 _MaskTiling;
		uniform float2 _MaskOffset;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Base = i.uv_texcoord * _Base_ST.xy + _Base_ST.zw;
			float4 tex2DNode1 = tex2D( _Base, uv_Base );
			float4 temp_output_78_0 = ( i.vertexColor * tex2DNode1 * tex2DNode1.a );
			float Emissive42 = i.uv_texcoord.w;
			o.Emission = ( temp_output_78_0 * ( Emissive42 + 1.0 ) ).rgb;
			float4 temp_cast_1 = (i.uv_texcoord.z).xxxx;
			float2 appendResult60 = (float2(( i.uv2_texcoord2.y * _Time.y ) , 1.0));
			float2 uvs_TexCoord53 = i.uv_texcoord;
			uvs_TexCoord53.xy = i.uv_texcoord.xy * _MaskTiling + ( _MaskOffset + appendResult60 );
			float4 temp_output_3_0 = ( tex2DNode1.a * i.vertexColor.a * step( temp_cast_1 , tex2D( _Noise, uvs_TexCoord53.xy ) ) );
			float4 temp_cast_2 = (i.uv_texcoord.z).xxxx;
			float4 temp_cast_3 = (0.1).xxxx;
			float clampResult18 = clamp( ( temp_output_3_0 + ( ( temp_output_3_0 - temp_cast_3 ) * i.uv2_texcoord2.x ) ).a , 0.0 , 1.0 );
			o.Alpha = clampResult18;
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
				float2 customPack2 : TEXCOORD2;
				float3 worldPos : TEXCOORD3;
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
				o.customPack2.xy = customInputData.uv2_texcoord2;
				o.customPack2.xy = v.texcoord1;
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
				surfIN.uv2_texcoord2 = IN.customPack2.xy;
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
Version=19106
Node;AmplifyShaderEditor.CommentaryNode;58;-1721.898,359.641;Inherit;False;1090.228;839.7407;MaskTilngAndOffset;9;60;63;56;59;61;57;54;53;66;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;49;-508.7377,-524.6851;Inherit;False;2061.94;676.9467;Color;11;71;74;76;35;69;9;72;1;36;43;78;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;48;533.7419,625.2585;Inherit;False;924.4846;574.9659;Contrast;4;45;44;4;46;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;31;-572.9139,227.7113;Inherit;False;1094.654;1013.391;PowNoiseMask;5;30;42;2;3;65;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;287.6674,345.1131;Inherit;True;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;43;-300.1396,-88.83288;Inherit;False;42;Emissive;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;44;571.1497,788.3623;Inherit;False;Constant;_Float0;Float 0;2;0;Create;True;0;0;0;False;0;False;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;4;787.9116,709.1815;Inherit;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;45;1087.944,768.3716;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;2;-524.4171,324.7219;Inherit;True;Property;_Noise;Noise;1;1;[NoScaleOffset];Create;True;0;0;0;False;0;False;-1;4e3951c538fc8a647a4a10a99b480987;3584f2bf4afb5284d91edb6a29126e62;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;53;-1063.242,409.641;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;54;-1456.896,485.8632;Inherit;False;Property;_MaskTiling;MaskTiling;2;0;Create;True;0;0;0;False;0;False;1,1;1,2;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;46;752.456,972.8727;Inherit;False;1;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;63;-948.4167,565.6185;Inherit;False;Constant;_Vector0;Vector 0;3;0;Create;True;0;0;0;False;0;False;0.5,0.5;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;30;-492.4632,559.4526;Inherit;False;0;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;57;-1680.791,965.4884;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;61;-1400.16,971.8941;Inherit;False;Constant;_Float2;Float 2;3;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;59;-1670.288,804.364;Inherit;False;1;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;56;-1446.796,827.1967;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;60;-1318.068,765.3179;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;66;-1441.515,643.5126;Inherit;False;Property;_MaskOffset;MaskOffset;3;0;Create;True;0;0;0;False;0;False;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleAddOpNode;67;-1147.646,671.634;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.VertexColorNode;9;-306.6669,-482.4848;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;71;691.192,-448.3276;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;69;116.1208,65.60186;Inherit;False;Property;_CenterSaturation;CenterSaturation;4;0;Create;True;0;0;0;False;0;False;0;100;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;2124.837,-4.975019;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;SharpDissolveSpike;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;;0;False;;False;0;False;;0;False;;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;True;2;5;False;;10;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
Node;AmplifyShaderEditor.ClampOpNode;18;1810.263,286.6022;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;80;1606.451,370.8786;Inherit;False;COLOR;1;0;COLOR;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleAddOpNode;47;1286.55,306.9924;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;1;-463.2111,-296.6113;Inherit;True;Property;_Base;Base;0;0;Create;True;0;0;0;False;0;False;-1;None;8aed1e8303e383d47b3273e583986c32;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;65;-5.537796,615.5311;Inherit;True;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;42;-286.4211,764.4086;Inherit;False;Emissive;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;78;-9.536772,-458.2468;Inherit;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;72;1218.052,-185.1761;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;35;87.78829,-117.4384;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;36;-281.4154,3.857325;Inherit;False;Constant;_Float3;Float 3;3;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;76;481.1188,-239.8147;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;74;344.5398,-71.79294;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
WireConnection;3;0;1;4
WireConnection;3;1;9;4
WireConnection;3;2;65;0
WireConnection;4;0;3;0
WireConnection;4;1;44;0
WireConnection;45;0;4;0
WireConnection;45;1;46;1
WireConnection;2;1;53;0
WireConnection;53;0;54;0
WireConnection;53;1;67;0
WireConnection;56;0;59;2
WireConnection;56;1;57;0
WireConnection;60;0;56;0
WireConnection;60;1;61;0
WireConnection;67;0;66;0
WireConnection;67;1;60;0
WireConnection;71;0;78;0
WireConnection;71;1;1;0
WireConnection;71;2;76;0
WireConnection;0;2;72;0
WireConnection;0;9;18;0
WireConnection;18;0;80;3
WireConnection;80;0;47;0
WireConnection;47;0;3;0
WireConnection;47;1;45;0
WireConnection;65;0;30;3
WireConnection;65;1;2;0
WireConnection;42;0;30;4
WireConnection;78;0;9;0
WireConnection;78;1;1;0
WireConnection;78;2;1;4
WireConnection;72;0;78;0
WireConnection;72;1;35;0
WireConnection;35;0;43;0
WireConnection;35;1;36;0
WireConnection;76;0;74;0
WireConnection;74;0;1;4
WireConnection;74;1;69;0
ASEEND*/
//CHKSM=FB6D442ED3EAB3B42965835E025C26470B9ED953