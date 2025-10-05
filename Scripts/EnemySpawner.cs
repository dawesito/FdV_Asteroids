// Este archivo no es usado.

using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float spawnRatePerMinute = 30f;
    public float spawnRateIncrement = 1f;
    public float xLimit = 8;
    public float maxLifeTime = 5f;

    private float spawnNext = 0;

    void Update()
    {
        if (Time.time > spawnNext)
        {
            spawnNext = Time.time + 60 / spawnRatePerMinute;
            spawnRatePerMinute += spawnRateIncrement;
            float random = Random.Range(-xLimit, xLimit);

            Vector2 spawnPosition = new Vector2(random, 8f);
            GameObject Meteor = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
        }


    }
}
