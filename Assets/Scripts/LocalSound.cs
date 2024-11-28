using UnityEngine;

public class LocalSound : MonoBehaviour
{
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void LocalAudioPlayer(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
