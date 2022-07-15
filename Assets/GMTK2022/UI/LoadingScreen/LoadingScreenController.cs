using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class LoadingScreenController : MonoBehaviour
{
    [SerializeField] private Image fader;
    [SerializeField] private GameObject LoadingScreen;

    private Color faderColor;

    private void OnEnable()
    {
        LoadingManager.Instance.FadeInLoadingScreen += FadeInLoadingScreen;
        LoadingManager.Instance.FadeOutLoadingScreen += FadeOutLoadingScreen;
    }

    private void OnDisable()
    {
        LoadingManager.Instance.FadeInLoadingScreen -= FadeInLoadingScreen;
        LoadingManager.Instance.FadeOutLoadingScreen -= FadeOutLoadingScreen;
    }

    private void Start()
    {
        faderColor = fader.color;
        fader.color = new Color(0,0,0,0);
        LoadingScreen.SetActive(false);
    }

    public void FadeInLoadingScreen(float nbSecForFade)
    {
        fader.DOColor(faderColor, nbSecForFade).OnComplete(OnFadeInLoadingComplete);
    }

    private void OnFadeInLoadingComplete()
    {
        LoadingScreen.SetActive(true);
    }

    public void FadeOutLoadingScreen(float nbSecForFade)
    {
        LoadingScreen.SetActive(false);
        fader.DOColor(new Color(0,0,0,0), nbSecForFade).OnComplete(OnFadeOutLoadingComplete);  
    }

    private void OnFadeOutLoadingComplete()
    {
        
    }



}
