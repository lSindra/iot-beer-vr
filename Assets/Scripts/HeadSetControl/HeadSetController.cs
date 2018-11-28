using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HeadSetController : MonoBehaviour
{
    public FadeInAndOut fader;
    public GameObject playerCamera;

    private SmoothCamera smoothCamera;

    private void Start()
    {
        smoothCamera = playerCamera.GetComponent<SmoothCamera>();
    }
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.Two))
        {
            LoadScene();
        }
        if (OVRInput.Get(OVRInput.Touch.One))
        {
            RecenterPose();
        }
    }

    private void LoadScene()
    {
        StartCoroutine(LoadScene("mainLevel"));

        //if (SceneManager.GetActiveScene().name != "mainLevel")
        //{
        //    fader.FadeOpaque();
        //    SceneManager.LoadSceneAsync("mainLevel");
        //}
    }

    private void RecenterPose()
    {
        if (smoothCamera != null)
        {
            //OVRManager.display.RecenterPose();
            smoothCamera.Reset();
        }
    }

    private IEnumerator LoadScene(string sceneName)
    {
        if (SceneManager.GetActiveScene().name != sceneName)
        {
            fader.FadeOpaque();

            yield return new WaitForSeconds(2);

            SceneManager.LoadScene(sceneName);
        }
    }
}