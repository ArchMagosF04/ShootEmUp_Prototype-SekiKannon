using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacadeManager : MonoBehaviour
{
    [SerializeField] private int sceneMusic;

    private void Start()
    {
        Debug.Log("Facade");
        MusicManager.Instance.SwitchTrack(sceneMusic);
    }
}
