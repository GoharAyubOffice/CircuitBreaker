using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 20f; // Amount of damage the projectile does
    public float lifetime = 5f; // Time in seconds before the projectile is destroyed
    public string shooterTag; // Tag of the GameObject that fired the projectile

    private void Start()
    {
        // Destroy the projectile after its lifetime
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ignore collision with the shooter
        if (other.CompareTag(shooterTag))
        {
            return;
        }

        // Check if the projectile hits the player or enemy
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            // Get the Health component of the collided object
            Health health = other.GetComponent<Health>();

            if (health != null)
            {
                // Apply damage
                health.TakeDamage(damage);
            }

            // Destroy the projectile on impact
            Destroy(gameObject);
        }
        else
        {
            // Optionally handle collisions with other objects here
            // Uncomment the next line if you want to destroy the projectile on hitting any object
            // Destroy(gameObject);
        }
    }
}
