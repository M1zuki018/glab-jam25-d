using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemTrigger : MonoBehaviour
{
    public void PlayParticleAnimation()
    {
        gameObject.GetComponent<ParticleSystem>().Play();
    }
}
