using System.Collections.Generic;
using Blended;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinSpawner : MonoBehaviour
{
    private Pool pool; // Pool instance
    private GameManager gameManager;
    private Transform playerTransform;
    public float randomX; // Range for random X position
    public float randomZ; // Range for random Z position
    
    private List<GameObject> coinList = new List<GameObject>(); // List to store spawned coins
 //   private List<GameObject> heartList = new List<GameObject>();

    private void Awake()
    {
        pool = Pool.Instance;
        gameManager = GameManager.Instance;
    }

    private void Start()
    {
        playerTransform = gameManager.player;
    }
    
    private void SpawnCoin(int coins)
    {
        for (int i = 0; i < coins; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            var coin = pool.SpawnObject(spawnPosition, PoolItemType.Coin, null); // Spawn a coin from the pool
            coinList.Add(coin);  // Add the spawned coin to the list 
        }
    }

    // Get a random spawn position
    private Vector3 GetRandomSpawnPosition()
    {
        randomX = Random.Range(-150, 150);
        randomZ = Random.Range(-150, 150);
        Vector3 spawnPosition =
            new Vector3(playerTransform.position.x + randomX, 2, playerTransform.position.z + randomZ);
        return spawnPosition;
    }

    private void Update()
    {
        // If the number of coins in the scene is less than 100, continuously spawn 300 coins
        if (coinList.Count < 100)
        {
            SpawnCoin(300);
        }
        
    }
}