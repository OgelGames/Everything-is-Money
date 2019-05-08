using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int startNumberOfEnemies; // The number of enemies in the first wave
    public float startSpawnDelay; // The time between enemy spawns for the first wave
    public float spawnRadius; // How far away to spawn enemies
    public GameObject enemyPrefab; // The enemy object

    [HideInInspector] public int enemiesAlive = 0; // The number of enemies currently spawned

    private int numberOfEnemies; // The number of enemies
    private float spawnDelay; // The time between enemy spawns

    public void StartSpawing()
    {
        // Set the number of enemies
        numberOfEnemies = startNumberOfEnemies;

        // Set the spawn delay
        spawnDelay = startSpawnDelay;

        // Start spawning enemies
        StartCoroutine(SpawnEnemies());
    }

    public void NextWave()
    {
        // Increase the number of enemies
        numberOfEnemies += (int)(numberOfEnemies / 5f) + 1;

        // Decrease the spawn delay
        spawnDelay -= spawnDelay / 10f;

        // Start spawning enemies
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        // Create a wait object
        WaitForSecondsRealtime wait = new WaitForSecondsRealtime(spawnDelay);

        // Spawn enemies
        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Spawn a new enemy
            Instantiate(enemyPrefab, RandomCirclePosition(transform.position, spawnRadius), Quaternion.identity);

            // Increase the enemy count
            enemiesAlive++;

            // Wait for a bit
            yield return wait;
        }

        // Wait until all enemies are dead
        while (enemiesAlive > 0)
        {
            yield return null;
        }

        // Complete the wave
        Reference.Instance.masterScript.WaveComplete();
    }

    private Vector2 RandomCirclePosition(Vector2 center, float radius)
    {
        // Create a random angle
        float angle = Random.value * 360f;

        // Calculate the position where the angle points on the circle
        Vector2 position = new Vector2
        {
            x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad),
            y = center.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad)
        };

        // Return the random position
        return position;
    }
}
