using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadingManager : SingletonPersistant<LoadingManager>
{
    private Scene currentScene;

    public void ChangeScene(string sceneToLoad)
    {
        StartCoroutine(LoadSceneAsync(sceneToLoad));
    }
    
    private IEnumerator LoadSceneAsync(string newSceneToLoad)
    {
        Scene oldScene = SceneManager.GetActiveScene();
        //Load loading screen
        AsyncOperation asyncTask = SceneManager.LoadSceneAsync("LoadingScreen", LoadSceneMode.Additive);

        while (!asyncTask.isDone)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("LoadingScreen"));
        //Unload old scene
        asyncTask = SceneManager.UnloadSceneAsync(oldScene);

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

        //Unload loadingScreen
        asyncTask = SceneManager.UnloadSceneAsync("LoadingScreen");

        while (!asyncTask.isDone)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(newSceneToLoad));
        yield break;
    }
}
