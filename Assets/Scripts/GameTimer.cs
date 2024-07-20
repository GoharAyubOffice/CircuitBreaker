using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Reference to the TextMeshProUGUI element
    public float timeLimit = 300f; // Time limit in seconds (e.g., 5 minutes)
    private float timeRemaining;
    private bool timerActive = true;

    private void Start()
    {
        timeRemaining = timeLimit;
        UpdateTimerDisplay();
    }

    private void Update()
    {
        if (timerActive)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                TimerEnded();
            }
            UpdateTimerDisplay();
        }
    }

    private void UpdateTimerDisplay()
    {
        // Format the time as minutes:seconds
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = $"Explosion Time: {minutes:00}:{seconds:00}";
    }

    private void TimerEnded()
    {
        timerActive = false;
        timerText.text = "Time's Up!"; // Optionally update the text when time ends
        // Optionally trigger game over or end scenario here
    }

    // Call this method to reset the timer if needed
    public void ResetTimer(float newTimeLimit)
    {
        timeLimit = newTimeLimit;
        timeRemaining = timeLimit;
        timerActive = true;
        UpdateTimerDisplay();
    }
}
