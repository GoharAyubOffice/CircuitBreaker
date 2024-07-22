using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip backgroundMusic; // The background music clip
    public float musicVolume = 0.5f; // Volume of the background music

    private AudioSource audioSource;

    void Start()
    {
        // Ensure there is an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Configure the AudioSource component
        audioSource.loop = true; // Loop the music
        audioSource.clip = backgroundMusic; // Set the music clip
        audioSource.volume = musicVolume; // Set the volume

        // Play the background music
        audioSource.Play();
    }
}
