using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPage;
    [SerializeField] private GameObject settingsMenuPage;

    public void ShowPauseMenu()
    {
        pauseMenuPage.SetActive(true);
        settingsMenuPage.SetActive(false);
    }

    public void ClosePauseMenu()
    {
        pauseMenuPage.SetActive(false);
        settingsMenuPage.SetActive(false);
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

    public void ShowSettingsMenu()
    {
        pauseMenuPage.SetActive(false);
        settingsMenuPage.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        LoadingManager.Instance.ChangeScene("MainMenu");
    }
}
