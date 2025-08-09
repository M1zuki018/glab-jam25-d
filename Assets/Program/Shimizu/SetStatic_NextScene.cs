using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetStatic_NextScene : MonoBehaviour
{

    [SerializeField] int setIndex;
    //ロードするシーンにわかりやすく番号をつける
    void Start()
    {
        StaticMember.Index = setIndex;
        //設定された値をStaticMemberに保存
    }
    public void LoadScene()
    {
        SceneManager.LoadScene("何か入れる");
        //ロードしたいスクリーンを入れる
    }
}