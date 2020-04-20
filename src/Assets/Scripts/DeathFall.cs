using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFall : MonoBehaviour
{
    LevelController levelController;
    AudioController ac;

    private void Start()
    {
        levelController = FindObjectOfType<LevelController>().GetComponent<LevelController>();
        ac = FindObjectOfType<AudioController>().GetComponent<AudioController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            levelController.SetLatestLevel();
            ac.Fade();
            levelController.LoadGameOver();
        }
    }
}
