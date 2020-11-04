Shader "Unlit/FaceOnHead"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
        SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
            "PreviewType" = "Plane"
            "CanUseSpriteAtlas" = "True"
        }

        Pass
        {
            Cull Off
            Lighting Off
            ZWrite Off
            Fog { Mode Off }
            Blend SrcAlpha OneMinusSrcAlpha


            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile DUMMY PIXELSNAP_ON

            sampler2D _MainTex;
            float4 _Color;
            half _Range;
            struct Vertex
            {
                float4 vertex : POSITION;
                float2 uv_MainTex : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
            };

            struct Fragment
            {
                float4 vertex : POSITION;
                float2 uv_MainTex : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
            };

            Fragment vert(Vertex v)
            {
                Fragment o;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv_MainTex = v.uv_MainTex;
                o.uv2 = v.uv2;

                return o;
            }

            float4 frag(Fragment IN) : COLOR
            {
                float v = IN.uv_MainTex.y;
                
                half4 c = tex2D(_MainTex, IN.uv_MainTex);
                //o.rgb = c.rgb;
                if (v > 0.7)
                {
                    c.rgb = (1, 1, 1);
                }
                return c;
            }
            ENDCG
        }
    }
}
