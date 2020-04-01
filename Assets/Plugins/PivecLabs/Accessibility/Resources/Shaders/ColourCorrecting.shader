
Shader "Hidden/ColourCorrecting" 
{
	Properties 
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Brightness ("Brightness", Float) = 1
		_Saturation("Saturation", Float) = 1
		_Contrast("Contrast", Float) = 1
		_Hue("Hue", Float) =0
	}

	SubShader 
	{
		Pass 
		{  
			ZTest Always Cull Off ZWrite Off
			
			CGPROGRAM  
			#pragma vertex vert  
			#pragma fragment frag  
			#pragma target 3.0  

			#include "UnityCG.cginc"  
			  
						 
            half shift(half value, half min, half max)
            {
                 return (value < min) ? value + max : (value > max) ? value - max : value;
              }



            
            half3 rgb2hsv(half3 c)
            {
                half4 K = half4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                half4 p = c.g < c.b ? half4(c.bg, K.wz) : half4(c.gb, K.xy);
                half4 q = c.r < p.x ? half4(p.xyw, c.r) : half4(c.r, p.yzx);

                 float d = q.x - min(q.w, q.y);
                 float e = 1.0e-10;
                return half3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
            }   

            half3 hsv2rgb(half3 c)
            {
              half4 K = half4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
              half3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
              return c.z * lerp(K.xxx, saturate(p - K.xxx), c.y);
            }      
            
            sampler2D _MainTex;  
            half _Brightness;
            half _Saturation;
            half _Contrast;
            half _Hue;
   
			struct v2f 
			{
				float4 pos : SV_POSITION;
				half2 uv: TEXCOORD0;
			};
			  
			v2f vert(appdata_img v) 
			{
				v2f o;				
				o.pos = UnityObjectToClipPos(v.vertex);				
				o.uv = v.texcoord;						 
				return o;
			}
		
			half4 frag(v2f i) : SV_Target 
			{
				half4 srccolor = tex2D(_MainTex, i.uv);  				  				
				half3 finalColor = srccolor.rgb ;
				half3 hsv=rgb2hsv(finalColor);
				hsv.x=shift(hsv.x+_Hue,0,1);
				finalColor=hsv2rgb(hsv);
				fixed3 luminanceColor=Luminance(srccolor);
				finalColor = clamp(lerp(luminanceColor, finalColor, _Saturation),0,10);
				finalColor=finalColor *_Brightness;										
				half3 grey = half3(0.5, 0.5, 0.5);
				finalColor=pow((finalColor+grey),_Contrast)-grey;								
				return fixed4(finalColor, srccolor.a);  
			} 
             	
           
			ENDCG
		}  
	}	
	Fallback Off
}