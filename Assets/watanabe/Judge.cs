using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Judge : MonoBehaviour
{
    public Text correntText;  //正解用テキスト
    public Text wrongText;　　　//不正解用テキスト
    public float correctThreshold = 0.5f; //判定
    private Collider2D woundCollider;  //傷口の当たり判定コライダー


    private void Awake()
    {
        woundCollider = GetComponent<Collider2D>();
        if (woundCollider == null)
        {
            Debug.LogError($"JudgeをアタッチしているオブジェクトにCollider2Dがありません！:{gameObject.name}");
        }
        correntText.gameObject.SetActive( false );
        wrongText.gameObject.SetActive( false );
    }
    public void OnShipPlaced(GameObject ship)
    {
        if (IsCorrectPlacement(ship))
        {
            correntText.gameObject.SetActive(true);
            wrongText.gameObject.SetActive(false);
            ScoreManager.Instance.AddScore(1);
        }
        else
        {
            correntText.gameObject.SetActive(false);
            wrongText.gameObject.SetActive(true);
        }

    }

    public bool IsCorrectPlacement(GameObject ship)
    {
      if(woundCollider  == null)
        {
            return false;
        }

      Collider2D shipCollider = ship.GetComponent<Collider2D>();
      if (shipCollider == null)
        {
            Debug.LogError($"渡されたshipにCollider2Dがありません！:{ship.name}");
            return false;
        }

        Bounds shipBounds = shipCollider.bounds;
        Bounds woundBounds = woundCollider.bounds;

        if (!shipBounds.Intersects(woundBounds))
        {
            // 重なっていないなら不正解
            return false;
        }

        // 重なり矩形を計算
        float xMin = Mathf.Max(shipBounds.min.x, woundBounds.min.x);
        float xMax = Mathf.Min(shipBounds.max.x, woundBounds.max.x);
        float yMin = Mathf.Max(shipBounds.min.y, woundBounds.min.y);
        float yMax = Mathf.Min(shipBounds.max.y, woundBounds.max.y);

        float overlapWidth = xMax - xMin;
        float overlapHeight = yMax - yMin;

        if (overlapWidth <= 0 || overlapHeight <= 0)
            return false;

        float overlapArea = overlapWidth * overlapHeight;
        float shipArea = shipBounds.size.x * shipBounds.size.y;

        float overlapRatio = overlapArea / shipArea;

        Debug.Log($"重なり割合: {overlapRatio}");

        // 閾値以上なら正解
        return overlapRatio >= correctThreshold;
    }
}