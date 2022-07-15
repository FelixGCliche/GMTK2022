using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DroppingZone : MonoBehaviour
{
    [SerializeField] private GameObject droppingZoneHeightEvaluator;
    [SerializeField] private float zOffset;

    private void OnEnable()
    {
        TurnManager.Instance.NextTurn += AdjustHeight;
    }

    private void OnDisable()
    {
        TurnManager.Instance.NextTurn -= AdjustHeight;
    }

    public void AdjustHeight()
    {
        RaycastHit hit;
        if(Physics.BoxCast(droppingZoneHeightEvaluator.transform.position, new Vector3(1.5f,1.5f,1.5f), Vector3.down, out hit, droppingZoneHeightEvaluator.transform.rotation,  Mathf.Infinity, LayerMask.GetMask("Draggable")))
        {
            Debug.Log("hit");
            transform.DOMoveY(hit.point.y + zOffset, 0.5f);
        }
    }
}
