using Blended;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public Transform player;
    public int killingEnemies;
    public int coinAmount;
    public int playerHealth=100;

    private void Start()
    {
        // Ensures the game is running at normal speed (not paused)
        Time.timeScale = 1;
        
        // Loads game-related data (coinAmount and killingEnemies) from player preferences
        LoadGame();
    }

    private void LoadGame()
    {
        // Retrieving the stored coinAmount and killingEnemies from player preferences
        coinAmount = PlayerPrefs.GetInt("CoinAmount", coinAmount);
        killingEnemies = PlayerPrefs.GetInt("killingCount", killingEnemies);
    }

    public void RestartGame()
    {
        // Loads the current scene to restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        // Disables the level fail UI (if it was active)
        UIManager.Instance.levelFailUI.SetActive(false);
    }
}
