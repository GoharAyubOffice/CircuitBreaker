using UnityEngine;
using TMPro; // Make sure to include TextMesh Pro namespace

public class Treasure : MonoBehaviour
{
    public string doorCode; // The code for the door associated with this treasure
    public bool isCorrectTreasure; // Set to true if this is the treasure with the code
    public AudioClip openSound; // Sound when the treasure is opened
    public TextMeshProUGUI messageText; // TextMesh Pro UI to display messages
    public float messageDisplayTime = 2f; // Time to display the message

    private bool isPlayerNearby = false;
    private AudioSource audioSource; // AudioSource component for playing sounds

    private void Start()
    {
        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void Interact()
    {
        // Play the treasure opening sound
        if (openSound != null)
        {
            audioSource.PlayOneShot(openSound);
        }

        // Check if this treasure has a code
        if (isCorrectTreasure)
        {
            // Show the code to the player
            UIManager.Instance.ShowCode(doorCode);
            ShowMessage("Code found!");
        }
        else
        {
            // Show a message indicating the treasure is empty
            UIManager.Instance.ShowMessage("This treasure has no useful code.");
            ShowMessage("No code available.");
        }

        // Optionally destroy or deactivate the treasure after interaction
        // Destroy(gameObject); // Uncomment if you want to destroy the treasure
    }

    private void ShowMessage(string message)
    {
        if (messageText != null)
        {
            messageText.text = message;
            Invoke("HideMessage", messageDisplayTime);
        }
    }

    private void HideMessage()
    {
        if (messageText != null)
        {
            messageText.text = "";
        }
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
