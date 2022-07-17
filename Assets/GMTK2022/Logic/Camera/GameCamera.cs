using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameCamera : MonoBehaviour
{
    [SerializeField] private GameObject focusPoint;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    [SerializeField] private float zoomRate;

    private bool zoomingIn = false;
    private bool zoomingOut = false;

    private void Update()
    {
        if(zoomingIn)
        {
            float distance = Vector3.Distance(transform.position, focusPoint.transform.position);
            if(distance  - (zoomRate * Time.deltaTime) > minDistance)
            {
                Vector3 newPosition = transform.position + ((focusPoint.transform.position - transform.position).normalized * (zoomRate * Time.deltaTime));
                transform.position = newPosition;
            }
        }
        if(zoomingOut)
        {
            float distance = Vector3.Distance(transform.position, focusPoint.transform.position);
            if(distance  - (zoomRate * Time.deltaTime) < maxDistance)
            {
                Vector3 newPosition = transform.position + ((focusPoint.transform.position - transform.position).normalized * -(zoomRate * Time.deltaTime));
                transform.position = newPosition;
            }
        }
    }
    
    public void ZoomIn(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            zoomingIn = true;
        if (ctx.canceled)
            zoomingIn= false;
    }

    public void ZoomOut(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            zoomingOut = true;
        if (ctx.canceled)
            zoomingOut= false;
    }
}
