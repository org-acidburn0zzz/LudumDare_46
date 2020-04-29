using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    int currentSceneIndex;
    const int menuIndex = 0;
    int gameOverIndex = 5;
    int latestLevelIndex;


    AudioSource audioSource;
    Animator anim;
    AudioController audioController;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        if (FindObjectOfType<AudioController>().GetComponent<AudioController>() != null)
        {
            audioController = FindObjectOfType<AudioController>().GetComponent<AudioController>();
        }
    }



    public void LoadTutorialLevel()
    {
        StartCoroutine(TutorialLevelLoader());
    }

    public void LoadFirstLevel()
    {
        StartCoroutine(FirstLevelLoader());
    }

    public void LoadNextScene()
    {
        StartCoroutine(NextSceneLoader());
    }

    IEnumerator NextSceneLoader()
    {
        anim.SetTrigger("fadeOut");
        yield return new WaitForSeconds(1);
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

    public void LoadGameOver()
    {
        SceneManager.LoadScene(gameOverIndex);
    }

    IEnumerator FirstLevelLoader()
    {
        anim.SetTrigger("fadeOut");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(2);
    }
    IEnumerator TutorialLevelLoader()
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

    public void SetLatestLevel()
    {
        latestLevelIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadLatestLevel()
    {
        SceneManager.LoadScene(latestLevelIndex);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    void DestroyOnReload()
    {
        if (FindObjectOfType<TutorialMusicController>() != null)
        {
            Destroy(FindObjectOfType<TutorialMusicController>());
        }
        if (FindObjectOfType<AudioController>() != null)
        {
            Destroy(FindObjectOfType<AudioController>());
        }
    }
}
