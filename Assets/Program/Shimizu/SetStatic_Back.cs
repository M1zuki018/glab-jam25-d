using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetStatic_Back : MonoBehaviour
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
        //ロードしたいシーンを入れる
    }
}