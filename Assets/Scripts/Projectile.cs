using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 20f; // Amount of damage the projectile does
    public float lifetime = 5f; // Time in seconds before the projectile is destroyed
    public string shooterTag; // Tag of the GameObject that fired the projectile
    public float rotationSpeed = 360f; // Rotation speed of the projectile
    public AudioClip projectileSound; // Sound effect for the projectile when instantiated
    public AudioClip impactSound; // Sound effect for when the projectile hits something

    private AudioSource audioSource; // AudioSource component for playing sounds
    private bool hasHit = false; // Flag to check if the projectile has hit something

    private void Start()
    {
        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;

        // Play the projectile sound
        if (projectileSound != null)
        {
            audioSource.PlayOneShot(projectileSound);
        }
        else
        {
            Debug.LogWarning("Projectile sound is not assigned.");
        }

        // Destroy the projectile after its lifetime
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Rotate the projectile over time
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
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
            if (!hasHit)
            {
                hasHit = true;

                // Get the Health component of the collided object
                Health health = other.GetComponent<Health>();

                if (health != null)
                {
                    // Apply damage
                    health.TakeDamage(damage);
                }

                // Play impact sound
                if (impactSound != null)
                {
                    audioSource.PlayOneShot(impactSound);

                    // Delay destruction to allow the sound to play
                    Destroy(gameObject, impactSound.length);
                }
                else
                {
                    Debug.LogWarning("Impact sound is not assigned.");

                    // Destroy immediately if no impact sound
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            // Optionally handle collisions with other objects here
            // Destroy the projectile immediately if it hits something else
            Destroy(gameObject);
        }
    }
}
