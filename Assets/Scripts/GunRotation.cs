using UnityEngine;

public class GunRotation : MonoBehaviour
{
    public Transform gunTransform; // The transform of the gun (can be the child of the player or the player itself)
    public Transform firePoint; // The point from where the projectile will be fired

    void Update()
    {
        RotateGun();
    }

    void RotateGun()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure we work in 2D

        Vector2 direction = (mousePosition - gunTransform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        gunTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
