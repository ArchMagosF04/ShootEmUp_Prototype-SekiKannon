using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private IObjectPool<SoundEmitter> soundEmitterPool;
    readonly List<SoundEmitter> activeSoundEmitters = new List<SoundEmitter>();
    public readonly Queue<SoundEmitter> FrequentSoundEmitters = new Queue<SoundEmitter>();

    [SerializeField] private SoundEmitter soundEmitterPrefab;
    [SerializeField] bool collectionCheck = true;
    [SerializeField] int defaultCapacity = 10;
    [SerializeField] int maxPoolSize = 100;
    [SerializeField] int maxSoundInstances = 30;

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

    private void Start()
    {
        InitializePool();
    }

    public SoundBuilder CreateSound() => new SoundBuilder(this);

    public bool CanPlaySound(SoundData data)
    {
        if (!data.IsFrequent) return true;

        if (FrequentSoundEmitters.Count >= maxSoundInstances)
        {
            try
            {
                FrequentSoundEmitters.Dequeue().Stop();
                return true;
            }
            catch
            {
                Debug.Log("SoundEmitter is already released");
            }
            return false;
        }
        return true;
    }

    public SoundEmitter GetSoundEmitter()
    {
        return soundEmitterPool.Get();
    }

    public void ReturnToPool(SoundEmitter soundEmitter)
    {
        soundEmitterPool.Release(soundEmitter);
    }

    private void OnDestroyPoolObject(SoundEmitter soundEmitter)
    {
        Destroy(soundEmitter.gameObject);
    }

    private void OnReturnedToPool(SoundEmitter soundEmitter)
    {
        soundEmitter.gameObject.SetActive(false);
        activeSoundEmitters.Remove(soundEmitter);
    }

    private void OnTakeFromPool(SoundEmitter soundEmitter)
    {
        soundEmitter.gameObject.SetActive(true);
        activeSoundEmitters.Add(soundEmitter);
    }

    private SoundEmitter CreateSoundEmitter()
    {
        var soundEmitter = Instantiate(soundEmitterPrefab);
        soundEmitter.gameObject.SetActive(false);
        return soundEmitter;
    }

    private void InitializePool()
    {
        soundEmitterPool = new ObjectPool<SoundEmitter>(CreateSoundEmitter, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionCheck, defaultCapacity, maxPoolSize);
    }
}
