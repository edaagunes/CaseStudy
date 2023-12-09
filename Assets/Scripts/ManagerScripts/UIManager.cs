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
        healtBar.fillAmount = GameManager.Instance.playerHealth;
        coinText.text = PlayerPrefs.GetInt("CoinAmount", GameManager.Instance.coinAmount).ToString();
        deathText.text = PlayerPrefs.GetInt("killingCount", GameManager.Instance.killingEnemies).ToString();
    }
}