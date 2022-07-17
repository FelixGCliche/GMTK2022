using System;
using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField]
    private Canvas endScreenCanvas;

    private int finalScore;
    private TextMeshProUGUI finalScoreText;

    private void Awake()
    {
        finalScoreText = endScreenCanvas.GetComponentInChildren<TextMeshProUGUI>();
    }

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
        endScreenCanvas.gameObject.SetActive(true);
        finalScoreText.SetText("Score : " + finalScore); 
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
