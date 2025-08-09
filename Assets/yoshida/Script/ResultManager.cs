using UnityEngine;

public class SubResultManager : MonoBehaviour
{
    public GameObject TextA;
    public GameObject TextB;
    public GameObject TextC;
    public GameObject TextD;
    public GameObject ScoreA;
    public GameObject ScoreB;
    public GameObject ScoreC;
    public GameObject ScoreD;

    void Start()
    {
        TextA.SetActive(false);
        TextB.SetActive(false);
        TextC.SetActive(false);
        TextD.SetActive(false);
        ScoreA.SetActive(false);
        ScoreB.SetActive(false);
        ScoreC.SetActive(false);
        ScoreD.SetActive(false);
    }

    public void ShowRating(string rank)
    {
        switch (rank)
        {
            case "A":
                TextA.SetActive(true);
                ScoreA.SetActive(true);
                break;
            case "B":
                TextB.SetActive(true);
                ScoreB.SetActive(true);
                break;
            case "C":
                TextC.SetActive(true);
                ScoreC.SetActive(true);
                break;
            case "D":
                TextD.SetActive(true);
                ScoreD.SetActive(true);
                break;
        }
    }
}
