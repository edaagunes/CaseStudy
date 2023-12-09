using System.Collections;
using Blended;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform player;
    public float minDistance;
    public float maxDistance;

    private bool shouldEnemySpawn;

    private Pool pool;

    private void Start()
    {
        pool = Pool.Instance;

        shouldEnemySpawn = true;
        StartCoroutine(SpawnEnemy(1));
    }

    private IEnumerator SpawnEnemy(float spawnTimer)
    {
        while (shouldEnemySpawn)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            pool.SpawnObject(spawnPosition, PoolItemType.Enemy, null);
            yield return new WaitForSeconds(spawnTimer);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float randomDistance = Random.Range(minDistance, maxDistance);
        Vector3 spawnPosition = new Vector3(player.position.x + randomDistance, 0, player.position.z + randomDistance);
        return spawnPosition;
    }
}