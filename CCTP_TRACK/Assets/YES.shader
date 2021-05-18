Shader "Unlit/YES"
{
    Properties
    {
        [PerRendererData]_NodePos("Node position", vector) = (0.0, 0.0, 0.0, 0.0)
        [PerRendererData]_Dist("Distance", float) = 5.0
        _MainTex("Texture", 2D) = "white" {}
        _SecondayTex("Secondary texture", 2D) = "white"{}
        _NumOfPeices("Pieces",float) = 5.0
            _Color("colour", Color) = (1,1,1,0.5)
            _isdone("isdone",float) = 0.0
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 4.0
            #include "UnityCG.cginc"

            struct v2f {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 worldPos : TEXCOORD1;
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                // We compute the world position to use it in the fragment function
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                return o;
            }
            uniform float4 _Color;
            float4 _NodePos;
            float4 _poses[1000];
            int Count = 0;
            sampler2D _MainTex;
            sampler2D _SecondayTex;
            float _Dist;
           float _NumOfPeices;
           float _isdone;

           fixed4 frag(v2f i) : SV_Target
           {
               // Depending on the distance from the player, we use a different texture
         
                if (distance(_NodePos.xyz, i.worldPos.xyz) < _Dist)
                {

                return tex2D(_SecondayTex, i.uv);
                }
                 else
                     return tex2D(_MainTex, i.uv);

               }
          

            ENDCG
        }
    }
}
