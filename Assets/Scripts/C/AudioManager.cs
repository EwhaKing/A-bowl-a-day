using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource sfxSource;

    public AudioClip successSound;
    public AudioClip failSound;
    public AudioClip clickSound;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void PlaySuccess()
    {
        sfxSource.PlayOneShot(successSound);
    }

    public void PlayFail()
    {
        sfxSource.PlayOneShot(failSound);
    }

    public void PlayClick()
    {
        sfxSource.PlayOneShot(clickSound);
    }
}