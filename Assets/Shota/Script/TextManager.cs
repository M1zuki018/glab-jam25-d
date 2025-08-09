using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextKeyword : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("������")]
    [SerializeField] Color normalColor = Color.white;
    [SerializeField] Color hoverColor = new Color(1f, 0.8f, 0.2f);

    [Header("���̃��C���L�[���[�h��ID�i0/1/2�j")]
    [SerializeField] int keywordId;

    [Header("1��ڕ\���Ɏg�����x���i���ݒ�Ȃ玩����Text���g�p�j")]
    [SerializeField] string firstViewLabelOverride;

    [Header("�t���[�Ǘ��Q��")]
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
        if (!flow) { Debug.LogWarning("[TextKeyword] flow���ݒ�"); return; }

        string label = string.IsNullOrEmpty(firstViewLabelOverride) ? _self.text : firstViewLabelOverride;
        flow.OnKeywordClicked(keywordId, label);
    }
}
