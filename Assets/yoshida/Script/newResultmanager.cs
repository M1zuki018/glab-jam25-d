using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class newResultManager : MonoBehaviour
{
    public Text displayText;             
    public Text rankText;                

    [TextArea(3, 6)]
    public string[] textsForA;
    public string[] textsForB;
    public string[] textsForC;
    public string[] textsForD;

    [TextArea(1, 3)]
    public string[] rankTextsForA;   
    public string[] rankTextsForB;   
    public string[] rankTextsForC;   
    public string[] rankTextsForD;   

    [SerializeField] float typingSpeed = 0.05f;   
    [SerializeField] float fadeDuration = 1.0f;    
    [SerializeField] float waitAfterEachText = 0.5f;  

    void Start()
    {
        if (displayText == null || rankText == null)
        {
            Debug.LogError("displayTextかrankTextがセットされていません！");
            return;
        }

        displayText.text = "";
        rankText.text = "";
        SetTextAlpha(rankText, 0f); 

        int score = ScoreManager.Instance.Score;
        string rank = GetRank(score);

        StartCoroutine(TypeEndingThenFadeInRankTexts(rank));
    }

    IEnumerator TypeEndingThenFadeInRankTexts(string rank)
    {
        
        string[] endingTexts = GetTextsByRank(rank);
        foreach (var text in endingTexts)
        {
            displayText.text = "";
            foreach (char c in text)
            {
                displayText.text += c;
                yield return new WaitForSeconds(typingSpeed);
            }
            yield return new WaitForSeconds(waitAfterEachText);
        }

     
        string[] rankTexts = GetRankTextsByRank(rank);
        for (int i = 0; i < rankTexts.Length; i++)
        {
            rankText.text = rankTexts[i];
            yield return StartCoroutine(FadeInText(rankText, fadeDuration));
            yield return new WaitForSeconds(waitAfterEachText);
        }
    }

    IEnumerator FadeInText(Text text, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            SetTextAlpha(text, Mathf.Clamp01(elapsed / duration));
            yield return null;
        }
        SetTextAlpha(text, 1f);
    }

    void SetTextAlpha(Text text, float alpha)
    {
        Color c = text.color;
        c.a = alpha;
        text.color = c;
    }

    string[] GetTextsByRank(string rank)
    {
        switch (rank)
        {
            case "A": return textsForA;
            case "B": return textsForB;
            case "C": return textsForC;
            case "D": return textsForD;
            default: return new string[0];
        }
    }

    string[] GetRankTextsByRank(string rank)
    {
        switch (rank)
        {
            case "A": return rankTextsForA;
            case "B": return rankTextsForB;
            case "C": return rankTextsForC;
            case "D": return rankTextsForD;
            default: return new string[0];
        }
    }

    string GetRank(int score)
    {
        switch (score)
        {
            case 3: return "A";
            case 2: return "B";
            case 1: return "C";
            default: return "D";
        }
    }
}
