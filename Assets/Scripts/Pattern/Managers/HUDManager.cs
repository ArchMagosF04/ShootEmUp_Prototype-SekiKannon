using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;

    public bool IsPaused {  get; private set; }
    public bool IsGameOver { get; private set;}

    [SerializeField] private GameObject gameHUD;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject gameOverMenu;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        InitializeHUD();
    }

    public void InitializeHUD()
    {
        ResumeGame();
        winMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        gameHUD.SetActive(true);
    }

    private void OnEnable()
    {
        Invoke("SubscribeToEvents", 5f);
    }

    private void OnDisable()
    {
        Player_Health.OnPlayerDeath -= LoseGame;
        BossHealth.OnEnemyDeath -= WinGame;
    }

    private void SubscribeToEvents()
    {
        Player_Health.OnPlayerDeath += LoseGame;
        BossHealth.OnEnemyDeath += WinGame;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        IsPaused = true;
        pauseMenu?.SetActive(true);
        gameHUD.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        IsPaused = false;
        pauseMenu?.SetActive(false);
        gameHUD.SetActive(true);
    }

    public void WinGame()
    {
        Time.timeScale = 0f;
        IsPaused = true;
        winMenu?.SetActive(true);
        gameHUD.SetActive(false);
    }

    public void LoseGame()
    {
        Time.timeScale = 0f;
        IsPaused = true;
        gameOverMenu?.SetActive(true);
        gameHUD.SetActive(false);
    }

    public void RestartScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
