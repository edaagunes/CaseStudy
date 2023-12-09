using Blended;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI deathText;
    public Image healtBar;
    public GameObject levelFailUI;

    private void Start()
    {
        // Setting initial values for UI elements when the scene starts
        // Setting the health bar fill amount based on the player's health from GameManager
        
        healtBar.fillAmount = GameManager.Instance.playerHealth;
        coinText.text = PlayerPrefs.GetInt("CoinAmount", GameManager.Instance.coinAmount).ToString();
        deathText.text = PlayerPrefs.GetInt("killingCount", GameManager.Instance.killingEnemies).ToString();
    }
}