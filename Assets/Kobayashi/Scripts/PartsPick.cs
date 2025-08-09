using UnityEngine;
using UnityEngine.UI;

public class PartsPick : MonoBehaviour
{
    [Header("部位の参照")]
    [SerializeField] private GameObject _head;
    [SerializeField] private GameObject _righthand;
    [SerializeField] private GameObject _lefthand;
    [SerializeField] private GameObject _rightleg;
    [SerializeField] private GameObject _leftleg;
    [Header("傷")]
    [Header("頭"), SerializeField] private GameObject[] _headdamages;
    [Header("右手"), SerializeField] private GameObject[] _righthanddamages;
    [Header("左手"), SerializeField] private GameObject[] _lefthanddamages;
    [Header("右足"), SerializeField] private GameObject[] _rightlegdamages;
    [Header("左足"), SerializeField] private GameObject[] _leftlegdamages;
    [Header("傷の透明度"), SerializeField, Range(0f, 1f)] private float _resetaipha;
    [Header("治療法表示ボタン"), SerializeField] private Button _button;
    [Header("ガーゼ"),SerializeField] private Image _gauze;
    [Header("絆創膏"), SerializeField] private Image _bandage;
    [Header("やり直しボタン"), SerializeField] private Button _retryButton;
    [Header("施術完了ボタン"), SerializeField] private Button _finishButton;
    [Header("薬生成コンポーネント"), SerializeField] private CompressSpawner _compressSpawner;
    public bool _expansion;
    private bool _display;
    Camera _camera;

    /// <summary>
    /// クリックで拡大、傷の表示
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        _button.onClick.AddListener(SelectTreatment);
        _button.gameObject.SetActive(false);
        _bandage.gameObject.SetActive(false);
        _gauze.gameObject.SetActive(false);
        _camera = Camera.main;
        _expansion = false;
        _display = false;
        ResetAlpha();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_expansion)//左クリック時
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //RaycastHit2D[] hit = 
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0, LayerMask.GetMask("PatientParts"));
            if(hit.collider != null)
            {
                BodyPart _part = hit.collider.GetComponent<BodyPart>();
                GameObject target = null;
                switch (_part._bodyPart)//部位の判定
                {
                    case BodyPartType.Head: target = _head; SetAlpha(_headdamages,1f); break;
                    case BodyPartType.RightHand: target = _righthand; SetAlpha(_righthanddamages,1f); break;
                    case BodyPartType.LeftHand: target = _lefthand; SetAlpha(_lefthanddamages,1f); break;
                    case BodyPartType.RightLeg: target = _rightleg; SetAlpha(_rightlegdamages,1f); break;
                    case BodyPartType.LeftLeg: target = _leftleg; SetAlpha(_leftlegdamages,1f); break;
                }

                //カメラの拡大、移動
                _camera.orthographicSize = 2f;
                _camera.transform.position = new Vector3(
                    target.transform.position.x,
                    target.transform.position.y,
                    _camera.transform.position.z);
                _expansion = true;

                _retryButton.interactable = false;
                _finishButton.interactable = false;
            }
        }
        if (Input.GetMouseButtonDown(1) && _expansion)//右クリック時
        {
            ResetCamera();
            _gauze.gameObject.SetActive(false);
            _bandage.gameObject.SetActive(false);
            _display = false;

            _compressSpawner.CanSpawn = false;
            _retryButton.interactable = true;
            _finishButton.interactable = true;
        }
        _button.gameObject.SetActive(_expansion);
    }
    /// <summary>
    /// カメラ位置、大きさを元に戻す
    /// </summary>
    private void ResetCamera()
    {
        _camera.orthographicSize = 5f;
        _camera.transform.position = new Vector3(0f, 0f, -10f);
        _expansion = false;
        ResetAlpha();
    }
    /// <summary>
    /// 傷の透明度のリセット
    /// </summary>
    private void ResetAlpha()
    {
        SetAlpha(_headdamages, _resetaipha);
        SetAlpha(_righthanddamages, _resetaipha);
        SetAlpha(_lefthanddamages, _resetaipha);
        SetAlpha(_rightlegdamages, _resetaipha);
        SetAlpha(_leftlegdamages, _resetaipha);
    }
    /// <summary>
    /// 傷の透明度を変える
    /// </summary>
    /// <param name="objects"></param>
    /// <param name="alpha"></param>
    private void SetAlpha(GameObject[] objects, float alpha)
    {
        for(int i = 0; i < objects.Length; i++)
        {
            SpriteRenderer sr = objects[i].GetComponent<SpriteRenderer>();
            Color c = sr.color;
            c.a = alpha;
            sr.color = c;
        }     
    }
    /// <summary>
    /// 治療法の表示
    /// </summary>
    private void SelectTreatment()
    {
        if (!_display)
        {
            _gauze.gameObject.SetActive(true);
            _bandage.gameObject.SetActive(true);
            _display = true;
        }
        else
        {
            _gauze.gameObject.SetActive(false);
            _bandage.gameObject.SetActive(false);
            _display = false;
        }
    }
}
