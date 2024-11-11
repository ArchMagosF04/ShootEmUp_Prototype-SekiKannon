using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private string newGameLevel;
    [SerializeField] private SoundLibraryObject soundLibrary;

    public void StartGame()
    {
        LoadingManager.Instance.LoadScene(newGameLevel);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PlaySound(int soundIndex)
    {
        SoundManager.Instance.CreateSound().WithSoundData(soundLibrary.soundData[soundIndex]).Play();
    }
}
