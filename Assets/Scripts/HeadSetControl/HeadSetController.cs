using System;
using System.Collections;
using KetosGames.SceneTransition;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeadSetController : MonoBehaviour
{
    public string mainScene = "mainLevel";
    public string ibsoScene = "IBSO";
    public int gameTime = 120;

    void Start()
    {
        GlobalCountDown.StartCountDown(TimeSpan.FromSeconds(gameTime));
    }

    void Update()
    {
        if (!OVRPlugin.userPresent)
        {
            RestartGame();
        }
        if (OVRInput.Get(OVRInput.Button.Two))
        {
            BackToStart();
        }
        if (GlobalCountDown.TimeLeft <= TimeSpan.Zero)
        {
            StartCoroutine(WaitThenLoadIBSO());
        }
    }

    private void BackToStart()
    {
        if (SceneManager.GetActiveScene().name != mainScene)
        {
            SceneLoader.LoadScene(mainScene);
        }
    }

    private void RestartGame()
    {
        SceneLoader.LoadScene(mainScene);
        GlobalCountDown.RestartCountDown(TimeSpan.FromSeconds(gameTime));
    }

    private IEnumerator WaitThenLoadIBSO() {
        yield return new WaitForSeconds(3);
        if (SceneManager.GetActiveScene().name != ibsoScene)
        {
            SceneLoader.LoadScene(ibsoScene);
        }
        yield return null;
    }
}