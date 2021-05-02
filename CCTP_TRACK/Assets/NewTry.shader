// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/NewTry"
{
    Properties{
     _MainTex("Outer Texture", 2D) = "" {}
     _FadeTex("Inner Texture", 2D) = "" {}
     _Radius("Radius", Range(0, 10)) = 1
     _PlayerPosition("Center", Vector) = (0,0,0,0)
     _FadeFactor("Fade Factor",Range(0,32)) = 1
    }
        SubShader{
            Pass {
            CGPROGRAM
                     #pragma vertex vert
                     #pragma fragment frag
                     #pragma fragmentoption ARB_precision_hint_fastest
                     #include "UnityCG.cginc"
                     sampler2D _MainTex;
                     sampler2D _FadeTex;
                     float4 _PlayerPosition;
                     float _Radius;
                     float _FadeFactor;

                     struct v2f
                     {
                         float4 pos  : POSITION;
                         float2 uv   : TEXCOORD0;
                         float3 wpos : TEXCOORD1;
                         float3 vpos : TEXCOORD2;
                         float3 color : COLOR0;
                     };

                    v2f vert(appdata_img v)
                    {
                        v2f o;
                        o.pos = UnityObjectToClipPos(v.vertex);
                        float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                        float3 disp = worldPos - _PlayerPosition;
                        o.color = disp;
                        o.wpos = worldPos;
                        o.vpos = v.vertex.xyz;
                        o.uv = v.texcoord.xy;
                        return o;
                    }

                    float4 frag(v2f i) : COLOR
                    {
                        float alpha = clamp(length(i.color) / _Radius,0,1);
                        float3 temp = lerp(tex2D(_FadeTex,i.uv),tex2D(_MainTex,i.uv),pow(alpha,_FadeFactor));
                        return float4(temp.x,temp.y,temp.z,1);
                    }
                     ENDCG
                 }
     }
}
