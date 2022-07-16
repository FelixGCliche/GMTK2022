using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosingScreen : MonoBehaviour
{
    [SerializeField] private Canvas LoseScreenCanvas;

    private void OnEnable()
    {
        DiceGameManager.Instance.GameLost += ShowLosingScreen;
    }

    private void OnDisable()
    {
        DiceGameManager.Instance.GameLost -= ShowLosingScreen;
    }

    public void ShowLosingScreen()
    {
        LoseScreenCanvas.gameObject.SetActive(true);
    }

    public void Replay()
    {
        DiceGameManager.Instance.GameRestarted();
        LoseScreenCanvas.gameObject.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        LoadingManager.Instance.ChangeScene("MainMenu");
    }
}
