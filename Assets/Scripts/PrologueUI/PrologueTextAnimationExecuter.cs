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

    private int currentStringIndex;
    private bool isTyping;
    private bool skipRequest;

    void Start()
    {
        prologueText.text = "";
        currentStringIndex = 0;
        isTyping = false;
        skipRequest = false;

        prologueMessage = new string[]
        {
            "Bonjour. Tu es la? Je dois te parler..\n" +
            "Je ne sais pas ce que je ferais sans toi.\n\n",
            "Moi non plus. Mais on se l'etait promis.\n" +
            "Tu t'en souviens?\n\n",
            "Oui, mais je ne peux pas garder cette promesse.\n" +
            "Je suis lache.\n" +
            "...Pardonne-moi."
        };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            StartCoroutine(RunPrologue());
    }

    private IEnumerator RunPrologue()
    {
        for(int i = 0; i < prologueMessage.Length; i++)
        {
            yield return StartCoroutine(FadeInText(prologueMessage[i]));

            yield return new WaitForSeconds(paragraphPause);
        }
    }

    private IEnumerator FadeInText(string paragraph)
    {
        isTyping = true;

        foreach(char c in paragraph)
        {
            prologueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
    }

}
