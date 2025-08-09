using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class KeywordFlowManager : MonoBehaviour
{
    [Header("左側の3つのリンク済みテキスト（A/B/C用）")]
    [SerializeField] Text[] leftKeywordTexts = new Text[3]; // index: 0,1,2

    [Header("2回目選択後に表示する“結果テキスト”")]
    [SerializeField] Text resultText; // これは左の3枠とは別のText

    [Serializable]
    public struct Rule
    {
        public int firstId;     // 1回目に押されたメインID（0/1/2）
        public int secondId;    // 2回目に押されたメインID（0/1/2）
        [TextArea] public string result; // 表示したい結果テキスト
    }

    [Header("（1回目, 2回目）→ 結果テキスト の対応表")]
    [SerializeField] Rule[] rules;

    enum Phase { First, Second }
    Phase _phase = Phase.First;
    int _firstSelectedId = -1;

    Dictionary<(int, int), string> _lookup;

    void Awake()
    {
        // 初期状態：左3枠オフ、結果もオフ
        HideAllLeftKeywordTexts();
        if (resultText) resultText.gameObject.SetActive(false);

        // ルックアップ辞書に展開
        _lookup = new Dictionary<(int, int), string>();
        foreach (var r in rules)
        {
            _lookup[(r.firstId, r.secondId)] = r.result;
        }
    }

    /// <summary>メインのキーワードがクリックされた時に呼ぶ</summary>
    public void OnKeywordClicked(int id, string labelForFirstView = null)
    {
        if (_phase == Phase.First)
        {
            // 1回目：対応する左枠を1つだけ表示
            HideAllLeftKeywordTexts();
            if (resultText) resultText.gameObject.SetActive(false);

            var t = GetLeftTextById(id);
            if (t != null)
            {
                if (!string.IsNullOrEmpty(labelForFirstView))
                    t.text = labelForFirstView; // 左枠の中身をメイン名で上書きしたいなら
                t.gameObject.SetActive(true);
            }

            _firstSelectedId = id;
            _phase = Phase.Second;
        }
        else
        {
            // 2回目：結果テキストに置き換え
            string result = null;
            if (_lookup != null && _lookup.TryGetValue((_firstSelectedId, id), out var found))
            {
                result = found;
            }
            else
            {
                result = $"結果未設定: ({_firstSelectedId},{id})";
            }

            HideAllLeftKeywordTexts();
            if (resultText)
            {
                resultText.text = result;
                resultText.gameObject.SetActive(true);
            }

            // サイクルをリセット
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
