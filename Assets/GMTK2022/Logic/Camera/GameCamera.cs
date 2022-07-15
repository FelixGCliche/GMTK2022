using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [SerializeField] private GameObject focusPoint;
    [SerializeField] private float focusPointZOffset;

    private void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.y = focusPoint.transform.position.y + focusPointZOffset;
        transform.position = newPosition;
    }
}
