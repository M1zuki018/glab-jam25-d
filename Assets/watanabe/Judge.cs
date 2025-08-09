using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Judge : MonoBehaviour
{
    [Header("判定する傷オブジェクト")]
    public List<GameObject> wounds;  // Inspectorで傷オブジェクトを登録

    [Header("判定に使うシップのタグ名")]
    public string shipTag = "Ship";

    [Header("正解判定に必要な重なり割合 (0~1)")]
    [Range(0f, 1f)]
    public float correctThreshold = 0.5f;

    [Header("正解した傷1つあたりのスコア")]
    public int scorePerCorrect = 1;

    private int totalScore;

    /// <summary>
    /// リザルトボタン押下時に一斉判定するメソッド
    /// </summary>
    public void EvaluateAll()
    {
        totalScore = 0;

        foreach (var wound in wounds)
        {
            Collider2D woundCol = wound.GetComponent<Collider2D>();
            if (woundCol == null)
            {
                Debug.LogWarning($"傷にCollider2Dが付いていません: {wound.name}");
                continue;
            }

            bool isCorrect = false;

            // 重なっているColliderを取得
            Collider2D[] overlapped = Physics2D.OverlapBoxAll(woundCol.bounds.center, woundCol.bounds.size, 0f);

            foreach (var col in overlapped)
            {
                if (col.CompareTag(shipTag))
                {
                    float overlapArea = CalculateOverlapArea(woundCol, col);
                    float woundArea = woundCol.bounds.size.x * woundCol.bounds.size.y;
                    float colArea = col.bounds.size.x * col.bounds.size.y;

                    // 傷がどのくらいおおわれているかで判定
                    float ratio = overlapArea / woundArea;

                    if (ratio >= correctThreshold)
                    {
                        isCorrect = true;
                        Debug.Log($"正解！ {wound.name} はシップ {col.gameObject.name} によってカバーされました (重なり割合: {ratio:F2})");
                        break; // この傷は正解なので次の傷へ
                    }
                }
            }

            if (isCorrect)
            {
                totalScore += scorePerCorrect;
            }
            else
            {
                Debug.Log($"不正解: {wound.name} は十分にカバーされていません");
            }
        }

        ScoreManager.Instance.AddScore(totalScore);
        Debug.Log($"判定終了。スコア加算: {totalScore}");
    }

    
    /// ２つのCollider2Dの重なり面積を簡易計算
   
    private float CalculateOverlapArea(Collider2D a, Collider2D b)
    {
        Bounds aBounds = a.bounds;
        Bounds bBounds = b.bounds;

        float xMin = Mathf.Max(aBounds.min.x, bBounds.min.x);
        float xMax = Mathf.Min(aBounds.max.x, bBounds.max.x);
        float yMin = Mathf.Max(aBounds.min.y, bBounds.min.y);
        float yMax = Mathf.Min(aBounds.max.y, bBounds.max.y);

        float width = xMax - xMin;
        float height = yMax - yMin;

        if (width <= 0 || height <= 0)
            return 0f;

        return width * height;
    }
}