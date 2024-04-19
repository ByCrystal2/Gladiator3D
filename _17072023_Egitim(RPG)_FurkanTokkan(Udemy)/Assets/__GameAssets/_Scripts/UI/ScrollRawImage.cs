using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRawImage : MonoBehaviour
{
    public float horizontalSpeed;
    public float verticalSpeed;

    RawImage myRaw;

    private void Start()
    {
            myRaw = GetComponent<RawImage>();
    }

    private void Update()
    {
        Rect currentUv = myRaw.uvRect;
        currentUv.x -= Time.deltaTime * horizontalSpeed;
        currentUv.y -= Time.deltaTime * verticalSpeed;

        if (currentUv.x <= -1f || currentUv.x >= 1f)
        {
            currentUv.x = 0f;
        }
        if (currentUv.y <= -1f || currentUv.y >= 1f)
        {
            currentUv.y = 0f;
        }

        myRaw.uvRect = currentUv;
    }
}
