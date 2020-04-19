using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    int currentSceneIndex;
    const int menuIndex = 0;
    const int gameOverIndex = 3;

    AudioSource audioSource;
    Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }



    public void LoadFirstLevel()
    {
        StartCoroutine(FirstLevelLoader());
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(GetCurrentSceneIndex() + 1);
    }

    public int GetCurrentSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator FirstLevelLoader()
    {
        anim.SetTrigger("fadeOut");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }


    //Singleton
    private static LevelController _instance;
    public static LevelController Instance { get; }

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
