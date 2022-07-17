using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class DroppingZone : MonoBehaviour
{
    [SerializeField] private GameObject towerHeightEvaluator;
    [SerializeField] private GameObject towerHeightBase;
    [SerializeField] private float zOffset;

    [Header("Freemode")]
    [SerializeField] private bool IsFreemode = false;
    [SerializeField] private float verticalMovementSpeed;

    private GameCamera gameCamera;

    private float baseY;

    private bool verticalMovementUp = false;
    private bool verticalMovementDown = false;

    private void  Awake()
    {
        baseY = transform.position.y;
    }

    private void Update()
    {
        if (verticalMovementUp)
        {
            float targetYPosition = transform.position.y + verticalMovementSpeed * Time.deltaTime;
            targetYPosition = Mathf.Clamp(targetYPosition, baseY, baseY + 15f);
            transform.position = new Vector3(transform.position.x, targetYPosition, transform.position.z);
        }
        if (verticalMovementDown)
        {
            float targetYPosition = transform.position.y - verticalMovementSpeed * Time.deltaTime;
            targetYPosition = Mathf.Clamp(targetYPosition, baseY, baseY + 15f);
            transform.position = new Vector3(transform.position.x, targetYPosition, transform.position.z);
        }
    }

    private void OnEnable()
    {
        DiceGameManager.Instance.EndTurn += AdjustScoreWithHeight;
        DiceGameManager.Instance.NextTurn += AdjustHeight;
        DiceGameManager.Instance.AdjustHeight += AdjustHeight;
        DiceGameManager.Instance.RestartGame += ResetHeight;
    }

    private void OnDisable()
    {
        DiceGameManager.Instance.EndTurn -= AdjustScoreWithHeight;
        DiceGameManager.Instance.NextTurn -= AdjustHeight;
        DiceGameManager.Instance.AdjustHeight -= AdjustHeight;
        DiceGameManager.Instance.RestartGame -= ResetHeight;
    }

    private void AdjustHeight()
    {
        if (!IsFreemode)
        {
            RaycastHit hit;
            if(Physics.BoxCast(towerHeightEvaluator.transform.position, new Vector3(1.5f,1.5f,1.5f), Vector3.down, out hit, towerHeightEvaluator.transform.rotation,  Mathf.Infinity, LayerMask.GetMask("Draggable")))
            {
                transform.DOMoveY(hit.point.y + zOffset, 0.5f);
            }
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

    public void GoUp(InputAction.CallbackContext ctx)
    {
        if (IsFreemode)
        {
            if (ctx.performed)
                verticalMovementUp = true;
            if (ctx.canceled)
                verticalMovementUp = false;
        }
    }

    public void GoDown(InputAction.CallbackContext ctx)
    {
        if (IsFreemode)
        {
            if (ctx.performed)
                verticalMovementDown = true;
            if (ctx.canceled)
                verticalMovementDown = false;
        }
    }
}
