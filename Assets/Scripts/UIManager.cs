using UnityEngine;
using TMPro; // Make sure to include TextMesh Pro namespace
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public TextMeshProUGUI codeText; // Reference to the TextMeshProUGUI element for displaying the code
    public TextMeshProUGUI messageText; // Reference to the TextMeshProUGUI element for displaying messages
    public Image backgroundCode;

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
    private void Start()
    {
        backgroundCode.gameObject.SetActive(false);
    }

    public void ShowCode(string code)
    {
        codeText.text = "Door Code: " + code;
        messageText.text = ""; // Clear any previous messages
        backgroundCode.gameObject.SetActive(true);

        StartCoroutine(ClearCodeAfterDelay());
    }

    public void ShowMessage(string message)
    {
        messageText.text = message;
        codeText.text = ""; // Clear the code text if a message is shown
        backgroundCode.gameObject.SetActive(true);
        StartCoroutine(ClearMessageAfterDelay());
    }

    private IEnumerator ClearCodeAfterDelay()
    {
        yield return new WaitForSeconds(codeDisplayTime);
        codeText.text = ""; // Clear the code text after the delay
        backgroundCode.gameObject.SetActive(false);

    }

    private IEnumerator ClearMessageAfterDelay()
    {
        yield return new WaitForSeconds(messageDisplayTime);
        messageText.text = ""; // Clear the message after the delay
        backgroundCode.gameObject.SetActive(false);

    }
}
