
Shader "Screen" {
	Properties {
		_CharMap("Character Map", 2D) = "white" {}
		_Palette("Palette", 2D) = "white" {}
	}
	
	SubShader {

		Tags { "RenderType" = "Opaque" }
		LOD 100

		Pass {

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0
			#pragma multi_compile_fog

			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				half2 texcoord : TEXCOORD0;
			};

			sampler2D _CharMap;
			float4 _CharMap_ST;
			sampler2D _BaseLayer;
			sampler2D _FloatingLayer;

			float4 _BaseLayer_ST;
			sampler2D _Palette;
			float4 _Dims;
			float4 _CharDims;

			float4 _BaseLayerDims;



			v2f vert(appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _BaseLayer);
				return o;
			}

			float2 charMapOffset(int c, int page) 
			{

				float u = ((int)fmod(c, _CharDims.x)) * _CharDims.z;
				float v = (((16 - 1) - (int)floor(c / 16)) * _CharDims.w);

				v += ((3-page)*0.25);

				return float2(u, v);

			}

			fixed4 fromPalette(float val) 
			{
				half u = fmod(val * 4,1); // only 64 colors, need to scale back up to normalized range to properly index a 64 color palette texture
				return tex2D(_Palette, float2(u, 0.5));

			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 layer = tex2D(_FloatingLayer, i.texcoord );
				fixed4 buf = tex2D(_BaseLayer, (i.texcoord * _BaseLayerDims.zw) + _BaseLayerDims.xy);

				if (layer.a == 1) {
					buf = layer;
				}

				fixed4 fg = fromPalette(buf.g);
				fixed4 bg = fromPalette(buf.b);

				int character = int(buf.r * 255.0f);


				float2 charuv = fmod(i.texcoord * _Dims.xy, float2(1, 1)); 
				// now normalized 

				charuv *= float2(8, 8); // scale up to dimensions of glyph

				charuv.x = (int)charuv.x;
				charuv.y = (int)charuv.y; // clamp to nearest int
				charuv *= float2(1.0f / 8.0f, 1.0f / 8.0f); // back to normalized range
				charuv *= _CharDims.zw;

				int codepage = (buf.g * 256) / 64;

				charuv += charMapOffset(character, codepage);

				fixed4 col = tex2D(_CharMap, charuv);

	

				return lerp(bg,fg,col.r);
			}

			ENDCG
		}
	}
}
