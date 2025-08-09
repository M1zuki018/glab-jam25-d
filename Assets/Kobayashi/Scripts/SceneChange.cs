using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// シーン移行
/// </summary>
public class SceneChange : MonoBehaviour
{
    /// <summary>
    /// 指定先のシーンに移行
    /// </summary>
    /// <param name="sceneName"></param>
    public void ChangingScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
