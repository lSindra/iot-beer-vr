using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAsyncOperation : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        FadeInAndOut fader = new FadeInAndOut();
        fader.FadeOpaque();
        StartCoroutine(LoadAsync(sceneName));
    }

    IEnumerator LoadAsync(string sceneName)
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