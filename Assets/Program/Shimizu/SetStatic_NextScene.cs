using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetStatic_NextScene : MonoBehaviour
{

   [SerializeField] int setIndex;

    //���[�h����V�[���ɂ킩��₷���ԍ�������
    void Start()
    {
        StaticMember.Index = setIndex;
        //�ݒ肳�ꂽ�l��StaticMember�ɕۑ�
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

            SceneManager.LoadScene("���������");
            //���ẪX�N���[��������
        }
        else
        {
            Debug.Log("B" + count);

            SceneManager.LoadScene("���������");
            //�߂�X�N���[��������
        }
        
    }
}