using Blended;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour
{
    private GameManager gameManager;
    private Pool pool;

    public ParticleSystem enemyBlood;

    public Transform throwSpawnPosition;
    public Transform playerTransform;
    
    private NavMeshAgent navMesh;
    private Animator enemyAnimator;
    private bool isEnemyDeath;

    // Enemy actions
    private bool isThrowing;
    private static readonly int Throwing = Animator.StringToHash("Throw");
    private static readonly int Death = Animator.StringToHash("Death");


    private void Awake()
    {
        // Initialize
        gameManager = GameManager.Instance;
        pool = Pool.Instance;
    }

    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        navMesh = GetComponent<NavMeshAgent>();
        playerTransform = gameManager.player;
    }

    void Update()
    {
        if (!isThrowing)
        {
            navMesh.SetDestination(playerTransform.position);
        }
    }

    public void Throw()
    {
        // Spawning a projectile (ball) from the pool at a specified position
        
        var ball = pool.SpawnObject(throwSpawnPosition.position, PoolItemType.Ball, null);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttackRadius"))
        {
            // Trigger throwing animation if the enemy is not dead
            if (!isEnemyDeath)
            {
                isThrowing = true;
                enemyAnimator.SetBool(Throwing, true);
            }
        }

        if (other.gameObject.CompareTag("Boomerang"))
        {
            // Play blood particle effect, trigger death animation, disable movement, update game stats,
            // and destroy the enemy object after some time
            
            enemyBlood.Play();
            enemyAnimator.SetTrigger(Death);
            enemyAnimator.SetBool(Throwing, false);
            navMesh.speed = 0;
            gameManager.killingEnemies++;
            PlayerPrefs.SetInt("killingCount", GameManager.Instance.killingEnemies);
            UIManager.Instance.deathText.text = GameManager.Instance.killingEnemies.ToString();
            GetComponent<Collider>().enabled = false;
            Destroy(gameObject, 3f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Reset throwing flag and animation parameter when exiting the player's attack radius
        isThrowing = false;
        enemyAnimator.SetBool(Throwing, false);
    }
    
}