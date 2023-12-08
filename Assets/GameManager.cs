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
        Time.timeScale = 1;
        LoadGame();
    }

    private void LoadGame()
    {
        coinAmount = PlayerPrefs.GetInt("CoinAmount", coinAmount);
        killingEnemies = PlayerPrefs.GetInt("killingCount", killingEnemies);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        UIManager.Instance.levelFailUI.SetActive(false);
    }
}
