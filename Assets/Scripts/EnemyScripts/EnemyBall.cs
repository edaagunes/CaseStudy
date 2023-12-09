using UnityEngine;
using DG.Tweening;


public class EnemyBall : MonoBehaviour
{
    public float moveSpeed;
    private Transform playerTransform;
    private Vector3 direction;

    private void Start()
    {
        // Getting the player's transform reference using GameManager's singleton
        playerTransform = GameManager.Instance.player.transform;
        
        // Calculating the direction vector from the enemy to the player
        direction = playerTransform.position - transform.position;
        
        // Adding an upward offset to the direction vector
        direction += Vector3.up;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance.playerHealth > 20)
            {
                GameManager.Instance.playerHealth -= 20;
                
                // Tweening the health bar fill amount using DOTween
                DOTween.To(() => UIManager.Instance.healtBar.fillAmount,
                    x => UIManager.Instance.healtBar.fillAmount = x,
                    UIManager.Instance.healtBar.fillAmount - 0.2f, 0.25f);
                Destroy(gameObject);
            }
            else
            {
                // Tweening the health bar fill amount to zero
                DOTween.To(() => UIManager.Instance.healtBar.fillAmount,
                    x => UIManager.Instance.healtBar.fillAmount = x,
                    0, 0.25f).SetUpdate(true);
                
                // Activating the level fail UI, triggering player's death animation, and pausing the game
                UIManager.Instance.levelFailUI.SetActive(true);
                PlayerController.Instance.playerAnimator.SetTrigger("Death");
                Time.timeScale = 0;
            }
        }
    }

    private void Update()
    {
        // Move the enemy in the calculated direction at a specified speed
        
        transform.Translate(direction * (Time.deltaTime * moveSpeed));
    }
}