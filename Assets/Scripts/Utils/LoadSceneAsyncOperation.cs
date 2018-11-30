using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAsyncOperation : MonoBehaviour
{
    public static LoadSceneAsyncOperation instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public IEnumerator LoadAsync(string sceneName)
    {
        yield return null;

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName);
        loadOperation.allowSceneActivation = false;

        while (!loadOperation.isDone)
        {
            if (loadOperation.progress >= 0.9f)
            {
                StartCoroutine(UnLoadAsyncThenFinishLoading(loadOperation));
            }
            yield return null;
        }
    }

    public IEnumerator UnLoadAsyncThenFinishLoading(AsyncOperation loadOperation)
    {
        yield return null;

        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);

        unloadOperation.allowSceneActivation = false;

        while (!unloadOperation.isDone)
        {
            if (unloadOperation.progress >= 0.9f)
            {
                loadOperation.allowSceneActivation = true;
                unloadOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}