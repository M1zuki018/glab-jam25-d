using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Judge : MonoBehaviour
{
    public Text correctText;
    public Text wrongText;
    public float correctThreshold = 0.5f;

    private Collider2D woundCollider;
    private bool scored = false; // 一度スコア加算したか

    private void Awake()
    {
        woundCollider = GetComponent<Collider2D>();
        if (woundCollider == null)
            Debug.LogError($"JudgeにCollider2Dがありません！:{gameObject.name}");
        correctText.gameObject.SetActive(false);
        wrongText.gameObject.SetActive(false);
    }

    public void OnShipPlaced(GameObject ship)
    {
        if (scored) return; // 既にスコア加算済みなら無視

        Collider2D shipCollider = ship.GetComponent<Collider2D>();
        if (shipCollider == null)
        {
            Debug.LogError($"渡されたshipにCollider2Dがありません！ : {ship.name}");
            return;
        }

        float overlapArea = GetOverlapArea(shipCollider, woundCollider);
        if (overlapArea <= 0)
        {
            ShowWrong();
            return;
        }

        float shipArea = shipCollider.bounds.size.x * shipCollider.bounds.size.y;
        float overlapRatio = overlapArea / shipArea;

        if (overlapRatio >= correctThreshold)
        {
            ShowCorrect();
            ScoreManager.Instance.AddScore(1);
            scored = true;
            Debug.Log($"傷口 {gameObject.name} に正しく貼れました。スコア加算！");
        }
        else
        {
            ShowWrong();
            Debug.Log($"傷口 {gameObject.name} は不正解です。");
        }
    }

    private void ShowCorrect()
    {
        correctText.gameObject.SetActive(true);
        wrongText.gameObject.SetActive(false);
    }

    private void ShowWrong()
    {
        correctText.gameObject.SetActive(false);
        wrongText.gameObject.SetActive(true);
    }

    private float GetOverlapArea(Collider2D a, Collider2D b)
    {
        Bounds aBounds = a.bounds;
        Bounds bBounds = b.bounds;

        float xMin = Mathf.Max(aBounds.min.x, bBounds.min.x);
        float xMax = Mathf.Min(aBounds.max.x, bBounds.max.x);
        float yMin = Mathf.Max(aBounds.min.y, bBounds.min.y);
        float yMax = Mathf.Min(aBounds.max.y, bBounds.max.y);

        float width = xMax - xMin;
        float height = yMax - yMin;

        if (width <= 0 || height <= 0) return 0f;
        return width * height;
    }
}