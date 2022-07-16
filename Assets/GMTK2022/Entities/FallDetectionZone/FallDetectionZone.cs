using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDetectionZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Dice"))
        {
            DiceGameManager.Instance.LostGame();
        }
    }
}
