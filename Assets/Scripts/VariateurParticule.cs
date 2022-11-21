using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariateurParticule : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public float index;

    private void Update()
    {
        var particleSystemMain = particleSystem.main;
        particleSystemMain.startLifetime = index;
    }
}
