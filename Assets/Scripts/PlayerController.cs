using UnityEngine;

public class TopDownPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
    }

    private void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // Create movement vector
        Vector2 movement = new Vector2(moveX, moveY).normalized * moveSpeed;

        // Move the player
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);

        // Handle sprite flipping based on movement direction
        HandleSpriteFlip(moveX, moveY);
    }

    private void HandleSpriteFlip(float moveX, float moveY)
    {
        // Only flip if there is movement
        if (moveX != 0 || moveY != 0)
        {
            if (Mathf.Abs(moveX) > Mathf.Abs(moveY))
            {
                // Horizontal movement
                spriteRenderer.flipX = moveX < 0; // Flip horizontally based on direction
            }
            else
            {
                // Vertical movement (optional, you can adjust this based on your needs)
                // Example: flip sprite vertically based on direction
                // You can set different sprites or animations for up/down if needed
                // For now, we will only flip horizontally
            }
        }
    }
}
