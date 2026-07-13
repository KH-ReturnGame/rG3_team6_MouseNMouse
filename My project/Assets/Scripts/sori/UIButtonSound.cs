using UnityEngine;

public class UIButtonSound : MonoBehaviour
{
    public static UIButtonSound Instance;

    public AudioSource audioSource;
    public AudioClip buttonClick;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayClick()
    {
        audioSource.PlayOneShot(buttonClick);
    }
}