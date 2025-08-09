using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class a : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    void Update()
    {
        if (ScoreManager.Instance != null)
        {
            scoreText.text = "ÉXÉRÉA: " + ScoreManager.Instance.Score;
        }
    }
}
