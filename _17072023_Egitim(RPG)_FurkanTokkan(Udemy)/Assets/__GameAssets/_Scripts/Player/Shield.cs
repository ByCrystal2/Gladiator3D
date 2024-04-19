using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    PlayerHealth ph;
    private void Awake()
    {
        ph = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void OnEnable()
    {
        ph.Shielded = true;
    }

    private void OnDisable()
    {
        ph.Shielded = false;
    }
}
