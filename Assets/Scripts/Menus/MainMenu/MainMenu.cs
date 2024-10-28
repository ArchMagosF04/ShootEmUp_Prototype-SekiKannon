using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private Button startButton;
    private Button exitButton;

    private void Start()
    {
        startButton = GetComponentInChildren<Button>();
        exitButton = GetComponentInChildren<Button>();
        startButton.onClick.AddListener(PlayGame);
        exitButton.onClick.AddListener(ExitGame);
    }
    public void PlayGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
