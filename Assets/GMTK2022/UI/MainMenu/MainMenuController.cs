using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject OptionsMenu;
    [SerializeField] private GameObject CreditsMenu;

    public void StartGame()
    {
        LoadingManager.Instance.ChangeScene("GameScene");
    }

    public void GoBackToMainMenu()
    {
        OptionsMenu.SetActive(false);
        CreditsMenu.SetActive(false);

        MainMenu.SetActive(true);
    }

    public void GoToOptionsMenu()
    {
        MainMenu.SetActive(false);

        OptionsMenu.SetActive(true);
    }

    public void GoToCreditsMenu()
    {
        MainMenu.SetActive(false);

        CreditsMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
