Shader "HLSL/glitchstep"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white"{}
	}

		CGINCLUDE
		#include "UnityCG.cginc"

	
		sampler2D _MainTex;
		float4 _MainTex_ST;

		float Random(float t)
		{
			return frac(
			sin(
					dot(float2(t, t), float2(12.9898, 78.233))
				) * 43758.5453123
			);
		}

		float getTickedTime()
		{
			int fps = 60;
			float delta = 1.0f / float(fps);
			float time = _Time;
			float garbage = fmod(time, delta);
			return time - garbage;
		}

		float4 frag(v2f_img i) : SV_Target
		{
			float glitchstep = lerp(4.0f, 32.0f, Random(getTickedTime()));
			float4 screenColor = tex2D(_MainTex, i.uv);
			i.uv.x = round(i.uv.x * glitchstep) / glitchstep;
			float4 glitchColor = tex2D(_MainTex, i.uv);

			float4 col = lerp(screenColor, glitchColor, float(0.3f));
			return col;
		}

		ENDCG

	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			ENDCG
		}
	}
}