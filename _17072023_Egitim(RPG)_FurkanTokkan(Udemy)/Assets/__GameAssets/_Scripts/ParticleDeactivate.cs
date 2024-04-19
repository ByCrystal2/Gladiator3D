using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDeactivate : MonoBehaviour
{
    private ParticleSystem particleSystem;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        // Particle System çalýþýyor mu diye kontrol ediyoruz.
        if (particleSystem && particleSystem.isStopped)
        {
            // Particle System'un duration'ý dolduðunda objeyi devre dýþý býrak.
            gameObject.SetActive(false);
        }
    }
}
