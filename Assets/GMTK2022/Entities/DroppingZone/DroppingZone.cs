using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DroppingZone : MonoBehaviour
{
    [SerializeField] private GameObject droppingZoneHeightEvaluator;
    [SerializeField] private float zOffset;

    private float baseY;

    private void  Awake()
    {
        baseY = transform.position.y;
    }

    private void OnEnable()
    {
        DiceGameManager.Instance.NextTurn += AdjustHeight;
        DiceGameManager.Instance.StartGame += AdjustHeight;
    }

    private void OnDisable()
    {
        DiceGameManager.Instance.NextTurn -= AdjustHeight;
        DiceGameManager.Instance.StartGame -= AdjustHeight;
    }

    public void AdjustHeight()
    {
        RaycastHit hit;
        if(Physics.BoxCast(droppingZoneHeightEvaluator.transform.position, new Vector3(1.5f,1.5f,1.5f), Vector3.down, out hit, droppingZoneHeightEvaluator.transform.rotation,  Mathf.Infinity, LayerMask.GetMask("Draggable")))
        {
            transform.DOMoveY(hit.point.y + zOffset, 0.5f);
        }
        else
        {
            transform.DOMoveY(baseY, 0.5f);
        }
    }
}
