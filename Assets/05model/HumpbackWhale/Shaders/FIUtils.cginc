#ifndef FI_UTILS
#define FI_UTILS

sampler2D _FlowMap, _CausticsMap;

float _FlowMapIntensity, _FlowMapScale_1, _FlowMapScale_2,
            _FlowMapScale_3, _CausticsScale_1, _CausticsScale_2,
            _CausticsScale_3, _CausticsSpeed_1, _CausticsSpeed_2,
            _CausticsSpeed_3, _CausticsWave1_Multiply, _GlobalScale, 
			_GlobalSpeed;

float ThreePointLevels(float col, float2 uv, float black, float middle, float white)
{
	float col2 = 1 - col;
	float middle2 = 1 - middle;

	float alpha1 = saturate(col2 / middle2);
	float alpha2 = saturate(col / middle);

	float alpha3 = floor(alpha2);

	float col11 = lerp(black, middle, alpha2);
	float col12 = lerp(white, middle, alpha1);

	return lerp(col11, col12, alpha3);
}

float GetCaustics(float2 uv, float3 worldNormal, float localScale, float localIntensity)
{
    float2 uv1 = _CausticsScale_1 * localScale * _GlobalScale * lerp(uv, tex2D(_FlowMap, uv * _FlowMapScale_1 * _GlobalScale * localScale).rg, _FlowMapIntensity);
    float2 uv2 = _CausticsScale_2 * localScale * _GlobalScale * lerp(uv, tex2D(_FlowMap, uv * _FlowMapScale_2 * _GlobalScale * localScale).rg, _FlowMapIntensity);
    float2 uv3 = _CausticsScale_3 * localScale * _GlobalScale * lerp(uv, tex2D(_FlowMap, uv * _FlowMapScale_3 * _GlobalScale * localScale).rg, _FlowMapIntensity);

	float waves_1a = tex2D(_CausticsMap, uv1 + _Time.y * _CausticsSpeed_1 * _GlobalSpeed + .1);
	float waves_2a = tex2D(_CausticsMap, uv2 + _Time.y * _CausticsSpeed_2 * _GlobalSpeed + .2);
    float waves_3a = tex2D(_CausticsMap, uv3 + _Time.y * _CausticsSpeed_3 * _GlobalSpeed + .3);
	
    float waves_a = localIntensity * _CausticsWave1_Multiply * (waves_1a + waves_2a) * waves_3a;
		
	// Normal Mask
	float mask = (worldNormal.y + 0.7);
	mask = saturate(mask * mask);
	
	return waves_a * mask;
}

#endif