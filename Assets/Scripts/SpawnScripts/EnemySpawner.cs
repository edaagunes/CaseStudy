using System.Collections;
using Blended;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform player;
    public float minDistance; // Min distance for enemy spawn
    public float maxDistance; // Max distance for enemy spawn

    private bool shouldEnemySpawn; // Flag indicating if enemies should spawn

    private Pool pool; // Pool instance

    private void Start()
    {
        pool = Pool.Instance;

        shouldEnemySpawn = true;
        StartCoroutine(SpawnEnemy(1)); // Start coroutine to spawn enemies with a delay of 1 second
    }

    private IEnumerator SpawnEnemy(float spawnTimer)
    {
        while (shouldEnemySpawn)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            pool.SpawnObject(spawnPosition, PoolItemType.Enemy, null); // Spawn an enemy from the pool
            yield return new WaitForSeconds(spawnTimer); // Wait for the specified spawn timer before spawning the next enemy
        }
    }

    // Get a random spawn position
    private Vector3 GetRandomSpawnPosition()
    {
        // Calculate a random distance within the specified range
        float randomDistance = Random.Range(minDistance, maxDistance);
        
        // Create a spawn position based on the player's position and the random distance
        Vector3 spawnPosition = new Vector3(player.position.x + randomDistance, 0, player.position.z + randomDistance);
        
        return spawnPosition;
    }
}