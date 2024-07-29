using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        audioSource.Play();
    }

}
