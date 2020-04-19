using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudioController : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Fade()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        while (audioSource.volume > 0)
        {
            audioSource.volume -= audioSource.volume * Time.deltaTime / 1;
            yield return null;
        }
    }
}
