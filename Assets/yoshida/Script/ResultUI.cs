using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI12 : MonoBehaviour
{
    private ReadyCheckHelper readyCheckHelper;

    public Text rankText;
    public SubResultManager endingManager;

    void Start()
    {

        //readyCheckHelper = FindObjectOfType<ReadyCheckHelper>();
        //if (readyCheckHelper == null)
        //{
        //    Debug.LogError("ReadyCheckHelper��������܂���I");
        //    return;
        //}

        if (rankText == null)
        {
            Debug.LogError("rankText���Z�b�g����Ă��܂���I");
            return;
        }
        int baseScore = ScoreManager.Instance.Score;


        //float clearTime = readyCheckHelper.CurrentTime;
        float clearTime = 10f;
        int timeScore = ConvertTimeToScore(clearTime);
        int score = baseScore * timeScore;
        Debug.Log($"���݂̃X�R�A: {baseScore}, �^�C��: {clearTime:F2}�b, �^�C���X�R�A: {timeScore}, ���v: {score}");
        
        string rank = GetRank(score);
        Debug.Log($"�����N���茋��: {rank}");
        rankText.text = $"�����N: {rank}";

        endingManager.ShowRating(rank);
    }

    int ConvertTimeToScore(float time)
    {
        if(time <= 20f)
        {
            return 2;
        }

        if(time <= 40f)
        {
            return 1;
        }


        else
        {
            return 0;
        }
    }

    string GetRank(int score)
    {
        switch (score)
        {
            case 4:
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
