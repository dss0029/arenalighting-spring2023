using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPeer : MonoBehaviour
{
    public AudioSource audioSource;

    public float bufferDecreaseSpeed = 0.005f;
    public float bufferDecreaseAcceleration = 1.2f;

    [HideInInspector]
    public float[] audioBand, audioBandBuffer;

    [HideInInspector]
    public float[] audioBand64, audioBandBuffer64;

    [HideInInspector]
    public float amplitude, amplitudeBuffer;

    public float audioProfile;
    
    public enum Channel { Stereo, Left, Right };
    public Channel channel = new Channel();

    float[] samplesLeft = new float[512];
    float[] samplesRight = new float[512];

    // Audio 8
    float[] freqBand = new float[8];
    float[] bandBuffer = new float[8];
    float[] bufferDecrease = new float[8];
    float[] freqBandHighest = new float[8];

    // Audio 64
    float[] freqBand64 = new float[64];
    float[] bandBuffer64 = new float[64];
    float[] bufferDecrease64 = new float[64];
    float[] freqBandHighest64 = new float[64];

    float amplitudeHighest;
    
    // Start is called before the first frame update
    void Start()
    {
        audioBand = new float[8];
        audioBandBuffer = new float[8];

        audioBand64 = new float[64];
        audioBandBuffer64 = new float[64];

        audioSource = GetComponent<AudioSource>();
        AudioProfile();
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        MakeFrequencyBands64();
        BandBuffer();
        BandBuffer64();
        CreateAudioBands();
        CreateAudioBands64();
        GetAmplitude();
    }

    public void GetSpectrumAudioSource()
    {
        audioSource.GetSpectrumData(samplesLeft, 0, FFTWindow.Blackman);
    }

    void MakeFrequencyBands()
    {
        int count = 0;

        for (int i = 0; i < 8; i++)
        {
            float average = 0f;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if (i == 7)
            {
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++)
            {
                if (channel == Channel.Stereo)
                {
                    average += (samplesLeft[count] + samplesRight[count]) * (count + 1);
                }
                else if (channel == Channel.Left)
                {
                    average += samplesLeft[count] * (count + 1);
                }
                else
                {
                    average += samplesRight[count] * (count + 1);
                }
                
                count++;
            }

            average /= count;

            freqBand[i] = average * 10f;
        }
    }

    void MakeFrequencyBands64()
    {
        int count = 0;
        int sampleCount = 1;
        int power = 0;

        for (int i = 0; i < 8; i++)
        {
            float average = 0f;
            
            if (i > 8 && i % 8 == 0)
            {
                power++;
                sampleCount = (int)Mathf.Pow(2, power);

                if (power == 3)
                {
                    sampleCount -= 2;
                }
            }

            for (int j = 0; j < sampleCount; j++)
            {
                if (channel == Channel.Stereo)
                {
                    average += (samplesLeft[count] + samplesRight[count]) * (count + 1);
                }
                else if (channel == Channel.Left)
                {
                    average += samplesLeft[count] * (count + 1);
                }
                else
                {
                    average += samplesRight[count] * (count + 1);
                }

                count++;
            }

            average /= count;

            freqBand64[i] = average * 80f;
        }
    }

    void BandBuffer()
    {
        for (int i = 0; i < 8; i++) {
            if (freqBand[i] > bandBuffer[i]) {
                bandBuffer[i] = freqBand[i];
                bufferDecrease[i] = bufferDecreaseSpeed;
            }

            else if (freqBand[i] < bandBuffer[i])
            {
                bandBuffer[i] -= bufferDecrease[i];
                bufferDecrease[i] *= bufferDecreaseAcceleration;
            }
        }
    }

    void BandBuffer64()
    {
        for (int i = 0; i < 64; i++)
        {
            if (freqBand64[i] > bandBuffer64[i])
            {
                bandBuffer64[i] = freqBand64[i];
                bufferDecrease64[i] = bufferDecreaseSpeed;
            }

            else if (freqBand64[i] < bandBuffer64[i])
            {
                bandBuffer64[i] -= bufferDecrease64[i];
                bufferDecrease64[i] *= bufferDecreaseAcceleration;
            }
        }
    }

    void CreateAudioBands()
    {
        for (int i = 0; i < 8; i++)
        {
            if (freqBand[i] > freqBandHighest[i])
            {
                freqBandHighest[i] = freqBand[i];
            }

            if (freqBandHighest[i] != 0)
            {
                audioBand[i] = (freqBand[i] / freqBandHighest[i]);
                audioBandBuffer[i] = (bandBuffer[i] / freqBandHighest[i]);
            }
        }
    }

    void CreateAudioBands64()
    {
        for (int i = 0; i < 64; i++)
        {
            if (freqBand64[i] > freqBandHighest64[i])
            {
                freqBandHighest64[i] = freqBand64[i];
            }

            if (freqBandHighest64[i] != 0)
            {
                audioBand64[i] = (freqBand64[i] / freqBandHighest64[i]);
                audioBandBuffer64[i] = (bandBuffer64[i] / freqBandHighest64[i]);
            }
        }
    }

        void GetAmplitude()
    {
        float currentAmplitude = 0f;
        float currentAmplitudeBuffer = 0f;

        for (int i = 0; i < 8; i++)
        {
            currentAmplitude += audioBand[i];
            currentAmplitudeBuffer += audioBandBuffer[i];
        }

        if (currentAmplitude > amplitudeHighest)
        {
            amplitudeHighest = currentAmplitude;
        }

        amplitude = currentAmplitude / amplitudeHighest;
        amplitudeBuffer = currentAmplitudeBuffer / amplitudeHighest;
    }

    void AudioProfile()
    {
        for (int i = 0; i < 8; i++)
        {
            freqBandHighest[i] = audioProfile;
        }
    }
}
