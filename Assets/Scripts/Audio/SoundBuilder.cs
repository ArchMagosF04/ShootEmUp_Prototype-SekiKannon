using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBuilder
{
    readonly SoundManager soundManager;
    private SoundData soundData;
    Vector3 position = Vector3.zero;
    private bool randomPitch = false;

    public SoundBuilder(SoundManager soundManager)
    {
        this.soundManager = soundManager;
    }

    public SoundBuilder WithSoundData(SoundData soundData)
    {
        this.soundData = soundData;
        return this;
    }

    public SoundBuilder WithPosition(Vector3 position)
    {
        this.position = position;
        return this;
    }

    public SoundBuilder WithRandomPitch()
    {
        this.randomPitch = true;
        return this;
    }

    public void Play()
    {
        if (!soundManager.CanPlaySound(soundData)) 
        {
            return;
        }

        SoundEmitter soundEmitter = soundManager.GetSoundEmitter();
        soundEmitter.Initialize(soundData);
        soundEmitter.transform.position = position;
        soundEmitter.transform.parent = soundManager.transform;

        if (randomPitch)
        {
            soundEmitter.WithRandomPitch();
        }

        if (soundData.IsFrequent)
        {
            soundManager.FrequentSoundEmitters.Enqueue(soundEmitter);
        }
        soundEmitter.Play();
    }
}
