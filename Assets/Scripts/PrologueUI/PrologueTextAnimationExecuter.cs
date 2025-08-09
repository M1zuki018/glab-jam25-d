using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;

public class PrologueTextAnimationExecuter : MonoBehaviour
{
    [Header("Text Details")]
    private string[] prologueMessage;
    [SerializeField] private float textSpeed = 0.05f;
    [SerializeField] private float paragraphPause = 1f;
    [SerializeField] private float duration = 0.5f;

    [Header("UI Elements")]
    [SerializeField] private GameObject ReadyUI;
    private GameObject prologueUI;
    private GameObject skipButton;
    private TMP_Text prologueText;

    [Header("Effects Details")]
    [SerializeField] private float waitBeforeFade = 1f;
    [SerializeField] private float soundPauseDuration = 1f;
    private GameObject injuryManager;

    private Coroutine prologueCoroutine;

    private bool isFading = false;

    private void Awake()
    {
        prologueUI = this.gameObject;
        skipButton = GameObject.Find("SkipButton");
        injuryManager = GameObject.Find("InjuryManager");
        prologueText = GetComponentInChildren<TMP_Text>();
    }

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

        prologueCoroutine = StartCoroutine(RunPrologue());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isFading)
        {
            if (prologueCoroutine != null)
                StopCoroutine(prologueCoroutine);

            ClosePrologueUI();
            SoundManager.Instance.StopPrologueSFX();
        }
    }

    private void ClosePrologueUI()
    {
        StartCoroutine(FadeOutUI(duration));
    }

    private IEnumerator RunPrologue()
    {
        SoundManager.Instance.musicSource.Stop();
        SoundManager.Instance.prologueSfxHasPlayed = false;
        SoundManager.Instance.PlayPrologueSFX();

        for (int i = 0; i < prologueMessage.Length; i++)
        {
            yield return StartCoroutine(FadeInText(prologueMessage[i]));

            if (i < prologueMessage.Length - 1)
            {
                SoundManager.Instance.StopPrologueSFX();
                yield return new WaitForSeconds(soundPauseDuration);
                SoundManager.Instance.PlayPrologueSFX();

                float remainingPause = paragraphPause - soundPauseDuration;
                if (remainingPause > 0)
                    yield return new WaitForSeconds(remainingPause);
            }
        }

        SoundManager.Instance.StopPrologueSFX();
        yield return new WaitForSeconds(waitBeforeFade);
        StartCoroutine(FadeOutUI(duration));
    }

    private IEnumerator FadeInText(string paragraph)
    {

        foreach (char c in paragraph)
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
        ReadyUI.SetActive(true);
        prologueUI.SetActive(false);
    }

}
