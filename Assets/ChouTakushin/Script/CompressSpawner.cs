using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 貼り薬の生成処理を管理するコンポーネント
/// </summary>
public class CompressSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _guaze;
    [SerializeField] private GameObject _bandage;
    [SerializeField, Tooltip("患者の湿布を格納する子オブジェクト")] private GameObject _patientCompresses;
    [SerializeField, TooltipAttribute("ガーゼが貼られる前の拡大率")] private float _pastingScaleRate = 1.2f;
    [SerializeField, TooltipAttribute("操作可能エリア")] RectTransform _treatmentArea;

    public EnumCompressType SelectedCompress = EnumCompressType.None;
    public bool CanSpawn = false;

    private GameObject _pastingCompress = null; // 貼り付け中の貼り薬オブジェクト
    private Vector2 _startPos; // マウスドラッグ時の始点
    private Vector2 _endPos; // マウスドラッグ時の終点
    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        // 施術不可（拡大状態でない）の場合、処理しない
        if (!CanSpawn)
        {
            return;
        }

        // ガーゼが選択された場合の処理
        if (SelectedCompress == EnumCompressType.Guaze)
        {
            if (Input.GetMouseButtonDown(0) && MouseInTreatmentArea())
            {
                // マウス押下位置が始点
                _startPos = _camera.ScreenToWorldPoint(Input.mousePosition);
                _pastingCompress = Instantiate(_guaze, _startPos, Quaternion.identity);
                _pastingCompress.transform.localScale = Vector3.one * _pastingScaleRate;
                AudioManager.Instance.PlaySE("Sippu_Streching");
            }
            if (Input.GetMouseButton(0) && _pastingCompress != null)
            {
                // マウスドラッグ中に、長さと回転角度を更新する
                _endPos = _camera.ScreenToWorldPoint(Input.mousePosition);
                Vector2 direction = _endPos - _startPos;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                _pastingCompress.GetComponent<SpriteRenderer>().size = new Vector2(1, direction.magnitude / _pastingScaleRate);
                _pastingCompress.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            }
            if (Input.GetMouseButtonUp(0) && _pastingCompress != null)
            {
                // マウスを放す時、ガーゼのサイズをマウス位置に合わせた大きさに設定する
                _pastingCompress.transform.localScale = Vector3.one;
                _endPos = _camera.ScreenToWorldPoint(Input.mousePosition);
                Vector2 direction = _endPos - _startPos;
                _pastingCompress.GetComponent<SpriteRenderer>().size = new Vector2(1, direction.magnitude);

                // パーティクルエフェクトの再生
                ParticleSystem ps = _pastingCompress.GetComponent<ParticleSystem>();
                float psPosY = direction.magnitude / 2;
                var shape = ps.shape;
                shape.position = new Vector3(0, psPosY, 0);
                ps.Play();

                // 貼り薬を患者の子オブジェクトとして追加
                _pastingCompress.transform.SetParent(_patientCompresses.transform);
                _pastingCompress = null;

                // 音を再生
                AudioManager.Instance.PlaySEInterrupt("Sippu_Paste");
            }
        }
        // 絆創膏が選択された場合の処理
        else if (SelectedCompress == EnumCompressType.Bandage)
        {
            if (Input.GetMouseButtonDown(0) && MouseInTreatmentArea())
            {
                // マウス押下時、絆創膏を生成する
                _startPos = _camera.ScreenToWorldPoint(Input.mousePosition);
                _pastingCompress = Instantiate(_bandage, _startPos, Quaternion.identity);

                // 貼り薬を患者の子オブジェクトとして追加
                _pastingCompress.transform.SetParent(_patientCompresses.transform);
                _pastingCompress = null;
                // 音を再生
                AudioManager.Instance.PlaySEInterrupt("Sippu_Paste");
            }
        }
    }

    /// <summary>
    /// マウスが施術エリアの中か判定する
    /// </summary>
    /// <returns></returns>
    private bool MouseInTreatmentArea()
    {
        bool rst = RectTransformUtility.RectangleContainsScreenPoint(_treatmentArea, Input.mousePosition);
        //Debug.Log("Allow TreatmentArea : " + rst);
        return rst;
    }
}
