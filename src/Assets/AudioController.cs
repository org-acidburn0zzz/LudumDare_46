using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioClip mainTrack, fireCrackling, fireIgnition;
    LevelController levelController;

    private void Start()
    {
        levelController = FindObjectOfType<LevelController>();
    }

    public IEnumerator FadeOut(float fadeOutTime, AudioSource audioSource)
    {
        while (audioSource.volume > 0)
        {
            audioSource.volume -= audioSource.volume * Time.deltaTime / fadeOutTime;
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
