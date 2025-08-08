using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InjuryManager : MonoBehaviour
{
    [Header("Details")]
    [SerializeField] private GameObject[] bodyPartsObject;
    private List<BodyPart> bodyParts;

    private class BodyPart
    {
        public string name;
        public bool isInjured;
        public GameObject partObject;
        public bool isCorrectGuess;

        public BodyPart(string name, GameObject partObject)
        {
            this.name = name;
            this.partObject = partObject;
            this.isInjured = false;
            this.isCorrectGuess = false;
        }
    }

    private void Start()
    {
        bodyParts = new List<BodyPart>();

        foreach(var go in bodyPartsObject)
        {
            bodyParts.Add(new BodyPart(go.name, go));
        }

        SetBodyPartToInjured();
    }

    private void SetBodyPartToInjured()
    {
        foreach(var part in bodyParts)
        {
            part.isInjured = false;
            part.isCorrectGuess = false;
        }

        List<int> indices = new List<int> { 0, 1, 2, 3, 4 };

        ShuffleParts(indices);

        int injuredIndex1 = indices[0]; // Assign the first 2 parts to injured
        int injuredIndex2 = indices[1];

        bodyParts[injuredIndex1].isInjured = true; // Make those parts injured
        bodyParts[injuredIndex2].isInjured = true;
    }

    private void ShuffleParts<T>(List<T> shuffleList)
    {
        for(int i = shuffleList.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            T temp = shuffleList[i];
            shuffleList[i] = shuffleList[j];
            shuffleList[j] = temp;
        }
    }

    public void GuessInjuryState(GameObject guessedPartObject)
    {
        BodyPart guessedPart = bodyParts.Find(part => part.partObject == guessedPartObject);

        if (guessedPart == null)
            return;

        if(guessedPart.isInjured)
        {
            Debug.Log("Injured");
            guessedPart.isCorrectGuess = true;
        }
        else
        {
            Debug.Log("Not injured!");
        }
    }



}
