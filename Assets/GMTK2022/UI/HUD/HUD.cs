using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponentInChildren<Canvas>();
    }

    private void OnEnable()
    {
        DiceGameManager.Instance.EndGame += HideHUD;
        DiceGameManager.Instance.StartGame += ShowHUD;
    }

    private void OnDisable()
    {
        DiceGameManager.Instance.EndGame -= HideHUD;
        DiceGameManager.Instance.StartGame -= ShowHUD;
    }

    private void HideHUD()
    {
        canvas.gameObject.SetActive(false);
    }

    private void ShowHUD()
    {
        canvas.gameObject.SetActive(true);
    }
}
