using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Click_2 : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMeshProUGUI_2;
    [SerializeField]
    private TextMeshProUGUI textMeshProUGUI_4;
    [SerializeField]
    private TextMeshProUGUI textMeshProUGUI_5;
    [SerializeField]
    private TextMeshProUGUI textMeshProUGUI_6;
    [SerializeField]
    private TextMeshProUGUI textMeshProUGUI_7;

    private int check;
    void Update()
    {
        if (check >= 0)
        {
            if (Input.GetMouseButtonDown(0))  // 左クリック
            {
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.name + " がクリックされました");


                    textMeshProUGUI_2.color = new Color32(255, 255, 255, 0);
                    textMeshProUGUI_4.color = new Color32(255, 255, 255, 255);
                    textMeshProUGUI_5.color = new Color32(255, 255, 255, 0);
                    textMeshProUGUI_6.color = new Color32(255, 255, 255, 255);
                    textMeshProUGUI_7.color = new Color32(255, 255, 255, 0);
                }
            }
        }
    }
}
