using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public GameObject uiPanel; // UI Panel to input the code
    public TMP_InputField codeInputField; // Input field for code entry
    public Button submitButton; // Button to submit code
    public string correctCode; // Unique code for each door
    public AudioClip openSound; // Sound when the door is opened
    private AudioSource audioSource; // AudioSource component for playing sounds

    private bool isPlayerNear = false; // To track player proximity

    void Start()
    {
        // Ensure UI is hidden at the start
        uiPanel.SetActive(false);

        // Set up button click listener
        submitButton.onClick.AddListener(CheckCode);

        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        // Handle UI visibility based on player proximity
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E)) // Assuming 'E' is the key to interact
        {
            uiPanel.SetActive(true);
            Time.timeScale = 0; // Pause game
            audioSource.PlayOneShot(openSound);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            HideUIPanel(); // Hide the UI and resume game
        }
    }

    void CheckCode()
    {
        if (codeInputField.text == correctCode)
        {
            OpenDoor();
        }
        else
        {
            Debug.Log("Incorrect Code");
            HideUIPanel(); // Hide the UI and resume game
        }
    }

    void OpenDoor()
    {
        // Implement door opening logic
        gameObject.SetActive(false); // Example: Hide the door
        HideUIPanel(); // Hide the UI
    }

    void HideUIPanel()
    {
        uiPanel.SetActive(false);
        Time.timeScale = 1; // Resume game
    }
}
