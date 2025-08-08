using UnityEngine;

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
    public bool _expansion;
    Camera _camera;

    /// <summary>
    /// クリックで拡大、傷の表示
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _expansion = false;
        ResetAlpha();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_expansion)//左クリック時
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if(hit.collider != null)
            {
                GameObject target = null;
                switch (hit.collider.tag)//部位の判定
                {
                    case "Head": target = _head; SetAlpha(_headdamages,1f); break;
                    case "RightHand": target = _righthand; SetAlpha(_righthanddamages,1f); break;
                    case "LeftHand": target = _lefthand; SetAlpha(_lefthanddamages,1f); break;
                    case "RightLeg": target = _rightleg; SetAlpha(_rightlegdamages,1f); break;
                    case "LeftLeg": target = _leftleg; SetAlpha(_leftlegdamages,1f); break;
                }

                //カメラの拡大、移動
                _camera.orthographicSize = 2f;
                _camera.transform.position = new Vector3(
                    target.transform.position.x,
                    target.transform.position.y,
                    _camera.transform.position.z);
                _expansion = true;
            }
        }
        if (Input.GetMouseButtonDown(1) && _expansion)//右クリック時
        {
            ResetCamera();
        }
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
}
