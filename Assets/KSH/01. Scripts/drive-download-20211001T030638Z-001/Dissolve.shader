Shader "Custom/Dissolve"
{
    Properties
    {
        _MainTex ("MainTex", 2D) = "white" {}
        _NoiseTex("NoiseTex", 2D) = "white" {}
        _Dp("DissolvePower", Range(0, 1.0)) = 0.02
        _Ec("EdgeColor", Color) = (1, 0, 0, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent"}
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard alpha:fade

        sampler2D _MainTex;
        sampler2D _NoiseTex;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_NoiseTex;
        };

        float _Dp;
        float4 _Ec;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 n = tex2D(_NoiseTex, IN.uv_NoiseTex);
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            o.Emission = step(n.r - 0.025, _Dp) * _Ec;
            o.Alpha = 1 - step(n.r, _Dp);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
