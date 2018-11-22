using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAsyncOperation : MonoBehaviour
{
    public IEnumerator LoadAsync(string sceneName)
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}