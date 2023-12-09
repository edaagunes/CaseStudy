using Blended;
using UnityEngine;
using DG.Tweening;

public class PlayerAttackController : MonoBehaviour
{
    private Pool pool;
    public Transform spawnPoint;
    public bool didIThrow; // Flag to track if the boomerang was thrown
    private int splinePointCount = 6; // Number of points in the spline path for boomerang trajectory
    public GameObject boomerang;
    public float shotPower = 1f;


    private void Awake()
    {
        // Accessing the Pool instance for object spawning
        pool = Pool.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && !didIThrow)
        {
            didIThrow = true; // Set to true to prevent multiple throws at once
            
            // Spawn a boomerang object from the pool at the spawnPoint position
            boomerang = pool.SpawnObject(spawnPoint.position, PoolItemType.Boomerang, null);
            Vector3[] path = new Vector3[splinePointCount]; // Array to store the points of the boomerang's path

            // Enable trail renderer for the boomerang
            boomerang.GetComponent<TrailRenderer>().enabled = true;

            // Calculate the trajectory direction towards the enemy and set up the shot direction
            Vector3 targetDirection = (other.gameObject.transform.position - boomerang.transform.position).normalized;
            targetDirection += Vector3.up;
            Vector3 shotDirection = targetDirection + (targetDirection * 10);

            // Calculate the points for the boomerang's path using a spline-like curve
            for (int i = 0; i < splinePointCount; i++)
            {
                float t = i / (float)(splinePointCount - 1);
                Vector3 pointPosition = Vector3.Lerp(spawnPoint.position, other.gameObject.transform.position, t) +
                                        shotDirection * Mathf.Sin(t * Mathf.PI);
                path[i] = pointPosition;
            }

            // Animate the boomerang along the calculated path using DOTween
            boomerang.transform.DOPath(path, shotPower, PathType.CatmullRom).SetOptions(true).SetLookAt(0.01f)
                .SetEase(Ease.Linear);
            Destroy(boomerang, 1f);
        }
    }

    private void Update()
    {
        // Reset the didIThrow flag if the boomerang object is null (destroyed)
        
        if (boomerang == null)
        {
            didIThrow = false;
        }
    }
}