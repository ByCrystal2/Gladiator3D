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
        // Particle System �al���yor mu diye kontrol ediyoruz.
        if (particleSystem && particleSystem.isStopped)
        {
            // Particle System'un duration'� doldu�unda objeyi devre d��� b�rak.
            gameObject.SetActive(false);
        }
    }
}
