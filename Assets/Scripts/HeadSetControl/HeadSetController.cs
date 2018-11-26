using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HeadSetController : MonoBehaviour
{
    public FadeInAndOut fader;

    void Update()
    {
        //Touch
        if (OVRInput.Get(OVRInput.Button.One))
        {
            SendMessage("SelectCountry");
        }
        //Back button
        if (OVRInput.Get(OVRInput.Button.Two))
        {
            StartCoroutine(LoadAsync("mainLevel"));

            //if (SceneManager.GetActiveScene().name != "mainLevel")
            //{
            //    fader.FadeOpaque();
            //    SceneManager.LoadSceneAsync("mainLevel");
            //}
        }
    }

    private IEnumerator LoadAsync(string sceneName)
    {
        if (SceneManager.GetActiveScene().name != sceneName)
        {
            fader.FadeOpaque();

            yield return new WaitForSeconds(2);

            SceneManager.LoadScene(sceneName);
        }
    }
}