Shader "Unlit/UnlitAlpha"
{
	Properties
	{
		 _Color("Main Color", Color) = (0,0,0,0)
		 _Stencil("Stencil Texture (RGB)", 2D) = "white" {}
	}
		Subshader
		{
		    Tags 
			{
				 "Queue" = "Transparent"
				 "IgnoreProjector" = "False"
				 "RenderType" = "Transparent"
		    }

		   CGPROGRAM
		   #pragma surface surf Lambert alpha

			struct Input 
			{
				float2 uv_MainTex;
			};

			half4 _Color;
			sampler2D _Stencil;

			void surf(Input IN, inout SurfaceOutput o) 
			{
				half4 c = _Color;
				o.Emission = c.rgb;
				o.Alpha = c.a - tex2D(_Stencil, IN.uv_MainTex).a;
			}
		   ENDCG
	}
}
