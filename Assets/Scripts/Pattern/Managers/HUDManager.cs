using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;

    public bool IsPaused {  get; private set; }
    public bool IsGameOver { get; private set;}

    [Header ("Panel References")]

    [SerializeField] private GameObject gameHUD;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject gameOverMenu;

    [Header("Saved Presets References")]

    [SerializeField] private SoundLibraryObject soundLibrary;

    [SerializeField] private EnemyHPBar enemyHPBarPrefab;


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

    public void PlaySound(int soundIndex)
    {
        SoundManager.Instance.CreateSound().WithSoundData(soundLibrary.soundData[soundIndex]).Play();
    }

    public void CreateBossHealthBar(EnemyHealth enemyHealth)
    {
        EnemyHPBar bar = Instantiate(enemyHPBarPrefab, gameHUD.transform);
        bar.InitializeHealthBar(enemyHealth);
    }

    public void InitializeHUD()
    {
        ResumeGame();
        winMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        gameHUD.SetActive(true);
        IsGameOver = false;
    }

    private void OnEnable()
    {
        Invoke("SubscribeToEvents", 1f);
    }

    private void OnDisable()
    {
        Player_Health.OnPlayerDeath -= LoseGame;
    }

    private void SubscribeToEvents()
    {
        Player_Health.OnPlayerDeath += LoseGame;
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
        optionsMenu.SetActive(false);
        gameHUD.SetActive(true);
    }

    public void WinGame()
    {
        IsGameOver = true;
        Time.timeScale = 0f;
        IsPaused = true;
        winMenu?.SetActive(true);
        gameHUD.SetActive(false);
    }

    public void LoseGame()
    {
        IsGameOver = true;
        Time.timeScale = 0f;
        IsPaused = true;
        gameOverMenu?.SetActive(true);
        gameHUD.SetActive(false);
    }

    public void RestartScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        LoadingManager.Instance.LoadScene(sceneName);
    }

    public void GoToMainMenu()
    {
        LoadingManager.Instance.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
