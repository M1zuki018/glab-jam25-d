using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickToTitle : MonoBehaviour
{
    public string titleSceneName = "TitleScene";

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ¶ƒNƒŠƒbƒN
        {
            SceneManager.LoadScene(titleSceneName);
        }
    }
}
