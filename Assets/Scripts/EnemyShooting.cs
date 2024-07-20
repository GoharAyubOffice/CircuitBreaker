using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject projectilePrefab; // The projectile prefab that the enemy will shoot
    public Transform firePoint; // The point from where the projectile will be fired
    public float shootInterval = 2f; // Interval between shots
    public float stopDistance = 3f; // Distance at which the enemy will stop and shoot
    public float projectileLifetime = 5f; // Time in seconds before the projectile is destroyed

    private Transform player; // Reference to the player's transform
    private float nextShootTime = 0f; // Time when the enemy can shoot next
    private bool isShooting = false; // To track if the enemy is currently shooting

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogWarning("Player not found! Ensure the player GameObject is tagged correctly.");
        }
    }

    void Update()
    {
        if (player == null) return;

        // Calculate distance to the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= stopDistance)
        {
            // Stop movement and shoot if it's time to shoot
            if (!isShooting)
            {
                isShooting = true;
                StopAndShoot();
            }
        }
        else
        {
            // Move towards the player if not within stop distance
            isShooting = false;
            MoveTowardsPlayer();
        }
    }

    public void StopAndShoot()
    {
        // Check if it's time to shoot
        if (Time.time >= nextShootTime)
        {
            Shoot();
            nextShootTime = Time.time + shootInterval;
        }
    }

    void MoveTowardsPlayer()
    {
        // Implement movement towards the player if needed
        // You can call the movement logic here
    }

    public void Shoot() 
    {
        if (projectilePrefab == null || firePoint == null)
        {
            Debug.LogWarning("Projectile prefab or fire point not assigned!");
            return;
        }

        // Instantiate and shoot the projectile
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Vector2 direction = (player.position - firePoint.position).normalized;
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * 10f; // Adjust speed as necessary
        }

        // Destroy the projectile after a certain time
        Destroy(projectile, projectileLifetime);
    }
}
