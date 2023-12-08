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

    private bool isThrowing;
    private static readonly int Throwing = Animator.StringToHash("Throw");


    private void Awake()
    {
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
        var ball = pool.SpawnObject(throwSpawnPosition.position, PoolItemType.Ball, null);
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttackRadius"))
        {
            if (!isEnemyDeath)
            {
                isThrowing = true;
                enemyAnimator.SetBool(Throwing, true);
            }
        }

        if (other.gameObject.CompareTag("Boomerang"))
        {
            enemyBlood.Play();
            enemyAnimator.SetTrigger("Death");
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
        isThrowing = false;
        enemyAnimator.SetBool(Throwing, false);
    }
    
}