using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetStatic_NextScene : MonoBehaviour
{

    [SerializeField] string _sceneName;
    //���[�h����V�[���ɂ킩��₷���ԍ�������
    void Start()
    {
        //StaticMember.Index = setIndex;
        //�ݒ肳�ꂽ�l��StaticMember�ɕۑ�
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(_sceneName);
        //���[�h�������X�N���[��������
    }
}