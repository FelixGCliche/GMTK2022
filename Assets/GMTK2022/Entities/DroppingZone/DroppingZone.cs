using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DroppingZone : MonoBehaviour
{
    [SerializeField] private GameObject towerHeightEvaluator;
    [SerializeField] private GameObject towerHeightBase;
    [SerializeField] private float zOffset;

    private GameCamera gameCamera;

    private float baseY;

    private void  Awake()
    {
        baseY = transform.position.y;
    }

    private void OnEnable()
    {
        DiceGameManager.Instance.EndTurn += AdjustScoreWithHeight;
        DiceGameManager.Instance.NextTurn += AdjustHeight;
        DiceGameManager.Instance.RestartGame += ResetHeight;
    }

    private void OnDisable()
    {
        DiceGameManager.Instance.EndTurn -= AdjustScoreWithHeight;
        DiceGameManager.Instance.NextTurn -= AdjustHeight;
        DiceGameManager.Instance.RestartGame -= ResetHeight;
    }

    private void AdjustHeight()
    {
        RaycastHit hit;
        if(Physics.BoxCast(towerHeightEvaluator.transform.position, new Vector3(1.5f,1.5f,1.5f), Vector3.down, out hit, towerHeightEvaluator.transform.rotation,  Mathf.Infinity, LayerMask.GetMask("Draggable")))
        {
            transform.DOMoveY(hit.point.y + zOffset, 0.5f);
        }
    }

    private void AdjustScoreWithHeight()
    {
        float score = GetTowerCurrentHeight() * 200;
        DiceGameManager.Instance.SetScore((int)score);

    }

    private float GetTowerCurrentHeight()
    {
        RaycastHit hit;
        if(Physics.BoxCast(towerHeightEvaluator.transform.position, new Vector3(1.5f,1.5f,1.5f), Vector3.down, out hit, towerHeightEvaluator.transform.rotation,  Mathf.Infinity, LayerMask.GetMask("Draggable")))
        {
            return hit.point.y - towerHeightBase.transform.position.y;
        }
        else
        {
            return 0;
        }
    }

    private void ResetHeight()
    {
        transform.DOMoveY(baseY, 0.5f).OnComplete(DiceGameManager.Instance.GameStarted);
    }
}
