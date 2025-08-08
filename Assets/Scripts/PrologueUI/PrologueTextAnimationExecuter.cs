using UnityEngine;
using TMPro;
using System.Collections;

public class PrologueTextAnimationExecuter : MonoBehaviour
{
    [Header("Prologue text details")]
    private string[] prologueMessage;
    [SerializeField] private TMP_Text prologueText;
    [SerializeField] private float textSpeed = 0.05f;
    [SerializeField] private float paragraphPause = 1f;
    [SerializeField] private GameObject prologueUI;
    [SerializeField] private GameObject skipButton;
    [SerializeField] private float duration = 0.5f;

    private bool isFading = false;

    void Start()
    {
        prologueText.text = "";

        prologueMessage = new string[]
        {
            "ここは、妖怪たちがひっそりと集う“癒しの診療所”。\n" +
            "痛みを隠して強がる子も、本当のことを話してくれない子も、たくさん。\n" +
            "最初はみんな、心に鍵をかけてやってくる。\n\n" ,

            "人間からは恐れられる彼らが、実は誰よりも寂しがりで、\n" +
            "誰よりも優しいことを、\n" +
            "俺は知っている。\n\n",

            "気づけば、あの日憧れた「妖怪たちの友達」という夢は、\n" +
            "もうすぐ、夢じゃなくなる。"
        };

        StartCoroutine(RunPrologue());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isFading)
            ClosePrologueUI();

    }

    private void ClosePrologueUI()
    {
        StartCoroutine(FadeOutUI(duration));
    }

    private IEnumerator RunPrologue()
    {
        for(int i = 0; i < prologueMessage.Length; i++)
        {
            yield return StartCoroutine(FadeInText(prologueMessage[i]));

            yield return new WaitForSeconds(paragraphPause);
        }

        yield return new WaitForSeconds(1.5f);
        StartCoroutine(FadeOutUI(2f));
    }

    private IEnumerator FadeInText(string paragraph)
    {
        foreach(char c in paragraph)
        {
            if (isFading) yield break;

            prologueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    private IEnumerator FadeOutUI(float duration)
    {

        isFading = true;

        skipButton.SetActive(false);

        Color originalColor = prologueText.color;
        float elapsed = 0f;

        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            prologueText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        prologueText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        prologueUI.SetActive(false);
    }

}
