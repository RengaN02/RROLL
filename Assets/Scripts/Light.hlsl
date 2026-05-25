void GetFullDitherLight_float(float3 WorldPos, float3 WorldNormal, out float OutIntensity)
{
   OutIntensity = 0;
    #ifndef SHADERGRAPH_PREVIEW
       // 1. Main light
       Light mainLight = GetMainLight();
       float mainDot = saturate(dot(WorldNormal, mainLight.direction));
       OutIntensity += mainDot * mainLight.shadowAttenuation;

       // 2. Point-Spot Light
       int pixelLightCount = GetAdditionalLightsCount();
       for (int i = 0; i < pixelLightCount; ++i)
       {
           Light light = GetAdditionalLight(i, WorldPos);
           float addDot = saturate(dot(WorldNormal, light.direction));

           float intensity = addDot * light.distanceAttenuation * light.shadowAttenuation * 100;

           intensity = pow(intensity, 0.01); 

           OutIntensity += intensity;
       }
    #else
       OutIntensity = saturate(dot(WorldNormal, float3(0.5, 0.5, -0.5)));
    #endif
}