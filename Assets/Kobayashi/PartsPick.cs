using UnityEngine;

public class PartsPick : MonoBehaviour
{
    [Header("部位の参照")]
    [SerializeField] private GameObject _head;
    [SerializeField] private GameObject _righthand;
    [SerializeField] private GameObject _lefthand;
    [SerializeField] private GameObject _rightleg;
    [SerializeField] private GameObject _leftleg;
    private bool _expansion;
    Camera _camera;

    /// <summary>
    /// クリックで拡大の処理
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _expansion = false;
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
                GameObject _target = null;
                switch (hit.collider.tag)//部位の判定
                {
                    case "Head": _target = _head; break;
                    case "RightHand": _target = _righthand; break;
                    case "LeftHand": _target = _lefthand; break;
                    case "RightLeg": _target = _rightleg; break;
                    case "LeftLeg": _target = _leftleg; break;
                }

                //カメラの拡大、移動
                _camera.orthographicSize = 2f;
                _camera.transform.position = new Vector3(
                    _target.transform.position.x,
                    _target.transform.position.y,
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
    }
}
