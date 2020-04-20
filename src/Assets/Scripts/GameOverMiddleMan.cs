using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMiddleMan : MonoBehaviour
{
    LevelController levelController;

    private void Start()
    {
        levelController = FindObjectOfType<LevelController>();
    }

    public void RestartGame()
    {
        levelController.LoadFirstLevel();
    }

    public void ExitGame()
    {
        levelController.ExitGame();
    }

    public void RestartLevel()
    {
        levelController.LoadLatestLevel();
    }

    public void LoadMenu()
    {
        levelController.LoadMenu();
    }
}
