Shader "Custom/RadialGradient"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" { }
        _Center ("Center Position", Vector) = (0, 0, 0, 0)
        _MaxDistance ("Max Distance", Float) = 1.0
        _InnerColor ("Inner Color", Color) = (1, 0, 0, 1)  // Red
        _OuterColor ("Outer Color", Color) = (0, 0, 0, 1)  // Black
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
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
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            float4 _Center;           // Center of the gradient
            float _MaxDistance;       // Maximum distance for the gradient
            float4 _InnerColor;       // Color at the center
            float4 _OuterColor;       // Color at the edges
            sampler2D _MainTex;      // Dummy texture property to prevent warning

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                // Get the UV coordinates, which range from 0 to 1 for the whole screen
                float2 screenPos = i.uv * 2.0 - 1.0; // Convert to range -1 to 1 for screen space
                float distanceFromCenter = length(screenPos);  // Calculate distance from the center (0, 0)

                // Interpolate using smoothstep for a smooth transition between colors
                float t = distanceFromCenter / _MaxDistance; // Normalize the distance to the max distance
                t = smoothstep(0.0, 1.0, t); // Apply smoothstep for smoother interpolation

                // Lerp between inner color (red) and outer color (black)
                return lerp(_OuterColor, _InnerColor, 1.0 - t);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
