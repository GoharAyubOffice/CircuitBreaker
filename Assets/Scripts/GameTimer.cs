using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
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
    public CameraFollow cameraFollowScript; // Reference to the camera follow script (disable during end sequence)

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
        timerText.text = string.Format("Explosion Timer: {0:00}:{1:00}", minutes, seconds);
    }

    void TriggerEndSequence()
    {
        isEndSequenceTriggered = true;
        StartCoroutine(MoveCameraAndExplode());
    }

    IEnumerator MoveCameraAndExplode()
    {
        // Disable the camera follow script to stop camera movement
        if (cameraFollowScript != null)
        {
            cameraFollowScript.enabled = false;
        }

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

        // Ensure the camera stays at the hostages' position
        mainCamera.transform.position = targetPosition;

        // Wait for a moment to emphasize the explosion
        yield return new WaitForSeconds(1f);

        // Trigger explosion and disable hostages
        GameObject explosion = Instantiate(explosionPrefab, hostages.transform.position, Quaternion.identity);

        // Play the explosion sound
        AudioSource explosionAudio = explosion.GetComponent<AudioSource>();
        if (explosionAudio != null)
        {
            explosionAudio.Play();
            // Optionally, wait until the sound has finished before deactivating the explosion
            yield return new WaitForSeconds(explosionAudio.clip.length);
        }

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
