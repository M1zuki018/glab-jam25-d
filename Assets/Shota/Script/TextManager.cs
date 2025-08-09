using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextKeyword : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("見た目")]
    [SerializeField] Color normalColor = Color.white;
    [SerializeField] Color hoverColor = new Color(1f, 0.8f, 0.2f);

    [Header("このメインキーワードのID（0/1/2）")]
    [SerializeField] int keywordId;

    [Header("1回目表示に使うラベル（未設定なら自分のTextを使用）")]
    [SerializeField] string firstViewLabelOverride;

    [Header("フロー管理参照")]
    [SerializeField] KeywordFlowManager flow;

    Text _self;

    void Awake()
    {
        _self = GetComponent<Text>();
        _self.color = normalColor;
    }

    public void OnPointerEnter(PointerEventData _) => _self.color = hoverColor;
    public void OnPointerExit(PointerEventData _) => _self.color = normalColor;

    public void OnPointerClick(PointerEventData _)
    {
        if (!flow) { Debug.LogWarning("[TextKeyword] flow未設定"); return; }

        string label = string.IsNullOrEmpty(firstViewLabelOverride) ? _self.text : firstViewLabelOverride;
        flow.OnKeywordClicked(keywordId, label);
    }
}
