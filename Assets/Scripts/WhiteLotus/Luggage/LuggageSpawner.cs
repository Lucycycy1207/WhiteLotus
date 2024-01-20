using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuggageSpawner : MonoBehaviour
{
    [SerializeField] GameObject luggagePrefab;
    [SerializeField] private float spawnRadius = 5f;
    [SerializeField] private Vector3 spawnCenter;


    private void Start()
    {

    }

    public void SpawnLuggage()
    {
        Vector3 spawnPosition = GetRandomSpawnPositionInCircle();
        Instantiate(luggagePrefab, spawnPosition, Quaternion.identity);
    }

    private Vector3 GetRandomSpawnPositionInCircle()
    {
        // Generate a random angle
        float randomAngle = Random.Range(0f, 360f);

        // Convert angle to radians
        float angleInRadians = Mathf.Deg2Rad * randomAngle;

        // Calculate position within the circle using polar coordinates
        float randomX = spawnCenter.x + spawnRadius * Mathf.Cos(angleInRadians);
        float randomZ = spawnCenter.z + spawnRadius * Mathf.Sin(angleInRadians);

        return new Vector3(randomX, luggagePrefab.transform.position.y, randomZ);
    }
}
