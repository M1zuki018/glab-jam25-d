using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultButton : MonoBehaviour
{
    public string resultSceneName = "Result";  // リザルトシーン名

    public void OnResultButtonClicked()
    {
        // シーン内のすべての Judge を取得
        Judge[] judges = FindObjectsOfType<Judge>();

        if (judges.Length == 0)
        {
            Debug.LogError("Judgeがシーン内に存在しません！");
            return;
        }

        // 全部の傷（Judge）を判定
        foreach (Judge judge in judges)
        {
            judge.EvaluateAll();
        }

        Debug.Log($"合計スコア: {ScoreManager.Instance.Score}");

        // 最後にリザルトシーンへ移動
        SceneManager.LoadScene(resultSceneName);
    }
}
