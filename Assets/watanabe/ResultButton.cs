using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultButton : MonoBehaviour
{
    public string resultSceneName = "Result";  // ���U���g�V�[����

    public void OnResultButtonClicked()
    {
        // �V�[�����̂��ׂĂ� Judge ���擾
        Judge[] judges = FindObjectsOfType<Judge>();

        if (judges.Length == 0)
        {
            Debug.LogError("Judge���V�[�����ɑ��݂��܂���I");
            return;
        }

        // �S���̏��iJudge�j�𔻒�
        foreach (Judge judge in judges)
        {
            judge.EvaluateAll();
        }

        Debug.Log($"���v�X�R�A: {ScoreManager.Instance.Score}");

        // �Ō�Ƀ��U���g�V�[���ֈړ�
        SceneManager.LoadScene(resultSceneName);
    }
}
