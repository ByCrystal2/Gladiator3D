using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class MouseScript : MonoBehaviour
{
    public Texture2D cursorTextureNormal;
    public Texture2D cursorTextureEnemy;

    private CursorMode cursorMode = CursorMode.ForceSoftware;
    private Vector2 hotSpot = Vector2.zero;

    public GameObject mousePoint;
    void Start()
    {
        
    }

    
    void Update()
    {
        CursorChanger();

        if (Input.GetMouseButtonUp(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    Vector3 lastPos = hit.point;
                    lastPos.y = 0.35f;

                    Instantiate(mousePoint, lastPos, Quaternion.identity);                   
                }                
            }
        }
    }
    

    void CursorChanger()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Target") && !hit.collider.gameObject.GetComponentInParent<EnemyHealth>().isDead)
            {              

                Cursor.SetCursor(cursorTextureEnemy, hotSpot, cursorMode);
            }
            else
            {
                Cursor.SetCursor(cursorTextureNormal, hotSpot, cursorMode);
            }
        }
    }
}
