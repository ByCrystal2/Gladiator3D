using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class SpawnPoint : MonoBehaviour
{
    public float walkRadius = 5f;
    public int walkPointCount = 5;
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, walkRadius);
    }
}
