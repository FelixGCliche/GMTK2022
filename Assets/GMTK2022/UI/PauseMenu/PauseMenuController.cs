using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPage;
    [SerializeField] private GameObject optionMenuPage;

    public void ShowPauseMenu()
    {
        pauseMenuPage.SetActive(true);
        optionMenuPage.SetActive(false);
    }

    public void ClosePauseMenu()
    {
        pauseMenuPage.SetActive(false);
        optionMenuPage.SetActive(false);
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

    public void ShowOptionMenu()
    {
        pauseMenuPage.SetActive(false);
        optionMenuPage.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        LoadingManager.Instance.ChangeScene("MainMenu");
    }
}
