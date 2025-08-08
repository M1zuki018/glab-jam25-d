using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �\���̐����������Ǘ�����R���|�[�l���g
/// </summary>
public class CompressSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _guaze;
    [SerializeField] private GameObject _bandage;
    [SerializeField, Tooltip("���҂̎��z���i�[����q�I�u�W�F�N�g")] private GameObject _patientCompresses;
    [SerializeField, TooltipAttribute("�K�[�[���\����O�̊g�嗦")] private float _pastingScaleRate = 1.2f;
    [SerializeField, TooltipAttribute("����\�G���A")] Collider2D _treatmentCollider;

    public EnumCompressType SelectedCompress = EnumCompressType.None;
    public bool CanSpawn = false;

    private GameObject _pastingCompress = null;
    private Vector2 _startPos;
    private Vector2 _endPos;
    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // �{�p�s�i�g���ԂłȂ��j�̏ꍇ�A�������Ȃ�
        if (!CanSpawn)
        {
            return;
        }

        // �K�[�[���I�����ꂽ�ꍇ�̏���
        if (SelectedCompress == EnumCompressType.Guaze)
        {
            if (Input.GetMouseButtonDown(0) && MouseInTreatmentArea())
            {
                // �}�E�X�����ʒu���n�_
                _startPos = _camera.ScreenToWorldPoint(Input.mousePosition);
                _pastingCompress = Instantiate(_guaze, _startPos, Quaternion.identity);
                _pastingCompress.transform.localScale = Vector3.one * _pastingScaleRate;
            }
            if (Input.GetMouseButton(0) && _pastingCompress != null)
            {
                // �}�E�X�h���b�O���ɁA�����Ɖ�]�p�x���X�V����
                _endPos = _camera.ScreenToWorldPoint(Input.mousePosition);
                Vector2 direction = _endPos - _startPos;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                _pastingCompress.GetComponent<SpriteRenderer>().size = new Vector2(0.5f, direction.magnitude / _pastingScaleRate);
                _pastingCompress.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            }
            if (Input.GetMouseButtonUp(0) && _pastingCompress != null)
            {
                // �}�E�X��������A�K�[�[�̃T�C�Y���}�E�X�ʒu�ɍ��킹���傫���ɐݒ肷��
                _pastingCompress.transform.localScale = Vector3.one;
                _endPos = _camera.ScreenToWorldPoint(Input.mousePosition);
                Vector2 direction = _endPos - _startPos;
                _pastingCompress.GetComponent<SpriteRenderer>().size = new Vector2(0.5f, direction.magnitude);


                // �p�[�e�B�N���G�t�F�N�g�̍Đ�
                ParticleSystem ps = _pastingCompress.GetComponent<ParticleSystem>();
                float psPosY = direction.magnitude / 2;
                var shape = ps.shape;
                shape.position = new Vector3(0, psPosY, 0);
                ps.Play();

                // �\�������҂̎q�I�u�W�F�N�g�Ƃ��Ēǉ�
                _pastingCompress.transform.SetParent(_patientCompresses.transform);
                _pastingCompress = null;

                // TODO �����Đ�
            }
        }
        // �J�n�p���I�����ꂽ�ꍇ�̏���
        else if (SelectedCompress == EnumCompressType.Bandage)
        {
            if (Input.GetMouseButtonDown(0) && MouseInTreatmentArea())
            {
                // �}�E�X�������A�J�n�p�𐶐�����
                _startPos = _camera.ScreenToWorldPoint(Input.mousePosition);
                _pastingCompress = Instantiate(_bandage, _startPos, Quaternion.identity);

                // �\�������҂̎q�I�u�W�F�N�g�Ƃ��Ēǉ�
                _pastingCompress.transform.SetParent(_patientCompresses.transform);
                _pastingCompress = null;
                // TODO �����Đ�
            }
        }
    }

    /// <summary>
    /// �}�E�X���{�p�G���A�̒������肷��
    /// </summary>
    /// <returns></returns>
    private bool MouseInTreatmentArea()
    {
        LayerMask layer = LayerMask.GetMask("AllowTreatmentLayer");
        Collider2D col = Physics2D.OverlapPoint(_camera.ScreenToWorldPoint(Input.mousePosition), layer);
        return col != null;
    }
}
