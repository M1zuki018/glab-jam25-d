using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetStatic_NextScene : MonoBehaviour
{

    [SerializeField] string _sceneName;
    //ロードするシーンにわかりやすく番号をつける
    void Start()
    {
        //StaticMember.Index = setIndex;
        //設定された値をStaticMemberに保存
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(_sceneName);
        //ロードしたいスクリーンを入れる
    }
}