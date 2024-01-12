#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

matrix WorldViewProjection;
matrix World;

texture Texture;
float3 DiffuseColor = float3(1, 1, 1);

sampler BasicTextureSampler = sampler_state
{
    texture = <Texture>;
    MinFilter = Anisotropic; // Minification filter
    MagFilter = Anisotropic; // Magnification filter
    MipFilter = Linear; // Mip-mapping filter
};

struct VertexShaderInput
{
    float4 Position : POSITION0;
    //float4 WorldPosition : POSITION1;
    float2 UV : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float2 UV : TEXCOORD0;
    float3 WorldPosition : TEXCOORD1;
};

VertexShaderOutput MainVS(in VertexShaderInput input)
{
    VertexShaderOutput output = (VertexShaderOutput) 0;

    output.Position = mul(input.Position, WorldViewProjection);
    output.UV = input.UV;
    output.WorldPosition = mul(input.Position, World);

    return output;
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float3 output = DiffuseColor * tex2D(BasicTextureSampler, input.UV).rgb; 
    float3 posColor = float3(abs(input.WorldPosition.x), abs(input.WorldPosition.y), abs(input.WorldPosition.z));
    float3 tint = float3(posColor.x, posColor.y, posColor.z) * 0.5;
    output *= tint;
    
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
