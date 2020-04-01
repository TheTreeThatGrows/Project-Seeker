

Shader "Hidden/ColourOutline" 
{
	Properties 
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_OutlineOnly ("Outline Only", Float) = 1.0
		_OutlineWidth ("OutlineWidth", Float) = 1.0
		_OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
		_BackgroundColor ("Background Color", Color) = (1, 1, 1, 1)
	}
	SubShader 
	{
		Pass 
		{  
			ZTest Always Cull Off ZWrite Off
			
			CGPROGRAM
			
			#include "UnityCG.cginc"
			
			#pragma target 3.0
			#pragma vertex vert  
			#pragma fragment fragSobel
			
			sampler2D _MainTex;  
			uniform half4 _MainTex_TexelSize;
			fixed _OutlineOnly;
			fixed _OutlineWidth;
			fixed4 _OutlineColor;
			fixed4 _BackgroundColor;
			
			struct v2f 
			{
				float4 pos : SV_POSITION;
				half2 uv : TEXCOORD0;
			};
			  
			v2f vert(appdata_img v) 
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				
				half2 uv = v.texcoord;
		
				o.uv=uv;
						 
				return o;
			}
			
			fixed luminance(fixed4 color) 
			{
				return  0.2125 * color.r + 0.7154 * color.g + 0.0721 * color.b; 
			}
			
			half Sobel(v2f i) 
			{
				const half Gx[9] = {-1, -2, -1,
										0,  0,  0,
										1,  2,  1};
				const half Gy[9] = {-1,  0,  1,
										-2,  0,  2,
										-1,  0,  1};
				half2 duv[9]= {i.uv + _MainTex_TexelSize.xy * half2(-1, -1),i.uv + _MainTex_TexelSize.xy * half2(0, -1),i.uv + _MainTex_TexelSize.xy * half2(1, -1),
								i.uv + _MainTex_TexelSize.xy * half2(-1, 0),i.uv + _MainTex_TexelSize.xy * half2(0, 0),i.uv + _MainTex_TexelSize.xy * half2(1, 0),
								i.uv + _MainTex_TexelSize.xy * half2(-1, 1),i.uv + _MainTex_TexelSize.xy * half2(0, 1),i.uv + _MainTex_TexelSize.xy * half2(1, 1)};	
				
				half texColor;
				half edgeX = 0;
				half edgeY = 0;
				for (int it = 0; it < 9; it++) 
				{
					texColor = luminance(tex2D(_MainTex, duv[it]));
					edgeX += texColor * Gx[it];
					edgeY += texColor * Gy[it];
				}
				
				half edge = 1 - abs(edgeX) - abs(edgeY);
				
				return edge;
			}
			
			fixed4 fragSobel(v2f i) : SV_Target 
			{
				half edge = Sobel(i);
				fixed4 withOutlineColor = lerp(_OutlineColor, tex2D(_MainTex, i.uv), edge);
				withOutlineColor=lerp(tex2D(_MainTex, i.uv),withOutlineColor,_OutlineWidth);
				fixed4 onlyOutlineColor = lerp(_OutlineColor, _BackgroundColor, edge);
				onlyOutlineColor=lerp(_BackgroundColor,onlyOutlineColor,_OutlineWidth);
				return lerp(withOutlineColor, onlyOutlineColor, _OutlineOnly);
 			}
			
			ENDCG
		} 
	}
	FallBack Off
}
