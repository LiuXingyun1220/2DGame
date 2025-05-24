Shader "UI/Ripple"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _RippleCenter ("Ripple Center", Vector) = (0.5, 0.5, 0, 0)
        _RippleTime ("Ripple Time", Range(0, 1)) = 0
        _RippleWidth ("Ripple Width", Range(0, 1)) = 1
        _RippleColor ("Ripple Color", Color) = (1,1,1,1)
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float2 _RippleCenter;
            float _RippleTime;
            float _RippleWidth;
            fixed4 _RippleColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 计算到中心的距离
                float distance = length(i.uv - _RippleCenter);
                // 波纹扩散范围
                // 更宽的平滑区间
                float edge0 = _RippleTime - _RippleWidth * 1.2;
                float edge1 = _RippleTime + _RippleWidth * 1.2;
                // 单一 smoothstep 形成波纹带
                float ripple = smoothstep(edge0, _RippleTime, distance) * (1.0 - smoothstep(_RippleTime, edge1, distance));
                // 对波纹进行二次平滑
                ripple = pow(ripple, 1.5);
                // 归一化距离，0为中心，1为波纹外缘
                float normalizedDist = saturate((distance - (_RippleTime - _RippleWidth)) / (_RippleWidth * 2));
                // 混合颜色
                fixed4 col = tex2D(_MainTex, i.uv);
                float rippleAlpha = ripple * _RippleColor.a * normalizedDist;
                col.rgb = lerp(col.rgb, _RippleColor.rgb, ripple * _RippleColor.a);
                return col;
            }
            ENDCG
        }
    }
}