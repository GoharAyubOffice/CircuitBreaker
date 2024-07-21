using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float detectionRange = 5f;
    public float stopDistance = 3f;
    public Transform player;
    public Transform[] waypoints; // Array of waypoints for patrolling
    public float waitTime = 2f; // Time to wait at each waypoint

    private EnemyShooting enemyShooting;
    private int currentWaypointIndex;
    private bool waiting;
    private bool isPatrolling;

    void Start()
    {
        enemyShooting = GetComponent<EnemyShooting>();
        if (enemyShooting == null)
        {
            Debug.LogWarning("EnemyShooting script not found on the enemy!");
        }

        if (waypoints.Length > 0)
        {
            transform.position = waypoints[0].position; // Start at the first waypoint
            currentWaypointIndex = 0;
            waiting = false;
            isPatrolling = true;
            StartCoroutine(Patrol());
        }
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            isPatrolling = false;
            StopCoroutine(Patrol()); // Stop patrolling when player is detected

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
        else
        {
            // Resume patrolling if player is out of range
            if (!isPatrolling)
            {
                isPatrolling = true;
                StartCoroutine(Patrol());
            }
        }
    }

    void MoveTowardsPlayer()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    private IEnumerator Patrol()
    {
        while (isPatrolling)
        {
            if (!waiting && waypoints.Length > 0)
            {
                Transform targetWaypoint = waypoints[currentWaypointIndex];
                Vector3 direction = (targetWaypoint.position - transform.position).normalized;
                float distance = Vector3.Distance(transform.position, targetWaypoint.position);

                while (distance > 0.1f && isPatrolling)
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, moveSpeed * Time.deltaTime);
                    distance = Vector3.Distance(transform.position, targetWaypoint.position);
                    yield return null;
                }

                if (isPatrolling)
                {
                    transform.position = targetWaypoint.position;
                    waiting = true;
                    yield return new WaitForSeconds(waitTime);
                    waiting = false;

                    currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                }
            }
            yield return null;
        }
    }
}
