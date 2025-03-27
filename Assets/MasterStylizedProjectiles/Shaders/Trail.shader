// Made with Amplify Shader Editor v1.9.3.2
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Trail"
{
	Properties
	{
		[NoScaleOffset]_TextureSample0("Texture Sample 0", 2D) = "white" {}
		[HDR]_TrailColor("TrailColor", Color) = (1,1,1,1)
		_Tiling("Tiling", Vector) = (1,1,0,0)
		_MainTexSpeed("MainTexSpeed", Float) = 0
		_SimpleNoise("SimpleNoise", 2D) = "white" {}
		_DissolveSpeed("DissolveSpeed", Vector) = (0,0,0,0)
		_BottomMask("BottomMask", Range( 0 , 1)) = 1
		[Toggle]_MeshNoise("MeshNoise", Float) = 1
		_MeshNoiseStrength("MeshNoiseStrength", Range( 1 , 10)) = 0
		_SurfaceNoiseTiling("SurfaceNoiseTiling", Vector) = (0,0,0,0)
		_NoiseScale("NoiseScale", Vector) = (0,0,0,0)
		_ScrollSpeed("ScrollSpeed", Vector) = (0,0,0,0)
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
		struct Input
		{
			float4 vertexColor : COLOR;
			float2 uv_texcoord;
		};

		uniform float _MeshNoise;
		uniform float2 _SurfaceNoiseTiling;
		uniform float2 _ScrollSpeed;
		uniform float3 _NoiseScale;
		uniform float _MeshNoiseStrength;
		uniform float4 _TrailColor;
		uniform sampler2D _SimpleNoise;
		uniform float2 _DissolveSpeed;
		uniform float _BottomMask;
		uniform sampler2D _TextureSample0;
		uniform float2 _Tiling;
		uniform float _MainTexSpeed;


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float2 uv_TexCoord64 = v.texcoord.xy * _SurfaceNoiseTiling + ( _ScrollSpeed * _Time.y );
			float simplePerlin2D110 = snoise( uv_TexCoord64*_NoiseScale.x );
			simplePerlin2D110 = simplePerlin2D110*0.5 + 0.5;
			float simplePerlin2D111 = snoise( uv_TexCoord64*_NoiseScale.y );
			simplePerlin2D111 = simplePerlin2D111*0.5 + 0.5;
			float simplePerlin2D112 = snoise( uv_TexCoord64*_NoiseScale.z );
			simplePerlin2D112 = simplePerlin2D112*0.5 + 0.5;
			float4 appendResult102 = (float4(simplePerlin2D110 , simplePerlin2D111 , simplePerlin2D112 , 0.0));
			float4 temp_cast_0 = (0.5).xxxx;
			v.vertex.xyz += (( _MeshNoise )?( ( v.texcoord.xy.x * ( appendResult102 - temp_cast_0 ) * _MeshNoiseStrength ) ):( float4( 0,0,0,0 ) )).xyz;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Emission = ( i.vertexColor * _TrailColor ).rgb;
			float4 color121 = IsGammaSpace() ? float4(1,1,1,1) : float4(1,1,1,1);
			float2 uv_TexCoord22 = i.uv_texcoord + ( _DissolveSpeed * _Time.y );
			float4 temp_cast_1 = (i.uv_texcoord.x).xxxx;
			float4 lerpResult122 = lerp( color121 , ( ( tex2D( _SimpleNoise, uv_TexCoord22 ) + ( 1.0 - i.uv_texcoord.x ) ) - temp_cast_1 ) , _BottomMask);
			float mulTime7 = _Time.y * _MainTexSpeed;
			float4 appendResult11 = (float4(mulTime7 , 0.0 , 0.0 , 0.0));
			float2 uv_TexCoord10 = i.uv_texcoord * _Tiling + appendResult11.xy;
			float4 clampResult50 = clamp( ( lerpResult122 * tex2D( _TextureSample0, uv_TexCoord10 ) * i.vertexColor.a ) , float4( 0,0,0,0 ) , float4( 1,0,0,0 ) );
			o.Alpha = clampResult50.r;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard alpha:fade keepalpha fullforwardshadows vertex:vertexDataFunc 

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
				float2 customPack1 : TEXCOORD1;
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
				vertexDataFunc( v, customInputData );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
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
				surfIN.uv_texcoord = IN.customPack1.xy;
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
Node;AmplifyShaderEditor.CommentaryNode;41;-1924.402,-137.8606;Inherit;False;1600.041;636.8571;Mask;9;12;14;13;17;16;18;21;22;20;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleTimeNode;18;-1829.149,74.43996;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;20;-1874.402,-59.37947;Inherit;False;Property;_DissolveSpeed;DissolveSpeed;5;0;Create;True;0;0;0;False;0;False;0,0;0,0.3;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.CommentaryNode;40;-1222.275,611.2413;Inherit;False;1157.156;425.3126;Trail;6;7;9;10;5;11;117;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-1613.964,50.73314;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleTimeNode;88;-713.933,1814.146;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;66;-782.6865,1645.637;Inherit;False;Property;_ScrollSpeed;ScrollSpeed;12;0;Create;True;0;0;0;False;0;False;0,0;-4.42,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;9;-1172.275,803.0377;Inherit;False;Property;_MainTexSpeed;MainTexSpeed;3;0;Create;True;0;0;0;False;0;False;0;-1.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;22;-1446.193,-87.8606;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;14;-1195.458,196.9965;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;67;-450.2749,1664.791;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;65;-779.5142,1492.399;Inherit;False;Property;_SurfaceNoiseTiling;SurfaceNoiseTiling;10;0;Create;True;0;0;0;False;0;False;0,0;2.75,0.89;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;7;-949.8669,838.5471;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;12;-1132.46,-54.99854;Inherit;True;Property;_SimpleNoise;SimpleNoise;4;0;Create;True;0;0;0;False;0;False;-1;None;4e3951c538fc8a647a4a10a99b480987;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;16;-880.637,150.8899;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;64;-290.5891,1548.724;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector3Node;116;-276.0471,1784.871;Inherit;False;Property;_NoiseScale;NoiseScale;11;0;Create;True;0;0;0;False;0;False;0,0,0;0.71,0.66,1.12;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DynamicAppendNode;11;-838.0846,661.2413;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.Vector2Node;117;-1112.234,909.2992;Inherit;False;Property;_Tiling;Tiling;2;0;Create;True;0;0;0;False;0;False;1,1;1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleAddOpNode;17;-698.8079,51.75106;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;112;64.74635,1831.091;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;111;63.77478,1582.973;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;110;74.69085,1341.538;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;39;-285.1063,-1324.955;Inherit;False;771.2992;1168.484;Color;6;1;33;4;28;25;26;;1,1,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;10;-670.0497,734.5539;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;13;-618.5934,123.7121;Inherit;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;121;-279.3824,33.62794;Inherit;False;Constant;_Color0;Color 0;11;0;Create;True;0;0;0;False;0;False;1,1,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;102;446.114,1274.321;Inherit;True;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;59;827.3732,1231.187;Inherit;False;Constant;_Float1;Float 1;6;0;Create;True;0;0;0;False;0;False;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;119;-259.2874,298.3325;Inherit;False;Property;_BottomMask;BottomMask;7;0;Create;True;0;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;33;335.6519,-751.0807;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;5;-385.1184,801.8178;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;1;[NoScaleOffset];Create;True;0;0;0;False;0;False;-1;None;3b553fa3300210e4da5803f20485a843;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;122;9.849993,43.93584;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;60;914.2504,808.6817;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;58;1030.908,1013.159;Inherit;True;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;62;1061.911,1383.421;Inherit;False;Property;_MeshNoiseStrength;MeshNoiseStrength;9;0;Create;True;0;0;0;False;0;False;0;4.67;1;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;292.4402,347.6017;Inherit;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;123;542.4391,-530.5817;Inherit;False;Property;_TrailColor;TrailColor;1;1;[HDR];Create;True;0;0;0;False;0;False;1,1,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;61;1363.755,973.9749;Inherit;True;3;3;0;FLOAT;0;False;1;FLOAT4;0,0,0,0;False;2;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleAddOpNode;51;34.63141,-357.297;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;1;-263.8744,-326.5705;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;26;-258.7932,-846.1656;Inherit;False;1;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;28;165.4909,-948.2759;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;25;-249.181,-1048.606;Inherit;False;0;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;54;672.7817,-278.3745;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;55;502.7817,-201.3745;Inherit;False;Constant;_Float0;Float 0;5;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;4;248.7559,-531.3147;Inherit;False;3;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;2;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ClampOpNode;50;577.5943,393.6278;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;53;511.3821,-318.6634;Inherit;False;Property;_Emissive;Emissive;6;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;52;792.3119,-555.846;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ToggleSwitchNode;124;1552.802,711.104;Inherit;False;Property;_MeshNoise;MeshNoise;8;0;Create;True;0;0;0;False;0;False;1;True;2;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1869.688,51.96114;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Trail;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;;0;False;;False;0;False;;0;False;;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;True;2;5;False;;10;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;17;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;16;FLOAT4;0,0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;21;0;20;0
WireConnection;21;1;18;0
WireConnection;22;1;21;0
WireConnection;67;0;66;0
WireConnection;67;1;88;0
WireConnection;7;0;9;0
WireConnection;12;1;22;0
WireConnection;16;0;14;1
WireConnection;64;0;65;0
WireConnection;64;1;67;0
WireConnection;11;0;7;0
WireConnection;17;0;12;0
WireConnection;17;1;16;0
WireConnection;112;0;64;0
WireConnection;112;1;116;3
WireConnection;111;0;64;0
WireConnection;111;1;116;2
WireConnection;110;0;64;0
WireConnection;110;1;116;1
WireConnection;10;0;117;0
WireConnection;10;1;11;0
WireConnection;13;0;17;0
WireConnection;13;1;14;1
WireConnection;102;0;110;0
WireConnection;102;1;111;0
WireConnection;102;2;112;0
WireConnection;5;1;10;0
WireConnection;122;0;121;0
WireConnection;122;1;13;0
WireConnection;122;2;119;0
WireConnection;58;0;102;0
WireConnection;58;1;59;0
WireConnection;6;0;122;0
WireConnection;6;1;5;0
WireConnection;6;2;33;4
WireConnection;61;0;60;1
WireConnection;61;1;58;0
WireConnection;61;2;62;0
WireConnection;51;0;1;1
WireConnection;51;1;26;4
WireConnection;28;0;25;3
WireConnection;28;1;25;4
WireConnection;28;2;26;1
WireConnection;54;0;53;0
WireConnection;54;1;55;0
WireConnection;4;0;33;0
WireConnection;4;1;28;0
WireConnection;4;2;25;1
WireConnection;50;0;6;0
WireConnection;52;0;33;0
WireConnection;52;1;123;0
WireConnection;124;1;61;0
WireConnection;0;2;52;0
WireConnection;0;9;50;0
WireConnection;0;11;124;0
ASEEND*/
//CHKSM=F629787C25AC0062B3A08C9410B98E929C92A3C0