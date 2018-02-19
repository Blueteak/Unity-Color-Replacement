Shader "Custom/ColorRamp" {
	Properties{
		_MainTex("Texture", 2D) = "white" {}
	_Ramp("Texture rampe", 2D) = "white" {}
	}
		SubShader{
		Pass{
		
		Tags
		{
			"Queue" = "Geometry"
			"RenderType" = "Opaque"
		}
		Offset 0, 1000
		CGPROGRAM

#pragma vertex vert_img
#pragma fragment frag
#pragma
#include "UnityCG.cginc"

		sampler2D _Ramp;
		sampler2D _MainTex;


		fixed4 frag(v2f_img i) : COLOR{
			fixed tempData = tex2D(_MainTex, i.uv.xy);
			fixed2 rampUV = fixed2(tempData, 0);
			fixed4 heatColor = tex2D(_Ramp, rampUV);
			return heatColor;
		}
		ENDCG
		}
	}
}