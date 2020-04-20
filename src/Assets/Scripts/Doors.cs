using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    TutorialMusicController tutorialMusic;
    AudioController gameMusic;
    Canvas canvas;
    bool doorEnabled = false;

    private void Start()
    {
        if (FindObjectOfType<TutorialMusicController>() != null)
        {
            tutorialMusic = FindObjectOfType<TutorialMusicController>().GetComponent<TutorialMusicController>();
        }
        if (FindObjectOfType<AudioController>() != null)
        {
            gameMusic = FindObjectOfType<AudioController>().GetComponent<AudioController>();
        }
        canvas = GetComponentInChildren<Canvas>();
        canvas.enabled = false;
    }

    private void Update()
    {
        if (doorEnabled && Input.GetKeyDown(KeyCode.E))
        {
            if (tutorialMusic != null)
            {
                tutorialMusic.Fade();
            }
            FindObjectOfType<LevelController>().GetComponent<LevelController>().LoadNextScene();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canvas.enabled = true;
            doorEnabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canvas.enabled = false;
            doorEnabled = false;
        }
    }

}
