using UnityEngine;
using UnityEngine.UI;

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
    [Header("���Ö@�\���{�^��"), SerializeField] private Button _button;
    [Header("�K�[�["),SerializeField] private Image _gauze;
    [Header("�J�n�p"), SerializeField] private Image _bandage;
    [Header("��蒼���{�^��"), SerializeField] private Button _retryButton;
    [Header("�{�p�����{�^��"), SerializeField] private Button _finishButton;
    [Header("�򐶐��R���|�[�l���g"), SerializeField] private CompressSpawner _compressSpawner;
    public bool _expansion;
    private bool _display;
    Camera _camera;

    /// <summary>
    /// �N���b�N�Ŋg��A���̕\��
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
        if (Input.GetMouseButtonDown(0) && !_expansion)//���N���b�N��
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //RaycastHit2D[] hit = 
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0, LayerMask.GetMask("PatientParts"));
            if(hit.collider != null)
            {
                BodyPart _part = hit.collider.GetComponent<BodyPart>();
                GameObject target = null;
                switch (_part._bodyPart)//���ʂ̔���
                {
                    case BodyPartType.Head: target = _head; SetAlpha(_headdamages,1f); break;
                    case BodyPartType.RightHand: target = _righthand; SetAlpha(_righthanddamages,1f); break;
                    case BodyPartType.LeftHand: target = _lefthand; SetAlpha(_lefthanddamages,1f); break;
                    case BodyPartType.RightLeg: target = _rightleg; SetAlpha(_rightlegdamages,1f); break;
                    case BodyPartType.LeftLeg: target = _leftleg; SetAlpha(_leftlegdamages,1f); break;
                }

                //�J�����̊g��A�ړ�
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
        if (Input.GetMouseButtonDown(1) && _expansion)//�E�N���b�N��
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
    /// <summary>
    /// ���Ö@�̕\��
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
