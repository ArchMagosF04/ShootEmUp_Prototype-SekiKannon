using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio Library/Sound Dictionary")]
public class SoundLibraryObject : ScriptableObject
{
    [SerializeField] public SoundData[] soundData;
    public Dictionary<string, SoundData> soundDataDictionary = new Dictionary<string, SoundData>();

    //public void Initialize()
    //{
    //    foreach (var data in soundData)
    //    {
    //        soundDataDictionary.Add(data.Id, data);
    //    }
    //}
}
