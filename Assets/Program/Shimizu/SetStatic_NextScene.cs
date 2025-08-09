using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetStatic_NextScene : MonoBehaviour
{
    [SerializeField] string sceneIfCountHigh; // �V�[�����i�J�E���g��2�ȏ�j
    [SerializeField] string sceneIfCountLow;  // �V�[�����i�J�E���g��2�����j

    void Start()
    {
        // �K�v�Ȃ�StaticMember�Ȃǂɒl��ۑ�
    }

    public void LoadScene()
    {
        if (KeywordCount.Instance == null)
        {
            Debug.LogError("KeywordCount��Instance��������܂���");
            return;
        }

        int count = KeywordCount.Instance.Getcount();

        if (count >= 2)
        {
            Debug.Log("A" + count);
            SceneManager.LoadScene(sceneIfCountHigh);
        }
        else
        {
            Debug.Log("B" + count);
            SceneManager.LoadScene(sceneIfCountLow);
        }
    }
}
