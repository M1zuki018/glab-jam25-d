using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Globalization;
using Unity.VisualScripting;

public class GameFlowManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject readyUI;
    [SerializeField] private GameObject injuryManager;
    [SerializeField] private GameObject countdownCanvas;
    [SerializeField] private Slider timerVisual;
    [SerializeField] private TMP_Text timerText;

    [Header("Timer Settings")]
    [SerializeField] private float timeLimit = 60f;

    private bool isReady = false;
    private float currentTime;

    private void Start()
    {
        currentTime = timeLimit;
        injuryManager.SetActive(false);
        readyUI.SetActive(false);
        countdownCanvas.SetActive(false);
        timerVisual.value = 1f;
        timerText.text = currentTime.ToString("F1");
    }

    private void Update()
    {
        if (!isReady)
        {
            if (Input.GetMouseButtonDown(0) && readyUI.activeSelf)
            {
                GotoKarte();
            }
        }
        else
        {
            HandleTimer();
        }
    }

    private void StartGame()
    {
        readyUI.SetActive(false);
        injuryManager.SetActive(true);
        countdownCanvas?.SetActive(true);
        isReady = true;
    }

    private void GotoKarte()
    {
        SceneManager.LoadScene("Karte");
    }

    private void HandleTimer()
    {
        currentTime -= Time.deltaTime;
        currentTime = Mathf.Max(currentTime, 0f);

        timerText.text = currentTime.ToString("F1", CultureInfo.InvariantCulture);
        if (timerVisual != null)
            timerVisual.value = currentTime / timeLimit;

        if (currentTime <= 0f)
            GameOver();
    }

    private void GameOver()
    {
        SceneManager.LoadScene("Result");
    }
}
