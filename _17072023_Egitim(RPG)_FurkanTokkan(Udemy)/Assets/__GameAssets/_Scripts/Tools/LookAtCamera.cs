using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookAtCamera : MonoBehaviour
{
    
    
    private void LateUpdate()
    {
        gameObject.transform.LookAt(Camera.main.transform);
    }
}
