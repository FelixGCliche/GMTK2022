using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadingManager : SingletonPersistant<LoadingManager>
{
    public delegate void FadeInLoadingScreenDelegate(float nbSec);
    public event FadeInLoadingScreenDelegate FadeInLoadingScreen;
    public delegate void FadeOutLoadingScreenDelegate(float nbSec);
    public event FadeInLoadingScreenDelegate FadeOutLoadingScreen;

    private float nbSecForFade = 2f;

    private Scene currentScene;

    public void ChangeScene(string sceneToLoad)
    {
        StartCoroutine(LoadSceneAsync(sceneToLoad));
    }
    
    private IEnumerator LoadSceneAsync(string newSceneToLoad)
    {
        Scene oldScene = SceneManager.GetActiveScene();

        //Load LoadingScreen if not yet loaded
        if(!SceneManager.GetSceneByName("LoadingScreen").IsValid())
        {
            SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Additive);
            yield return null;
        }

        //FadeIn loading screen
        FadeInLoadingScreen(nbSecForFade);
        yield return new WaitForSeconds(nbSecForFade);

        //Unload old scene
        AsyncOperation asyncTask = SceneManager.UnloadSceneAsync(oldScene);

        while (!asyncTask.isDone)
        {
            yield return null;
        }

        //Load new scene
        asyncTask = SceneManager.LoadSceneAsync(newSceneToLoad, LoadSceneMode.Additive);

        while (!asyncTask.isDone)
        {
            yield return null;
        }

        //FadeOut loadingScreen
        FadeOutLoadingScreen(nbSecForFade);
        yield return new WaitForSeconds(nbSecForFade);

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(newSceneToLoad));
        yield break;
    }
}
