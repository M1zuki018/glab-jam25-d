using UnityEngine;

public class PartsPick : MonoBehaviour
{
    [Header("���ʂ̎Q��")]
    [SerializeField] private GameObject _head;
    [SerializeField] private GameObject _righthand;
    [SerializeField] private GameObject _lefthand;
    [SerializeField] private GameObject _rightleg;
    [SerializeField] private GameObject _leftleg;
    [Header("��")]
    [Header("��"), SerializeField] private GameObject[] _headdamages;
    [Header("�E��"), SerializeField] private GameObject[] _righthanddamages;
    [Header("����"), SerializeField] private GameObject[] _lefthanddamages;
    [Header("�E��"), SerializeField] private GameObject[] _rightlegdamages;
    [Header("����"), SerializeField] private GameObject[] _leftlegdamages;
    [Header("���̓����x"), SerializeField, Range(0f, 1f)] private float _resetaipha;
    public bool _expansion;
    Camera _camera;

    /// <summary>
    /// �N���b�N�Ŋg��A���̕\��
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
        if (Input.GetMouseButtonDown(0) && !_expansion)//���N���b�N��
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if(hit.collider != null)
            {
                GameObject target = null;
                switch (hit.collider.tag)//���ʂ̔���
                {
                    case "Head": target = _head; SetAlpha(_headdamages,1f); break;
                    case "RightHand": target = _righthand; SetAlpha(_righthanddamages,1f); break;
                    case "LeftHand": target = _lefthand; SetAlpha(_lefthanddamages,1f); break;
                    case "RightLeg": target = _rightleg; SetAlpha(_rightlegdamages,1f); break;
                    case "LeftLeg": target = _leftleg; SetAlpha(_leftlegdamages,1f); break;
                }

                //�J�����̊g��A�ړ�
                _camera.orthographicSize = 2f;
                _camera.transform.position = new Vector3(
                    target.transform.position.x,
                    target.transform.position.y,
                    _camera.transform.position.z);
                _expansion = true;
            }
        }
        if (Input.GetMouseButtonDown(1) && _expansion)//�E�N���b�N��
        {
            ResetCamera();
        }
    }
    /// <summary>
    /// �J�����ʒu�A�傫�������ɖ߂�
    /// </summary>
    private void ResetCamera()
    {
        _camera.orthographicSize = 5f;
        _camera.transform.position = new Vector3(0f, 0f, -10f);
        _expansion = false;
        ResetAlpha();
    }
    /// <summary>
    /// ���̓����x�̃��Z�b�g
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
    /// ���̓����x��ς���
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
