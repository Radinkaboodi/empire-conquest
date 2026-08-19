using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player player;
    [SerializeField] private BuildingManager buildingManager;
    [SerializeField] private CombatManager combatManager;
    [SerializeField] private UIManager uiManager;

    private bool isGameRunning = true;
    private int currentLevel = 1;
    private int totalWins = 0;
    private float gameTime = 0f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        InitializeGame();
    }

    private void Update()
    {
        if (isGameRunning)
        {
            gameTime += Time.deltaTime;
            player.UpdateResources(Time.deltaTime);
        }
    }

    public void InitializeGame()
    {
        player.Initialize();
        buildingManager.Initialize();
        uiManager.Initialize();
        Debug.Log("🎮 Game Initialized!");
    }

    public void LevelUp()
    {
        currentLevel++;
        player.AddExperience(100);
        uiManager.UpdateLevelDisplay(currentLevel);
        Debug.Log($"📈 Level Up! Now Level {currentLevel}");
    }

    public void WinBattle()
    {
        totalWins++;
        player.AddResources(500, 300, 200, 150);
        LevelUp();
        uiManager.ShowWinMessage();
    }

    public void LoseBattle()
    {
        player.TakeDamage(100);
        uiManager.ShowLoseMessage();
    }

    public void PauseGame()
    {
        isGameRunning = false;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isGameRunning = true;
        Time.timeScale = 1f;
    }

    public Player GetPlayer() => player;
    public int GetCurrentLevel() => currentLevel;
    public int GetTotalWins() => totalWins;
    public float GetGameTime() => gameTime;
}