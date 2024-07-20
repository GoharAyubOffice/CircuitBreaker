using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // The player’s transform to follow
    public float smoothSpeed = 0.125f; // Speed at which the camera will follow
    public Vector3 offset; // Offset from the player's position to adjust camera positioning

    private void LateUpdate()
    {
        if (player == null) return;

        // Desired position is the player’s position plus the offset
        Vector3 desiredPosition = player.position + offset;

        // Smoothly interpolate between the camera’s current position and the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update camera position
        transform.position = smoothedPosition;

        // Optionally, keep the camera's Z position fixed if using 2D
        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }
}
