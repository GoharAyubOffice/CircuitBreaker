using UnityEngine;
using TMPro; // Make sure to include TextMesh Pro namespace
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public TextMeshProUGUI codeText; // Reference to the TextMeshProUGUI element for displaying the code
    public TextMeshProUGUI messageText; // Reference to the TextMeshProUGUI element for displaying messages

    public float messageDisplayTime = 3f; // Time in seconds to display the message
    public float codeDisplayTime = 5f; // Time in seconds to display the door code

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowCode(string code)
    {
        codeText.text = "Door Code: " + code;
        messageText.text = ""; // Clear any previous messages
        StartCoroutine(ClearCodeAfterDelay());
    }

    public void ShowMessage(string message)
    {
        messageText.text = message;
        codeText.text = ""; // Clear the code text if a message is shown
        StartCoroutine(ClearMessageAfterDelay());
    }

    private IEnumerator ClearCodeAfterDelay()
    {
        yield return new WaitForSeconds(codeDisplayTime);
        codeText.text = ""; // Clear the code text after the delay
    }

    private IEnumerator ClearMessageAfterDelay()
    {
        yield return new WaitForSeconds(messageDisplayTime);
        messageText.text = ""; // Clear the message after the delay
    }
}
