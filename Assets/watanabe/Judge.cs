using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Judge : MonoBehaviour
{
    [Header("���肷�鏝�I�u�W�F�N�g")]
    public List<GameObject> wounds;  // Inspector�ŏ��I�u�W�F�N�g��o�^

    [Header("����Ɏg���V�b�v�̃^�O��")]
    public string shipTag = "Ship";

    [Header("���𔻒�ɕK�v�ȏd�Ȃ芄�� (0~1)")]
    [Range(0f, 1f)]
    public float correctThreshold = 0.5f;

    [Header("����������1������̃X�R�A")]
    public int scorePerCorrect = 1;

    private int totalScore;

    /// <summary>
    /// ���U���g�{�^���������Ɉ�Ĕ��肷�郁�\�b�h
    /// </summary>
    public void EvaluateAll()
    {
        totalScore = 0;

        foreach (var wound in wounds)
        {
            Collider2D woundCol = wound.GetComponent<Collider2D>();
            if (woundCol == null)
            {
                Debug.LogWarning($"����Collider2D���t���Ă��܂���: {wound.name}");
                continue;
            }

            bool isCorrect = false;

            // �d�Ȃ��Ă���Collider���擾
            Collider2D[] overlapped = Physics2D.OverlapBoxAll(woundCol.bounds.center, woundCol.bounds.size, 0f);

            foreach (var col in overlapped)
            {
                if (col.CompareTag(shipTag))
                {
                    float overlapArea = CalculateOverlapArea(woundCol, col);
                    float woundArea = woundCol.bounds.size.x * woundCol.bounds.size.y;
                    float colArea = col.bounds.size.x * col.bounds.size.y;

                    // �����ǂ̂��炢�������Ă��邩�Ŕ���
                    float ratio = overlapArea / woundArea;

                    if (ratio >= correctThreshold)
                    {
                        isCorrect = true;
                        Debug.Log($"�����I {wound.name} �̓V�b�v {col.gameObject.name} �ɂ���ăJ�o�[����܂��� (�d�Ȃ芄��: {ratio:F2})");
                        break; // ���̏��͐����Ȃ̂Ŏ��̏���
                    }
                }
            }

            if (isCorrect)
            {
                totalScore += scorePerCorrect;
            }
            else
            {
                Debug.Log($"�s����: {wound.name} �͏\���ɃJ�o�[����Ă��܂���");
            }
        }

        ScoreManager.Instance.AddScore(totalScore);
        Debug.Log($"����I���B�X�R�A���Z: {totalScore}");
    }

    
    /// �Q��Collider2D�̏d�Ȃ�ʐς��ȈՌv�Z
   
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