using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Judge : MonoBehaviour
{
    public Text correntText;  //����p�e�L�X�g
    public Text wrongText;�@�@�@//�s����p�e�L�X�g
    public float correctThreshold = 0.5f; //����
    private Collider2D woundCollider;  //�����̓����蔻��R���C�_�[


    private void Awake()
    {
        woundCollider = GetComponent<Collider2D>();
        if (woundCollider == null)
        {
            Debug.LogError($"Judge���A�^�b�`���Ă���I�u�W�F�N�g��Collider2D������܂���I:{gameObject.name}");
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
            Debug.LogError($"�n���ꂽship��Collider2D������܂���I:{ship.name}");
            return false;
        }

        Bounds shipBounds = shipCollider.bounds;
        Bounds woundBounds = woundCollider.bounds;

        if (!shipBounds.Intersects(woundBounds))
        {
            // �d�Ȃ��Ă��Ȃ��Ȃ�s����
            return false;
        }

        // �d�Ȃ��`���v�Z
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

        Debug.Log($"�d�Ȃ芄��: {overlapRatio}");

        // 臒l�ȏ�Ȃ琳��
        return overlapRatio >= correctThreshold;
    }
}