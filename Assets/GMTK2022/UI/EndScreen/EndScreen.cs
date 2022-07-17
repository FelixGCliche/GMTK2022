using System;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    private Canvas endScreenCanvas;

    private void Awake()
    {
        endScreenCanvas = GetComponent<Canvas>();
    }

    private void OnEnable()
    {
        DiceGameManager.Instance.GameLost += ShowEndScreen;
    }

    private void OnDisable()
    {
        DiceGameManager.Instance.GameLost -= ShowEndScreen;
    }

    public void ShowEndScreen()
    {
        endScreenCanvas.gameObject.SetActive(true);
    }

    public void Replay()
    {
        DiceGameManager.Instance.GameRestarted();
        endScreenCanvas.gameObject.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        LoadingManager.Instance.ChangeScene("MainMenu");
    }
}
