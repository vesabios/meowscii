using UnityEngine;
using System.Collections;

public class FractalNoise {

    int octaves = 8;
    float lacunarity = 2.0f;
    float persistence = 0.5f;
    float baseFrequency = 1.0f;
    float baseAmplitude = 1.0f;

    public float Noise(float sample_x, float sample_y)
    {
        float sum = 0;
        float freq = baseFrequency;
        float amp = baseAmplitude;

        for (int i = 0; i < octaves; ++i)
        {
            sum += Mathf.PerlinNoise(sample_x * freq, sample_y * freq) * amp;

            freq *= lacunarity;
            amp *= persistence;
        }

        return sum;
    }

}

