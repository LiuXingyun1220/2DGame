Shader "Hidden/CustomEffects/EyeOpen"
{
    Properties
    {
        [HideInInspector]_MainTex ("Source Texture", 2D) = "white" {}
        _Strength ("睁眼强度", Range(0, 1)) = 1
        _Width ("眼缝宽度", Range(1, 3)) = 1.5
        _EdgeWidth ("边缘宽度", Range(0.01, 0.5)) = 0.1
    }

    SubShader
    {
        Tags { 
            "RenderType"="Opaque" 
            "RenderPipeline" = "UniversalPipeline"
        }
        
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            Name "EyeOpening"

            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            #pragma multi_compile_local _ _USE_FAST_BLUR
            #pragma multi_compile _ STEREO_INSTANCING_ON UNITY_SINGLE_PASS_STEREO
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            float4 _MainTex_TexelSize;
            
            CBUFFER_START(UnityPerMaterial)
                float _Strength;
                float _Width;
                float _EdgeWidth;
            CBUFFER_END
            
            static const float4 _ScreenColor = float4(0, 0, 0, 1);
            static const float2 BLUR_OFFSETS[9] = {
                float2(-1, -1), float2(0, -1), float2(1, -1),
                float2(-1,  0), float2(0,  0), float2(1,  0),
                float2(-1,  1), float2(0,  1), float2(1,  1)
            };

            Varyings Vert(Attributes input)
            {
                Varyings output = (Varyings)0;
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);
                
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                output.uv = input.uv;
                return output;
            }

            float4 SampleBox(float2 uv, float2 texelSize, float offset)
            {
                float4 color = 0;
                
                UNITY_UNROLL
                for(int i = 0; i < 9; i++)
                {
                    float2 offsetUV = uv + BLUR_OFFSETS[i] * texelSize * offset;
                    color += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, offsetUV);
                }
                
                return color * 0.111111f; // 1.0 / 9.0
            }

            float4 BoxBlur(float2 uv, float intensity)
            {
                float2 texelSize = _MainTex_TexelSize.xy * intensity * 2;
                
                #ifdef _USE_FAST_BLUR
                    return SampleBox(uv, texelSize, 1.0);
                #else
                    float4 color = 0;
                    UNITY_UNROLL
                    for(int i = 0; i < 3; i++)
                    {
                        color += SampleBox(uv, texelSize, (i + 1) * 0.333333f);
                    }
                    return color * 0.333333f;
                #endif
            }

            void GetEyelidMasks(float2 uv, out float2 masks)
            {
                float2 adjustedUV = (uv - 0.5) * 2;
                adjustedUV.x /= _Width;
                adjustedUV.y /= lerp(0.0, 1.4, _Strength);

                float dist = length(adjustedUV);
                float transitionFactor = smoothstep(0.95, 1.0, _Strength);
                
                float baseMask = smoothstep(1.0009999, 1.001, dist);
                
                float clipAdjust = adjustedUV.y > 0 ? 0.95 : 1.0;
                float scatterStart = (0.95 - _EdgeWidth) * clipAdjust - 0.25;
                float scatterMask = smoothstep(scatterStart, 1.0, dist);
                
                masks = float2(baseMask, scatterMask) * (1 - transitionFactor);
            }

            float4 Frag(Varyings input) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

                float blurAmount = (1 - _Strength) * 5 * (1.0 - _Strength * 0.7);
                float4 sceneColor = BoxBlur(input.uv, blurAmount);
                
                float2 masks;
                GetEyelidMasks(input.uv, masks);
                float eyelidMask = masks.x;
                float scatteringMask = masks.y;

                float4 scatterBlur = BoxBlur(input.uv, blurAmount * 0.5);
                float4 scatteringColor = lerp(_ScreenColor, scatterBlur, 0.3 * (1.0 - _Strength));
                
                float4 finalColor = sceneColor;
                finalColor = lerp(finalColor, scatteringColor, saturate(scatteringMask - eyelidMask));
                return lerp(finalColor, _ScreenColor, eyelidMask);
            }
            ENDHLSL
        }
    }
}