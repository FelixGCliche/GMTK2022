using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraBoom : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;

    private bool rotatingRight = false;
    private bool rotatingLeft = false;

    private void Update()
    {
        if (rotatingRight)
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, -rotationSpeed * Time.deltaTime, 0));
        if (rotatingLeft)
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, rotationSpeed * Time.deltaTime, 0));
    }

    public void RotateRight(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            rotatingRight = true;
        if (ctx.canceled)
            rotatingRight = false;
    }

    public void RotateLeft(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            rotatingLeft = true;
        if (ctx.canceled)
            rotatingLeft = false;
    }
}
