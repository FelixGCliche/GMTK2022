using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPage;
    [SerializeField] private GameObject settingsMenuPage;
    [SerializeField] private GameObject creditsMenuPage;

    public void StartGame()
    {
        LoadingManager.Instance.ChangeScene("GameScene");
    }

    public void StartFreemode()
    {
        LoadingManager.Instance.ChangeScene("FreemodeScene");
    }

    public void GoBackToMainMenu()
    {
        settingsMenuPage.SetActive(false);
        creditsMenuPage.SetActive(false);

        mainMenuPage.SetActive(true);
    }

    public void GoToSettingsMenu()
    {
        mainMenuPage.SetActive(false);

        settingsMenuPage.SetActive(true);
    }

    public void GoToCreditsMenu()
    {
        mainMenuPage.SetActive(false);

        creditsMenuPage.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
