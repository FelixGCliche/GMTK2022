//Inspired by https://www.youtube.com/watch?v=Ova7l0UB26U

using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject();
                obj.name = typeof(T).Name;
                obj.hideFlags = HideFlags.HideAndDontSave;
                _instance = obj.AddComponent<T>();
            }
            return _instance;
        }
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}

public class SingletonPersistant<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                if(!SceneManager.GetSceneByName("SingletonPersistant").IsValid())
                {
                    SceneManager.CreateScene("SingletonPersistant");
                }
                Scene activeScene = SceneManager.GetActiveScene();
                SceneManager.SetActiveScene(SceneManager.GetSceneByName("SingletonPersistant"));
                GameObject obj = new GameObject();
                obj.name = typeof(T).Name;
                obj.hideFlags = HideFlags.HideAndDontSave;
                _instance = obj.AddComponent<T>();
                SceneManager.SetActiveScene(activeScene);
            }
            return _instance;
        }
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}
