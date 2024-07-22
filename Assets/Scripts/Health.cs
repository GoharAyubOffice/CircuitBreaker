using UnityEngine;
using TMPro; // Import TextMesh Pro namespace
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public TextMeshProUGUI playerHealthText; // Reference to the player's health UI text

    private bool isPlayer; // Flag to determine if this is the player's health

    void Start()
    {
        currentHealth = maxHealth;

        // Check if this is the player
        isPlayer = gameObject.CompareTag("Player");

        if (isPlayer && playerHealthText != null)
        {
            UpdateHealthUI(); // Initialize UI with the starting health
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (isPlayer && playerHealthText != null)
        {
            UpdateHealthUI(); // Update the UI when health changes
        }

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        if (isPlayer)
        {
            // Load the Game Over scene
            SceneManager.LoadScene(0); // Replace with the actual name of your Game Over scene
        }
        else
        {
            // You can add effects, animations, etc., here for non-player objects
            Destroy(gameObject);
        }
    }

    void UpdateHealthUI()
    {
        if (playerHealthText != null)
        {
            playerHealthText.text = $"Health: {currentHealth:0}";
        }
    }
}
