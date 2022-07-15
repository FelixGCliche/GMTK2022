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
    private Color hiddenFaderColor;

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
        hiddenFaderColor = faderColor;
        hiddenFaderColor.a = 0;
        fader.color = hiddenFaderColor;
        LoadingScreen.SetActive(false);
    }

    public void FadeInLoadingScreen(float nbSecForFade)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(fader.DOColor(faderColor, nbSecForFade/2).OnComplete(ShowLoadingScreen));
        sequence.Append(fader.DOColor(hiddenFaderColor, nbSecForFade/2));
        sequence.Play();
    }

    private void ShowLoadingScreen()
    {
        LoadingScreen.SetActive(true);
    }

    public void FadeOutLoadingScreen(float nbSecForFade)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(fader.DOColor(faderColor, nbSecForFade/2).OnComplete(HideLoadingScreen));
        sequence.Append(fader.DOColor(hiddenFaderColor, nbSecForFade/2));
        sequence.Play(); 
    }

    private void HideLoadingScreen()
    {
        LoadingScreen.SetActive(false);
    }



}
