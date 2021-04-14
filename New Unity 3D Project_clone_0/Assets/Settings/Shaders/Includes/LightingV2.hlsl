void GetLightingInformation_float(out float3 Direction, out float3 Color, out float Attenuation)
{
#ifdef SHADERGRAPH_PREVIEW
    Direction = float3(-0.5, 0.5, -0.5);
    Color = float3(1, 1, 1);
    Attenuation = 0.4;
#else
    Light light = GetMainLight();
    Direction = light.direction;
    Attenuation = light.distanceAttenuation;
    Color = light.color;
#endif
}

void AddAdditionalLights_float(float3 WorldPosition, float3 WorldNormal, float3 WorldView,
    float MainDiffuse, float3 MainColor,
    out float Diffuse, out float3 Color)
{
    Diffuse = MainDiffuse;
    Color = MainColor * MainDiffuse;

#ifndef SHADERGRAPH_PREVIEW
    int pixelLightCount = GetAdditionalLightsCount();
    for (int i = 0; i < pixelLightCount; ++i) {
        Light light = GetAdditionalLight(i, WorldPosition);
        half NdotL = saturate(dot(WorldNormal, light.direction));
        half atten = light.distanceAttenuation * light.shadowAttenuation;
        half thisDiffuse = atten * NdotL;
        //half thisSpecular = LightingSpecular(thisDiffuse, light.direction, WorldNormal, WorldView, 1, Smoothness);
        Diffuse += thisDiffuse;
        //Specular += thisSpecular;
        //Color += light.color * (thisDiffuse + thisSpecular);
        Color += light.color * thisDiffuse;
    }
#endif

    //half total = Diffuse + Specular;

    half total = Diffuse;
    // If no light touches this pixel, set the color to the main light's color
    Color = total <= 0 ? MainColor : Color / total;
}
