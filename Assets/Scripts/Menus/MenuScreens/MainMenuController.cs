using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] public string newGameLevel;

    public void StartGame()
    {
        LoadingManager.Instance.LoadScene(newGameLevel);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
