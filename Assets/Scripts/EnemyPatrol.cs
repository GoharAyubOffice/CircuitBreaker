using UnityEngine;
using System.Collections;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 2f; // Movement speed of the enemy
    public float waitTime = 2f; // Time to wait at each waypoint
    public Transform[] waypoints; // Array of waypoints

    private int currentWaypointIndex;
    private bool waiting;

    private void Start()
    {
        if (waypoints.Length > 0)
        {
            transform.position = waypoints[0].position; // Start at the first waypoint
            currentWaypointIndex = 0;
            waiting = false;
            StartCoroutine(MoveToNextWaypoint());
        }
    }

    private IEnumerator MoveToNextWaypoint()
    {
        while (true)
        {
            if (!waiting)
            {
                Transform targetWaypoint = waypoints[currentWaypointIndex];
                Vector3 direction = (targetWaypoint.position - transform.position).normalized;
                float distance = Vector3.Distance(transform.position, targetWaypoint.position);

                while (distance > 0.1f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);
                    distance = Vector3.Distance(transform.position, targetWaypoint.position);
                    yield return null;
                }

                transform.position = targetWaypoint.position;
                waiting = true;
                yield return new WaitForSeconds(waitTime);
                waiting = false;

                currentWaypointIndex = Random.Range(0, waypoints.Length);
            }
            yield return null;
        }
    }
}
