#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

matrix WorldViewProjection;
float3 CameraPosition;
matrix World;

texture Texture;
float3 DiffuseColor = float3(1, 1, 1);
float3 AmbientColor = float3(0.15, 0.15, 0.15);

float3 LightDirection = float3(0, 0, 1);
float3 LightColor = float3(1, 0, 0); // Lambertian light

float3 SpecularPower = 4;
float3 SpecularColor = float3(0, 0, 1); // Specular highlight

sampler BasicTextureSampler = sampler_state
{
    texture = <Texture>;
    MinFilter = Anisotropic;
    MagFilter = Anisotropic;
    MipFilter = Linear; 
};

struct VertexShaderInput
{
    float4 Position : POSITION0;
    float2 UV : TEXCOORD0;
    float3 Normal : NORMAL0;
};

struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float3 Normal : NORMAL0;
    float2 UV : TEXCOORD0;
    float3 ViewDirection : TEXCOORD1;
};

VertexShaderOutput MainVS(in VertexShaderInput input)
{
    VertexShaderOutput output = (VertexShaderOutput) 0;

    float3 worldPosition = mul(input.Position, World);
    output.Position = mul(input.Position, WorldViewProjection);
    output.UV = input.UV;
    output.Normal = normalize(mul(input.Normal, World));
    output.ViewDirection = normalize(worldPosition - CameraPosition);

    return output;
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float3 diffuse = DiffuseColor * tex2D(BasicTextureSampler, input.UV).xyz; //Multiply the diffuse color with the texture color
    float3 lightDir = normalize(LightDirection); //Normalize for correct dot product results
    float3 lighting = saturate(dot(lightDir, input.Normal)) * LightColor; //Add the lambertian lighting

    //Specular Lighting
    float3 refl = reflect(lightDir, input.Normal); //Calculate the reflection vector
    lighting += pow(saturate(dot(refl, input.ViewDirection)), SpecularPower) * SpecularColor; //Add specular highlights
    float3 output = (saturate(AmbientColor) + lighting) * diffuse; //Calculate the final color
    
    return float4(output, 1);
}

technique BasicColorDrawing
{
    pass P0
    {
        VertexShader = compile VS_SHADERMODEL MainVS();
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};
