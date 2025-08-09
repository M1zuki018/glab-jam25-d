//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;

//public class InGameTimer : MonoBehaviour
//{
//    private ReadyCheckHelper checkHelper;
//    [Header("Timer Details")]
//    [SerializeField] private Slider timerVisual;
//    [SerializeField] private TMP_Text timerText;
//    [SerializeField] private float timeLimit = 60f;

//    private void Awake()
//    {
//        checkHelper = FindAnyObjectByType<ReadyCheckHelper>();
//    }

//    private void Update()
//    {
//        if (checkHelper.isReady)
//        {
//            HandleTimer();
//        }
//    }
//    private void HandleTimer()
//    {
//        timeLimit -= Time.deltaTime;

//        timeLimit = Mathf.Max(timeLimit, 0);

//        timerText.text = timeLimit.ToString("F1");

//        timerVisual.value = timeLimit / 60f;

//        if (timeLimit <= 0)
//            GameOver();
//    }

//    private void GameOver()
//    {
//        SceneManager.LoadScene("Result");
//    }
//}
