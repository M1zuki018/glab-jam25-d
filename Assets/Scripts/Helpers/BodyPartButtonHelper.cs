using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartButtonHelper : MonoBehaviour
{
    public InjuryManager injuryManager;

    public void OnClick()
    {
        injuryManager.GuessInjuryState(gameObject);
    }
}
