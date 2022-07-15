using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuPage;
    [SerializeField] private GameObject OptionsMenuPage;
    [SerializeField] private GameObject CreditsMenuPage;

    public void StartGame()
    {
        LoadingManager.Instance.ChangeScene("GameScene");
    }

    public void GoBackToMainMenu()
    {
        OptionsMenuPage.SetActive(false);
        CreditsMenuPage.SetActive(false);

        MainMenuPage.SetActive(true);
    }

    public void GoToOptionsMenu()
    {
        MainMenuPage.SetActive(false);

        OptionsMenuPage.SetActive(true);
    }

    public void GoToCreditsMenu()
    {
        MainMenuPage.SetActive(false);

        CreditsMenuPage.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
