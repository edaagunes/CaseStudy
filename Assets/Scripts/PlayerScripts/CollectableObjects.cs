using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObjects : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Checking if the collider belongs to an object tagged as "Coin"
        if (other.gameObject.CompareTag("Coin"))
        {
            GameManager.Instance.coinAmount++;
            UIManager.Instance.coinText.text = GameManager.Instance.coinAmount.ToString();
            PlayerPrefs.SetInt("CoinAmount", GameManager.Instance.coinAmount);
            Destroy(other.gameObject);
        }
    }
}
