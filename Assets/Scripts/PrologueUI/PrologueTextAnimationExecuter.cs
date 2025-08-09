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
            "�����́A�d���������Ђ�����ƏW���g�����̐f�Ï��h�B\n" +
            "�ɂ݂��B���ċ�����q���A�{���̂��Ƃ�b���Ă���Ȃ��q���A��������B\n" +
            "�ŏ��݂͂�ȁA�S�Ɍ��������Ă���Ă���B\n\n" ,

            "�l�Ԃ���͋������ނ炪�A���͒N�����₵����ŁA\n" +
            "�N�����D�������Ƃ��A\n" +
            "���͒m���Ă���B\n\n",

            "�C�Â��΁A���̓����ꂽ�u�d�������̗F�B�v�Ƃ������́A\n" +
            "���������A������Ȃ��Ȃ�B"
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
