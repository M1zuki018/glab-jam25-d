using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// ボタンで指定先のシーンへ移行
/// </summary>
public class SceneMoveButton : MonoBehaviour
{
    [Header("移行したいシーンの名前"),SerializeField] private string _scenename;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(SceneChange);
    }

    void SceneChange()
    {
        SceneManager.LoadScene(_scenename);
    }
}
