using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// �{�^���Ŏw���̃V�[���ֈڍs
/// </summary>
public class SceneMoveButton : MonoBehaviour
{
    [Header("�ڍs�������V�[���̖��O"),SerializeField] private string _scenename;
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
