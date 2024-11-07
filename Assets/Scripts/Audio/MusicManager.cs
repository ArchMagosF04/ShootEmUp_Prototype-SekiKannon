using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField] private SoundLibraryObject musicLibrary;

    [SerializeField] private AudioSource sourceA;
    [SerializeField] private AudioSource sourceB;
    private bool IsPlayingA;

    private int currentTrackIndex = -1;

    [SerializeField] private float timeToFade = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void SwitchTrack(int trackIndex)
    {
        if (currentTrackIndex != trackIndex)
        {
            //StopAllCoroutines();

            ////StartCoroutine(FadeTrack(musicLibrary.soundData[trackIndex].Clip));
            //if (IsPlayingA)
            //{
            //    StartCoroutine(FadeTrack(musicLibrary.soundData[trackIndex].Clip, sourceA, sourceB));
            //    IsPlayingA = false;
            //}
            //else
            //{
            //    StartCoroutine(FadeTrack(musicLibrary.soundData[trackIndex].Clip, sourceB, sourceA));
            //    IsPlayingA = true;
            //}

            sourceA.Stop();
            sourceA.clip = musicLibrary.soundData[trackIndex].Clip;
            sourceA.Play();


            currentTrackIndex = trackIndex;
        }
    }

    private IEnumerator FadeTrack(AudioClip clip, AudioSource currentSource, AudioSource nextSource)
    {
        float timeElapsed = 0;

        nextSource.clip = clip;

        nextSource.Play();

        while (timeElapsed < timeToFade)
        {
            nextSource.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
            currentSource.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        nextSource.volume = 1f;
        currentSource.volume = 0f;

        currentSource.Stop();

        //if (IsPlayingA)
        //{
        //    sourceB.clip = clip;
        //    sourceB.Play();

        //    while (timeElapsed < timeToFade)
        //    {
        //        sourceB.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
        //        sourceA.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
        //        timeElapsed += Time.deltaTime;
        //        yield return null;
        //    }

        //    sourceA.Stop();
        //}
        //else
        //{
        //    sourceA.clip = clip;
        //    sourceA.Play();

        //    while (timeElapsed < timeToFade)
        //    {
        //        sourceA.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
        //        sourceB.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
        //        timeElapsed += Time.deltaTime;
        //        yield return null;
        //    }

        //    sourceB.Stop();
        //}
    }
}
