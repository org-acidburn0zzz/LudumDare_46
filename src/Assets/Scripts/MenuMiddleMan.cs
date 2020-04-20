using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMiddleMan : MonoBehaviour
{
    LevelController levelController;

    private void Start()
    {
        levelController = FindObjectOfType<LevelController>();
    }

    public void StartTutorial()
    {
        levelController.LoadTutorialLevel();
    }

    public void ExitGame()
    {
        levelController.ExitGame();
    }
}
