using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioClip mainTrack, fireCrackling, fireIgnition;
    LevelController levelController;
    [SerializeField] float targetVolume;

    AudioSource audioSource;

    private void Start()
    {
        levelController = FindObjectOfType<LevelController>();
        audioSource = GetComponent<AudioSource>();

        StartMusic();
    }

    public void StartMusic()
    {
        audioSource.clip = mainTrack;
        audioSource.volume = 0.2f;
        audioSource.Play();
    }

    public void Fade()
    {
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut()
    {
        while (audioSource.volume > 0)
        {
            audioSource.volume -= audioSource.volume * Time.deltaTime / 1;
            yield return null;
        }
    }

    //Singleton
    private static AudioController _instance;
    public static AudioController Instance { get; }

    private void Awake()
    {
        CheckIfSingleton();
    }

    private void CheckIfSingleton()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
