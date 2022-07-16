using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosingScreen : MonoBehaviour
{
    [SerializeField] private Canvas LoseScreenCanvas;

    private void OnEnable()
    {
        TurnManager.Instance.EndGame += ShowLosingScreen;
    }

    private void OnDisable()
    {
        TurnManager.Instance.EndGame -= ShowLosingScreen;
    }

    public void ShowLosingScreen()
    {
        LoseScreenCanvas.gameObject.SetActive(true);
    }

    public void Replay()
    {
        LoadingManager.Instance.ChangeScene("GameScene");
    }

    public void ReturnToMainMenu()
    {
        LoadingManager.Instance.ChangeScene("MainMenu");
    }
}
