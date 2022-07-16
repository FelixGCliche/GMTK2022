using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoosingScreen : MonoBehaviour
{
    [SerializeField] private Canvas LooseScreenCanvas;

    private void OnEnable()
    {
        TurnManager.Instance.EndGame += ShowLoosingScreen;
    }

    private void OnDisable()
    {
        TurnManager.Instance.EndGame -= ShowLoosingScreen;
    }

    public void ShowLoosingScreen()
    {
        LooseScreenCanvas.gameObject.SetActive(true);
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
