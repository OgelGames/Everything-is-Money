using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect; // The effect spawned when the bullet hits something

    void Start()
    {
        // Apply shoot force
        GetComponent<Rigidbody2D>().velocity = transform.right * Reference.Instance.player.power.Value / 5f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check that the collision was not with the player
        if (collision.gameObject.tag != "Player")
        {
            // If bullet hit an enemy
            if (collision.gameObject.tag == "Enemy")
            {
                // Damage enemy
                collision.gameObject.GetComponent<Enemy>().TakeDamage(Reference.Instance.player.damage.Value);
            }

            // Spawn bullet hit effect
            Instantiate(hitEffect, transform.position, Quaternion.identity);

            // Shake the camera
            Reference.Instance.cameraShaker.Shake();

            // Destroy self
            Destroy(gameObject);
        }
    }
}
