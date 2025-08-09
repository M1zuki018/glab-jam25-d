using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetStatic_NextScene : MonoBehaviour
{
    [SerializeField] string sceneIfCountHigh; // シーン名（カウントが2以上）
    [SerializeField] string sceneIfCountLow;  // シーン名（カウントが2未満）

    void Start()
    {
        // 必要ならStaticMemberなどに値を保存
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
            SceneManager.LoadScene(sceneIfCountHigh);
        }
        else
        {
            Debug.Log("B" + count);
            SceneManager.LoadScene(sceneIfCountLow);
        }
    }
}
