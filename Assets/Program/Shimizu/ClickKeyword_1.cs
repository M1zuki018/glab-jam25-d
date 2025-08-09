using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickKeyword_1 : MonoBehaviour
{
        [SerializeField]
        private TextMeshProUGUI descriptionText;
        private void Start()
        {

        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    // 自分がクリックされたときだけ表示切替
                    bool isActive = descriptionText.gameObject.activeSelf;
                    SetTextVisible(false);
                }
            }
        }

        private void SetTextVisible(bool visible)
        {
            descriptionText.gameObject.SetActive(visible);
        }
    }

