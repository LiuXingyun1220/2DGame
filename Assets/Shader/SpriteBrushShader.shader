Shader "Custom/SpriteBrush"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}  // 主贴图
        _Color ("Tint", Color) = (1,1,1,1)     // 颜色叠加
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha  // 开启透明混合
        ZWrite Off                       // 关闭深度写入
        Cull Off                         // 双面渲染

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            fixed4 _Color;

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 texColor = tex2D(_MainTex, i.uv);
                return texColor * _Color;
            }
            ENDCG
        }
    }
}
