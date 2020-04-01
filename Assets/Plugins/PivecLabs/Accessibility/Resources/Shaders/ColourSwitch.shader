
Shader "Hidden/ColourSwitch" {
Properties {
	_MainTex ("MainTex", 2D) = "gray" {}
	_Tolerance ("Tolerance", Range(0, 1)) = 0.147644
	_TransitionSoftness ("TransitionSoftness", Range(0, 0.3)) = 0.08354305
	_SourceColorHSV ("SourceColorHSV", Color) = (0.1172414,0,1,1)
	_DestColorHSV ("DestColorHSV", Color) = (1,0.9724137,0,1)
}

SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 100

	Pass {
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog

			#include "UnityCG.cginc"

			float isTargetHue( float center , float tolerance , float soft , float hue ){
				float ret = 0;

				soft = max(1e-5, soft);
				float rcpSoft = 1 / soft;
				float b = (tolerance + soft) * rcpSoft;

				for (int off = -1; off < 2; ++off) {
					ret += saturate(
						-abs(center - hue + off) * rcpSoft + b
					);
				}

				return min(ret, 1);
			}

			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				half2 texcoord : TEXCOORD0;
				UNITY_FOG_COORDS(1)
			};

			uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
			uniform float _Tolerance;
			uniform float _TransitionSoftness;
			uniform float4 _SourceColorHSV;
			uniform float4 _DestColorHSV;

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 _MainTex_var = tex2D(_MainTex, i.texcoord);
				float node_4238 = _SourceColorHSV.rgb.r;
				float4 node_692_k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
				float4 node_692_p = lerp(float4(float4(_MainTex_var.rgb,0.0).zy, node_692_k.wz), float4(float4(_MainTex_var.rgb,0.0).yz, node_692_k.xy), step(float4(_MainTex_var.rgb,0.0).z, float4(_MainTex_var.rgb,0.0).y));
				float4 node_692_q = lerp(float4(node_692_p.xyw, float4(_MainTex_var.rgb,0.0).x), float4(float4(_MainTex_var.rgb,0.0).x, node_692_p.yzx), step(node_692_p.x, float4(_MainTex_var.rgb,0.0).x));
				float node_692_d = node_692_q.x - min(node_692_q.w, node_692_q.y);
				float node_692_e = 1.0e-10;
				float3 node_692 = float3(abs(node_692_q.z + (node_692_q.w - node_692_q.y) / (6.0 * node_692_d + node_692_e)), node_692_d / (node_692_q.x + node_692_e), node_692_q.x);;
				float3 emissive = lerp(_MainTex_var.rgb,(lerp(float3(1,1,1),saturate(3.0*abs(1.0-2.0*frac(((_DestColorHSV.rgb.r-node_4238)+node_692.r)+float3(0.0,-1.0/3.0,1.0/3.0)))-1),node_692.g)*node_692.b),isTargetHue( node_4238 , _Tolerance , _TransitionSoftness , node_692.r ));
				float3 finalColor = emissive;
				return fixed4(finalColor,_MainTex_var.a);
			}
		ENDCG
	}
}

}
