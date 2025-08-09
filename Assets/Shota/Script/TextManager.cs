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

    [Header("�\����i������Text�j")]
    [SerializeField] Text leftText;

    // �K�v�Ȃ�C���X�y�N�^�[���獷���ւ�����悤��
    [Header("�u�����[�h�ݒ�")]
    [SerializeField] string srcYasashiku = "�₳����";
    [SerializeField] string repYasashiku = "�������";
    [SerializeField] string srcSasatto = "��������";
    [SerializeField] string repSasatto = "���΂₭";
    [SerializeField] string srcHirihiri = "�Ђ�Ђ�";
    [SerializeField] string repHirihiri = "�C�菝";
    [SerializeField] string srcGangan = "�K���K��";
    [SerializeField] string repGangan = "�Ŗo";

    Text self;

    void Awake()
    {
        self = GetComponent<Text>();
        self.color = normalColor;
        if (leftText) leftText.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData _) => self.color = hoverColor;
    public void OnPointerExit(PointerEventData _) => self.color = normalColor;

    public void OnPointerClick(PointerEventData _)
    {
        if (!leftText) return;

        var src = (self.text ?? "").Trim();
        string message =
            (src == srcYasashiku) ? repYasashiku :
            (src == srcSasatto) ? repSasatto :
            (src == srcHirihiri) ? repHirihiri :
            (src == srcGangan) ? repGangan :

                                    src; // ����ȊO�͂��̂܂�

        if (string.IsNullOrEmpty(message)) return;

        leftText.text = message;
        leftText.gameObject.SetActive(true);
    }
}
