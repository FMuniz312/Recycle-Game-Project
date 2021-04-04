using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MunizCodeKit.Systems;

public class MouseController : MonoBehaviour
{
    bool isHoldingTrash;
    Transform trashTransform;
    private void Update()
    {
        if (!isHoldingTrash)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D info = MouseHelper.MouseRayCast();
                if (info)
                {
                    if(info.collider.CompareTag("trash"))trashTransform = info.transform;
                    isHoldingTrash = true;
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                trashTransform = null;
                isHoldingTrash = false;
            }
                if(isHoldingTrash)trashTransform.position = MouseHelper.MouseWorldPos();
        }
        
    }
}
