Shader "Custom/UnlitTextureMix"
{
    Properties
    {
        _Tex1 ("Texture1", 2D) = "white" {}
        _Tex2 ("Texture2", 2D) = "white" {}
        _MixValue("MixValue", Range(0,1)) = 0.5
        _Color("Main Color", COLOR) = (1,1,1,1)
        _Height("Height", Range(0,20)) = 0.5 // сила изгиба
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _Tex1; // текстура1
            float4 _Tex1_ST;
            sampler2D _Tex2; // текстура2
            float4 _Tex2_ST;
            float _MixValue; // параметр смешивания
            float4 _Color; //цвет, которым будет окрашиваться изображение

            float _Height; // сила изгиба


            struct v2f
            {
                float2 uv : TEXCOORD0; // UV-координаты вершины
                float4 vertex : SV_POSITION; // координаты вершины
            };


            v2f vert (appdata_full v)
            {
                v.vertex.xyz -= v.normal * _Height * (-0.5 + v.texcoord.x) * (-0.5 + v.texcoord.x);
                
                v2f result;
                result.vertex = UnityObjectToClipPos(v.vertex);
                result.uv = TRANSFORM_TEX(v.texcoord, _Tex1);
                return result;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 color;
                color = tex2D(_Tex1, i.uv) * _MixValue;
                color += tex2D(_Tex2, i.uv) * (1 - _MixValue);
                color = color * _Color;
                return color;
            }

            ENDCG
        }
    }
}
