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
        if (KeywordCount.Instance == null)
        {
            Debug.LogError("KeywordCountのInstanceが見つかりません");
            return;
        }

        int count = KeywordCount.Instance.Getcount();

        if (count >= 2)
        {
            Debug.Log("A" + count);

            SceneManager.LoadScene("何か入れる");
            //治療のスクリーンを入れる
        }
        else
        {
            Debug.Log("B" + count);

            SceneManager.LoadScene("何か入れる");
            //戻るスクリーンを入れる
        }
        
    }
}