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

    [Header("表示先（左側のText）")]
    [SerializeField] Text leftText;

    // 必要ならインスペクターから差し替えられるように
    [Header("置換ワード設定")]
    [SerializeField] string srcYasashiku = "やさしく";
    [SerializeField] string repYasashiku = "ゆっくり";
    [SerializeField] string srcSasatto = "ささっと";
    [SerializeField] string repSasatto = "すばやく";
    [SerializeField] string srcHirihiri = "ひりひり";
    [SerializeField] string repHirihiri = "擦り傷";
    [SerializeField] string srcGangan = "ガンガン";
    [SerializeField] string repGangan = "打撲";

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

                                    src; // それ以外はそのまま

        if (string.IsNullOrEmpty(message)) return;

        leftText.text = message;
        leftText.gameObject.SetActive(true);
    }
}
