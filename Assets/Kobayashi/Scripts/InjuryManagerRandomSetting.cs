using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InjuryManagerRandomSetting : MonoBehaviour
{
    [Header("Details")]
    [SerializeField] private GameObject[] bodyPartsObject;
    [Header("ƒL[ƒ[ƒh")]
    [Header("•”ˆÊ"), SerializeField] private Text _partText;
    [Header("Çó"),SerializeField] private Text _treatmentText;
    [Header("‘¬‚³"), SerializeField] private Text _speedText;
    [Header("")]
    [Header("“ª")]
    [Header("‘Å–o"),SerializeField] private GameObject _headbruise;
    [Header("C‚è"),SerializeField] private GameObject _headabrasion;
    [Header("‰Eè")]
    [Header("‘Å–o"),SerializeField] private GameObject _righthandbruise;
    [Header("C‚è"), SerializeField] private GameObject _righthandabrasion;
    [Header("¶è")]
    [Header("‘Å–o"), SerializeField] private GameObject _lefthandbruise;
    [Header("C‚è"), SerializeField] private GameObject _lefthandabrasion;
    [Header("‰E‘«")]
    [Header("‘Å–o"), SerializeField] private GameObject _rightlegbruise;
    [Header("C‚è"), SerializeField] private GameObject _rightlegabrasion;
    [Header("¶‘«")]
    [Header("‘Å–o"), SerializeField] private GameObject _leftlegbruise;
    [Header("C‚è"),SerializeField] private GameObject _leftlegabrasion;
    private List<BodyPart> bodyParts;
    private bool Head;
    private bool RightHand;
    private bool LeftHand;
    private bool RightLeg;
    private bool LeftLeg;

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
                _partText.text = "“ª•”‚ğ";
                bodyParts[injuredIndex1].isInjured = true;
                _headbruise.gameObject.SetActive(true);
                _headabrasion.gameObject.SetActive(true);
                break;
            case 1:
                _partText.text = "‰Eè‚ğ";
                bodyParts[injuredIndex1].isInjured = true;
                _righthandbruise.gameObject.SetActive(true);
                _righthandabrasion.gameObject.SetActive(true);
                break;
            case 2:
                _partText.text = "¶è‚ğ";
                bodyParts[injuredIndex1].isInjured = true;
                _lefthandbruise.gameObject.SetActive(true);
                _lefthandabrasion.gameObject.SetActive (true);
                break;
            case 3:
                _partText.text = "‰E‘«‚ğ";
                bodyParts[injuredIndex1].isInjured = true;
                _rightlegbruise.gameObject.SetActive(true);
                _rightlegabrasion.gameObject.SetActive(true);
                break;
            case 4:
                _partText.text = "¶‘«‚ğ";
                bodyParts[injuredIndex1].isInjured = true;
                _leftlegbruise.gameObject.SetActive(true);
                _leftlegabrasion.gameObject.SetActive(true);
                break;
        }
        switch (injuredIndex2)
        {
            case 0:
                _treatmentText.text = "‚Ğ‚è‚Ğ‚è";
                _headbruise.gameObject.SetActive(false);
                _righthandbruise.gameObject.SetActive(false);
                _lefthandbruise.gameObject.SetActive(false);
                _rightlegbruise.gameObject.SetActive(false);
                _leftlegbruise.gameObject.SetActive(false);
                break;
            case 1:
                _treatmentText.text = "ƒKƒ“ƒKƒ“";
                _headabrasion.gameObject.SetActive(false);
                _righthandabrasion.gameObject.SetActive(false);
                _lefthandabrasion.gameObject.SetActive(false);
                _rightlegabrasion.gameObject.SetActive(false);
                _leftlegabrasion.gameObject.SetActive(false);
                break;
        }
        switch (injuredIndex3)
        {
            case 0:
            _speedText.text = "‚â‚³‚µ‚­";
            break;
            case 1:
            _speedText.text = "‚³‚³‚Á‚Æ";
            break;
        }
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
