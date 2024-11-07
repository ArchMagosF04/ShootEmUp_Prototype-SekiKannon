using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class SoundData //This class keeps the data of the sound that will be played.
{
    public string Id;
    public AudioClip Clip;
    public AudioMixerGroup MixerGroup;
    [Range(0f, 1f)] public float Volume = 1f;
    public bool Loop;
    public bool PlayOnAwake;
    public bool IsFrequent;
}
