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
    [SerializeField, TooltipAttribute("����\�G���A")] RectTransform _treatmentArea;

    public EnumCompressType SelectedCompress = EnumCompressType.None;
    public bool CanSpawn = false;

    private GameObject _pastingCompress = null; // �\��t�����̓\���I�u�W�F�N�g
    private Vector2 _startPos; // �}�E�X�h���b�O���̎n�_
    private Vector2 _endPos; // �}�E�X�h���b�O���̏I�_
    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
    }

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
                AudioManager.Instance.PlaySE("Sippu_Streching");
            }
            if (Input.GetMouseButton(0) && _pastingCompress != null)
            {
                // �}�E�X�h���b�O���ɁA�����Ɖ�]�p�x���X�V����
                _endPos = _camera.ScreenToWorldPoint(Input.mousePosition);
                Vector2 direction = _endPos - _startPos;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                _pastingCompress.GetComponent<SpriteRenderer>().size = new Vector2(1, direction.magnitude / _pastingScaleRate);
                _pastingCompress.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            }
            if (Input.GetMouseButtonUp(0) && _pastingCompress != null)
            {
                // �}�E�X��������A�K�[�[�̃T�C�Y���}�E�X�ʒu�ɍ��킹���傫���ɐݒ肷��
                _pastingCompress.transform.localScale = Vector3.one;
                _endPos = _camera.ScreenToWorldPoint(Input.mousePosition);
                Vector2 direction = _endPos - _startPos;
                _pastingCompress.GetComponent<SpriteRenderer>().size = new Vector2(1, direction.magnitude);

                // �p�[�e�B�N���G�t�F�N�g�̍Đ�
                ParticleSystem ps = _pastingCompress.GetComponent<ParticleSystem>();
                float psPosY = direction.magnitude / 2;
                var shape = ps.shape;
                shape.position = new Vector3(0, psPosY, 0);
                ps.Play();

                // �\�������҂̎q�I�u�W�F�N�g�Ƃ��Ēǉ�
                _pastingCompress.transform.SetParent(_patientCompresses.transform);
                _pastingCompress = null;

                // �����Đ�
                AudioManager.Instance.PlaySEInterrupt("Sippu_Paste");
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
                // �����Đ�
                AudioManager.Instance.PlaySEInterrupt("Sippu_Paste");
            }
        }
    }

    /// <summary>
    /// �}�E�X���{�p�G���A�̒������肷��
    /// </summary>
    /// <returns></returns>
    private bool MouseInTreatmentArea()
    {
        bool rst = RectTransformUtility.RectangleContainsScreenPoint(_treatmentArea, Input.mousePosition);
        //Debug.Log("Allow TreatmentArea : " + rst);
        return rst;
    }
}
