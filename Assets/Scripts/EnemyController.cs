using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float detectionRange = 5f;
    public Transform player;
    public float stopDistance = 3f;

    private EnemyShooting enemyShooting;

    void Start()
    {
        enemyShooting = GetComponent<EnemyShooting>();
        if (enemyShooting == null)
        {
            Debug.LogWarning("EnemyShooting script not found on the enemy!");
        }
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {

            if (distanceToPlayer <= stopDistance)
            {
                // Stop movement and shoot
                if (enemyShooting != null)
                {
                    enemyShooting.StopAndShoot();
                }
            }
            else
            {
                // Move towards the player
                MoveTowardsPlayer();
            }
        }
       
    }

    void MoveTowardsPlayer()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }
}
