using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class KeywordFlowManager : MonoBehaviour
{
    [Header("������3�̃����N�ς݃e�L�X�g�iA/B/C�p�j")]
    [SerializeField] Text[] leftKeywordTexts = new Text[3]; // index: 0,1,2

    [Header("2��ڑI����ɕ\������g���ʃe�L�X�g�h")]
    [SerializeField] Text resultText; // ����͍���3�g�Ƃ͕ʂ�Text

    [Serializable]
    public struct Rule
    {
        public int firstId;     // 1��ڂɉ����ꂽ���C��ID�i0/1/2�j
        public int secondId;    // 2��ڂɉ����ꂽ���C��ID�i0/1/2�j
        [TextArea] public string result; // �\�����������ʃe�L�X�g
    }

    [Header("�i1���, 2��ځj�� ���ʃe�L�X�g �̑Ή��\")]
    [SerializeField] Rule[] rules;

    enum Phase { First, Second }
    Phase _phase = Phase.First;
    int _firstSelectedId = -1;

    Dictionary<(int, int), string> _lookup;

    void Awake()
    {
        // ������ԁF��3�g�I�t�A���ʂ��I�t
        HideAllLeftKeywordTexts();
        if (resultText) resultText.gameObject.SetActive(false);

        // ���b�N�A�b�v�����ɓW�J
        _lookup = new Dictionary<(int, int), string>();
        foreach (var r in rules)
        {
            _lookup[(r.firstId, r.secondId)] = r.result;
        }
    }

    /// <summary>���C���̃L�[���[�h���N���b�N���ꂽ���ɌĂ�</summary>
    public void OnKeywordClicked(int id, string labelForFirstView = null)
    {
        if (_phase == Phase.First)
        {
            // 1��ځF�Ή����鍶�g��1�����\��
            HideAllLeftKeywordTexts();
            if (resultText) resultText.gameObject.SetActive(false);

            var t = GetLeftTextById(id);
            if (t != null)
            {
                if (!string.IsNullOrEmpty(labelForFirstView))
                    t.text = labelForFirstView; // ���g�̒��g�����C�����ŏ㏑���������Ȃ�
                t.gameObject.SetActive(true);
            }

            _firstSelectedId = id;
            _phase = Phase.Second;
        }
        else
        {
            // 2��ځF���ʃe�L�X�g�ɒu������
            string result = null;
            if (_lookup != null && _lookup.TryGetValue((_firstSelectedId, id), out var found))
            {
                result = found;
            }
            else
            {
                result = $"���ʖ��ݒ�: ({_firstSelectedId},{id})";
            }

            HideAllLeftKeywordTexts();
            if (resultText)
            {
                resultText.text = result;
                resultText.gameObject.SetActive(true);
            }

            // �T�C�N�������Z�b�g
            _phase = Phase.First;
            _firstSelectedId = -1;
        }
    }

    Text GetLeftTextById(int id)
    {
        if (id < 0 || id >= leftKeywordTexts.Length) return null;
        return leftKeywordTexts[id];
    }

    void HideAllLeftKeywordTexts()
    {
        foreach (var t in leftKeywordTexts)
        {
            if (t) t.gameObject.SetActive(false);
        }
    }
}
