using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed; // The move speed of the enemy
    public GameObject pigDeathEffect; // The effect spawned when the enemy dies
    public GameObject coinPrefab; // The coin object

    private int coins = 0; // The total number of coins in this enemy
    private int health = 100; // The enemy's health
    private bool isTouchingPlayer = false; // Is the enemy touching the player
    private float coinTakeTimer = 1f; // The time between each coin taken from the player

    #region Events to log collisions with player

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isTouchingPlayer = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isTouchingPlayer = false;
        }
    }

    #endregion

    void Update()
    {
        // Move towards the player
        transform.position = Vector2.MoveTowards(transform.position, Reference.Instance.player.transform.position, speed * Time.deltaTime);

        // If out of health
        if (health <= 0)
        {
            // Enemy dies
            Die();
        }

        // If touching player and timer has elapsed
        if (isTouchingPlayer && coinTakeTimer <= 0)
        {
            // Take a coin from the player 
            Reference.Instance.player.lifeCoins.Value--;

            // Reset timer
            coinTakeTimer = 1f;
        }
        else if (coinTakeTimer > 0)
        {
            // Advance timer
            coinTakeTimer -= Time.deltaTime;
        }
    }

    private void DropCoins()
    {
        // Set a random number of coins to drop
        int coinsToDrop = Random.Range(1, coins + 3);

        // Drop the coins
        for (int i = 0; i < coinsToDrop; i++)
        {
            // Spawn a new coin
            GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);

            // Apply a small random force to the coin
            coin.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        }
    }

    private void Die()
    {
        // Spawn death effect
        Instantiate(pigDeathEffect, transform.position, Quaternion.identity);

        // Drop some coins
        DropCoins();

        // Decrease enemy count
        Reference.Instance.enemySpawner.enemiesAlive--;

        // Destroy self
        Destroy(gameObject);
    }

    public void TakeDamage(int amount)
    {
        // Decrease health
        health -= amount;

        // Increase number of coins in this enemy
        coins++;
    }
}
