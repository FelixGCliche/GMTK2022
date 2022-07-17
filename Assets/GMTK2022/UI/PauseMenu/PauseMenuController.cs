using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPage;
    [SerializeField] private GameObject settingsMenuPage;

    private float baseFixedDeltaTime;

    private void Awake()
    {
        baseFixedDeltaTime = Time.fixedDeltaTime;
    }

    public void ShowPauseMenu()
    {
        pauseMenuPage.SetActive(true);
        settingsMenuPage.SetActive(false);
        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0f;
        DiceGameManager.Instance.SetGamePause(true);
    }

    public void ClosePauseMenu()
    {
        pauseMenuPage.SetActive(false);
        settingsMenuPage.SetActive(false);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = baseFixedDeltaTime;
        DiceGameManager.Instance.SetGamePause(false);
    }

    public void TogglePauseMenu()
    {
        if(pauseMenuPage.activeSelf)
        {
            ClosePauseMenu();
        }
        else
        {
            ShowPauseMenu();
        }
    }

    public void Replay()
    {
        DiceGameManager.Instance.GameRestarted();
        ClosePauseMenu();
    }


    public void ShowSettingsMenu()
    {
        pauseMenuPage.SetActive(false);
        settingsMenuPage.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        DiceGameManager.Instance.GameEnded();
        Time.timeScale = 1f;
        Time.fixedDeltaTime = baseFixedDeltaTime;
        LoadingManager.Instance.ChangeScene("MainMenu");
    }
}
