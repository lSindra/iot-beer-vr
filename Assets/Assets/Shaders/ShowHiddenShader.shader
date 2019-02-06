// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Stencil/Stencil Mask Shader"
{
    Properties
    {
        _StencilRef("Stencil Value", Int) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque"
               "Queue" = "Geometry-20"}
        ColorMask 0
        ZWrite Off
        Stencil {
            Ref[_StencilRef]
            Comp always
            Pass replace
            ZFail replace
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                return fixed4(1,0,1,1);
            }
            ENDCG
        }
    }
}       