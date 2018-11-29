using System.Collections;
using KetosGames.SceneTransition;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeadSetController : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
        if (!OVRPlugin.userPresent)
        {
            RecenterPose();
        }
        if (OVRInput.Get(OVRInput.Button.Two))
        {
            BackToStart();
        }
    }

    private void BackToStart()
    {
        if (SceneManager.GetActiveScene().name != "mainLevel")
        {
            SceneLoader.LoadScene("mainLevel");
        }
    }

    private void RecenterPose()
    {
        SceneLoader.LoadScene(SceneManager.GetActiveScene().name);
    }
}