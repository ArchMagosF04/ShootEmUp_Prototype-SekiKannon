using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent (typeof (AudioSource))]
public class SoundEmitter : MonoBehaviour
{
    public SoundData Data {  get; private set; }

    private AudioSource audioSource;
    private Coroutine playingCoroutine;

    private void Awake()
    {
        audioSource = gameObject.GetOrAdd<AudioSource>(); //Gets the audiosource component or creates one if there is none.
    }

    public void Play() //Starts playing the clip
    {
        if (playingCoroutine != null)
        {
            StopCoroutine(playingCoroutine);
        }

        audioSource.Play();
        playingCoroutine = StartCoroutine(WaitForSoundToEnd());
    }

    private IEnumerator WaitForSoundToEnd() //When the sound stops playing the emitter is returned to the pool.
    {
        yield return new WaitWhile(() => audioSource.isPlaying);
        Stop();
    }

    public void Stop() //Stops the sound from playing and returns it to the pool.
    {
        if (playingCoroutine != null)
        {
            StopCoroutine(playingCoroutine);
            playingCoroutine = null;
        }

        audioSource.Stop();
        SoundManager.Instance.ReturnToPool(this);
    }

    public void Initialize(SoundData data) //Gives the data to the emitter;
    {
        Data = data;
        audioSource.clip = data.Clip;
        audioSource.outputAudioMixerGroup = data.MixerGroup;
        audioSource.loop = data.Loop;
        audioSource.playOnAwake = data.PlayOnAwake;
    }

    public void WithRandomPitch(float min = -0.03f, float max = 0.03f)
    {
        audioSource.pitch += Random.Range(min, max);
    }
}
