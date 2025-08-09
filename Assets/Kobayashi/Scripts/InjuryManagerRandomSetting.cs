using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InjuryManagerRandomSetting : MonoBehaviour
{
    [Header("Details")]
    [SerializeField] private GameObject[] bodyPartsObject;
    [Header("�L�[���[�h")]
    [Header("����"), SerializeField] private Text _partText;
    [Header("�Ǐ�"),SerializeField] private Text _treatmentText;
    [Header("����"), SerializeField] private Text _speedText;
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
        List<int> treatmentdices = new List<int> { 0,1 };
        List<int> speeddices = new List<int> { 0,1 };
        ShuffleParts(indices);
        ShuffleParts(treatmentdices);
        ShuffleParts(speeddices);

        int injuredIndex1 = indices[0]; // Assign the first 2 parts to injured
        int injuredIndex2 = treatmentdices[0];
        int injuredIndex3 = speeddices[0];

        switch (injuredIndex1)
        {
            case 0:
                _partText.text = "������";
            break;
            case 1:
                _partText.text = "�E���";
            break;
            case 2:
                _partText.text = "�����";
            break;
            case 3:
                _partText.text = "�E����";
            break;
            case 4:
                _partText.text = "������";
            break;
        }
        switch (injuredIndex2)
        {
            case 0:
                _treatmentText.text = "�Ђ�Ђ�";
                break;
            case 1:
                _treatmentText.text = "�K���K��";
                break;
        }
        switch (injuredIndex3)
        {
            case 0:
            _speedText.text = "�₳����";
            break;
            case 1:
            _speedText.text = "��������";
            break;
        }
        //bodyParts[injuredIndex1].isInjured = true; // Make those parts injured
        //bodyParts[injuredIndex2].isInjured = true;
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

    public void TextSetting()
    {

    }
}
