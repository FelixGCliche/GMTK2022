using System;
using TMPro;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private Canvas endScreenCanvas;
    [SerializeField] private TextMeshProUGUI scoreText;

    private int finalScore;

    private void OnEnable()
    {
        DiceGameManager.Instance.GameLost += ShowEndScreen;
    }

    private void OnDisable()
    {
        DiceGameManager.Instance.GameLost -= ShowEndScreen;
    }

    public void ShowEndScreen(int finalScore)
    {
        scoreText.SetText(DiceGameManager.Instance.GetScore().ToString());
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
