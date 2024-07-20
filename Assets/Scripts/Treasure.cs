using UnityEngine;

public class Treasure : MonoBehaviour
{
    public string doorCode; // The code for the door associated with this treasure
    public bool isCorrectTreasure; // Set to true if this is the treasure with the code

    private bool isPlayerNearby = false;

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void Interact()
    {
        if (isCorrectTreasure)
        {
            // Show the code to the player
            UIManager.Instance.ShowCode(doorCode);
        }
        else
        {
            // Show a message indicating the treasure is empty
            UIManager.Instance.ShowMessage("This treasure has no useful code.");
        }

        // Optionally destroy or deactivate the treasure after interaction
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}
