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
            Debug.LogError("rankTextがセットされていません！");
            return;
        }
        int score = ScoreManager.Instance.Score;
        Debug.Log($"現在のスコア: {score}");
        string rank = GetRank(score);
        Debug.Log($"ランク判定結果: {rank}");
        rankText.text = $"ランク: {rank}";

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
