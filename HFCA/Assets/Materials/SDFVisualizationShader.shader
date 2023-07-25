Shader "Custom/SDFVisualizationShader" {
    Properties {
        _MainTex ("Texture3D (RGB)", 3D) = "white" {}
    }

    SubShader {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf Lambert

        sampler3D _MainTex;

        struct Input {
            float3 worldPos;
        };

        void surf (Input IN, inout fixed4 color) {
            // 从Texture3D中采样并使用红色通道的值作为距离
            float distance = tex3D(_MainTex, IN.worldPos).r;

            // 可视化距离，你可以根据需要调整颜色
            color = float4(1 - distance, 0, distance, 1); // 从蓝色（最远）到红色（最近）的渐变
        }
        ENDCG
    }
    FallBack "Diffuse"
}
