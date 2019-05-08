using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public Stat lifeCoins; // Coins/Health
    public Stat damage; // Bullet damage
    public Stat power; // Bullet speed
    public Stat fireRate; // Fire rate
    public Stat speed; // Player move speed

    [Header("References")]
    public Sprite[] playerSprites = new Sprite[8]; // Array of player sprites with eyes pointing in different directions
    public Transform shootPoint; // Position where bullets spawn when shooting
    public GameObject bulletPrefab; // The bullet object
    public GameObject playerDeathEffect; // The effect spawned when the player dies
    public AudioSource coinPickup; // Sound played when player collects a coin
    public AudioSource shoot; // Sound played when shooting a bullet

    private float cooldownTimer = 0; // Shot cooldown timer
    private Rigidbody2D body; // Player's rigidbody
    private SpriteRenderer spriteRenderer; // Player's sprite renderer

    private void Start()
    {
        // Get references
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Update aim
        Aim();

        // If out of coins
        if (lifeCoins.Value <= 0)
        {
            // Player dies
            Die();
        }

        // If shoot button was pressed and timer has elapsed
        if (Input.GetButton("Fire1") && cooldownTimer <= 0)
        {
            // Shoot 
            Shoot();

            // Reset timer
            cooldownTimer = 10f / (float)fireRate.Value;
        }
        else if (cooldownTimer > 0)
        {
            // Advance timer
            cooldownTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        // Move player by changing rigidbody velocity
        body.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * (speed.Value / 5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If player collided with a coin
        if (collision.gameObject.tag == "Coin")
        {
            // Destroy the coin
            Destroy(collision.gameObject);

            // Increase number of coins
            lifeCoins.Value++;

            // Play coin pickup sound
            coinPickup.Play();
        }
    }

    private void Aim()
    {
        // Get the position of the mouse relitive to the player
        Vector3 lookPoint = Reference.Instance.mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        // Get the angle from the player to the mouse
        Vector3 rotation = new Vector3(0, 0, Mathf.Atan2(lookPoint.y, lookPoint.x) * Mathf.Rad2Deg);

        // Rotate the shoot point (the "gun")
        shootPoint.rotation = Quaternion.Euler(rotation);

        // Change sprite so the player's eyes look in the direction of the mouse
        spriteRenderer.sprite = playerSprites[Mathf.RoundToInt((rotation.z + 180f) / 45f) % 8];
    }

    private void Shoot()
    {
        // Take a coin away
        lifeCoins.Value -= 1;

        // Create a bullet
        Instantiate(bulletPrefab, shootPoint.position + shootPoint.right, shootPoint.rotation);

        // Play shoot sound
        shoot.Play();
    }

    private void Die()
    {
        // Spawn death effect
        Instantiate(playerDeathEffect, transform.position, Quaternion.identity);

        // Initiate game over
        Reference.Instance.masterScript.GameOver();

        // Disable the player
        gameObject.SetActive(false);
    }
}
