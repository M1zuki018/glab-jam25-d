using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class SceneChangeManager : MonoBehaviour
{
    public static SceneChangeManager Instance { get; private set; }

    public GameObject soundManagerPrefab;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (SoundManager.Instance == null)
        {
            Instantiate(soundManagerPrefab);
        }
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("InGame");
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene("TitleMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}