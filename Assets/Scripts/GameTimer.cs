using UnityEngine;
using TMPro; // Import TextMeshPro namespace
using UnityEngine.SceneManagement; // For reloading the scene
using System.Collections;

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 300f; // Example: 5 minutes
    public TextMeshProUGUI timerText; // Reference to the TextMeshProUGUI component
    public Transform hostagesPosition; // Reference to the hostages' position
    public Camera mainCamera; // Reference to the main camera
    public GameObject explosionPrefab; // Reference to the explosion prefab
    public GameObject hostages; // Reference to the hostages GameObject
    public GameObject missionFailedUI; // Reference to the Mission Failed UI GameObject

    private bool isTimerRunning = true;
    private bool isEndSequenceTriggered = false;

    void Update()
    {
        if (isTimerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerText();
            }
            else
            {
                if (!isEndSequenceTriggered)
                {
                    TriggerEndSequence();
                }
                isTimerRunning = false;
            }
        }
    }

    void UpdateTimerText()
    {
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void TriggerEndSequence()
    {
        isEndSequenceTriggered = true;
        StartCoroutine(MoveCameraAndExplode());
    }

    IEnumerator MoveCameraAndExplode()
    {
        // Smoothly move the camera to the hostages' position
        float duration = 2f; // Duration of the camera movement
        Vector3 startPosition = mainCamera.transform.position;
        Vector3 targetPosition = new Vector3(hostagesPosition.position.x, hostagesPosition.position.y, mainCamera.transform.position.z);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            mainCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Trigger explosion and disable hostages
        Instantiate(explosionPrefab, hostages.transform.position, Quaternion.identity);
        hostages.SetActive(false);

        // Display mission failed UI
        missionFailedUI.SetActive(true);
    }

    public void PlayAgain()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
