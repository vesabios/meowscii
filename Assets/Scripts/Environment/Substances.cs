
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Substances
{
    public enum SubstanceType
    {
        Floor,
        Stone,
        Grass,
        Air
    }

    public class BaseSubstance : MonoBehaviour
    {
        public char character = ' ';

        public virtual Color GetBrush(byte lightLevel)
        {
            return Screen.GenerateBrush(59, 0, character);
        }

        public virtual Color GetBrush(Vector2 worldPosition, byte lightLevel)
        {
            return GetBrush(lightLevel);
        }

    }

    public class FractalNoiseSubstance : BaseSubstance
    {
        public int octaves = 8;
        public float lacunarity = 1.77f;
        public float persistence = 0.72f;
        public float baseFrequency = 0.06f;
        public float baseAmplitude = 1.25f;

        public float Noise(Vector2 sample)
        {
            float sum = 0;
            float freq = baseFrequency;
            float amp = baseAmplitude;

            for (int i = 0; i < octaves; ++i)
            {
                sum += Mathf.PerlinNoise(sample.x * freq, sample.y * freq) * amp;

                freq *= lacunarity;
                amp *= persistence;
            }

            return sum;
        }
    }


    public class Floor : BaseSubstance
    {
        public Floor()
        {
            character = (char)176;
        }
    }

 

    public class Stone : FractalNoiseSubstance
    {

        public Stone()
        {
            character = '#';
        }
        
        public override Color GetBrush(Vector2 worldPosition, byte lightLevel)
        {

            float v = Noise(worldPosition);
            if (v>2)
            {
                return Screen.GenerateBrush(60, 0, character);
            }

            return Screen.GenerateBrush(60, 0, '.');

        }

    }

    public class Grass : BaseSubstance
    {
        public Grass()
        {
            character = '~';
        }
    }

    public class Air : BaseSubstance
    {
        public Air()
        {
            character = ' ';
        }
    }

}
