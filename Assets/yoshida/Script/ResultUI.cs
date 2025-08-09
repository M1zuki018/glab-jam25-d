using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ResultUI12 : MonoBehaviour
{
    public Text rankText;
    public ResultManager endingManager;

    void Start()
    {
        if (rankText == null)
        {
            Debug.LogError("rankText���Z�b�g����Ă��܂���I");
            return;
        }
        int score = ScoreManager.Instance.Score;
        Debug.Log($"���݂̃X�R�A: {score}");
        string rank = GetRank(score);
        Debug.Log($"�����N���茋��: {rank}");
        rankText.text = $"�����N: {rank}";

        endingManager.ShowRating(rank);
    }

    string GetRank(int score)
    {
        switch (score)
        {
            case 3:
                return "A";
            case 2:
                return "B";
            case 1:
                return "C";
            default:
                return "D";  
        }
    }
}
