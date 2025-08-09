using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextManager : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("色設定")]
    [SerializeField] Color normalColor = Color.white;
    [SerializeField] Color hoverColor = new Color(1f, 0.8f, 0.2f);

    [Header("クリック時に左へ表示するテキスト")]
    [SerializeField] Text leftText;
    [SerializeField] string messageOnLeft = "左側に表示する内容";

    Text self;

    void Awake()
    {
        self = GetComponent<Text>();
        self.color = normalColor;

        if (leftText != null)
        {
            leftText.gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        self.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        self.color = normalColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (leftText == null) return;

        leftText.text = messageOnLeft;
        leftText.gameObject.SetActive(true);
    }
}
