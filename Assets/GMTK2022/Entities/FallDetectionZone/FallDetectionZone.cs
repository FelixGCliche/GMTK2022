using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDetectionZone : MonoBehaviour
{
    [SerializeField] private bool IsFreemode = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Dice"))
        {
            if (!IsFreemode)
                DiceGameManager.Instance.LostGame();
            else
                DiceGameManager.Instance.AdjustCameraHeight();
        }
    }
}
