using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetStatic_Back : MonoBehaviour
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
        SceneManager.LoadScene("���������");
        //���[�h�������V�[��������
    }
}