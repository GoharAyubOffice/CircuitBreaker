using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Hostages : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI youWinText;
    [SerializeField] private Button playAgain;
    [SerializeField] private GameObject youWin;

    private void Start()
    {
        youWin.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        // Check if the projectile hits the player or enemy
        if (other.CompareTag("Player"))
        {
            Debug.Log($"{gameObject.name} hit {other.gameObject.name}");
            youWin.gameObject.SetActive(true);
            
        }
    }
    public void PlayAgain()
    {
            SceneManager.LoadScene(0);
    }
}
