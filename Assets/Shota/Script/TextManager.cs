using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextManager : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("�F�ݒ�")]
    [SerializeField] Color normalColor = Color.white;
    [SerializeField] Color hoverColor = new Color(1f, 0.8f, 0.2f);

    [Header("�N���b�N���ɍ��֕\������e�L�X�g")]
    [SerializeField] Text leftText;
    [SerializeField] string messageOnLeft = "�����ɕ\��������e";

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
