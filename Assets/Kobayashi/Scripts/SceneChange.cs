using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// �V�[���ڍs
/// </summary>
public class SceneChange : MonoBehaviour
{
    /// <summary>
    /// �w���̃V�[���Ɉڍs
    /// </summary>
    /// <param name="sceneName"></param>
    public void ChangingScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
