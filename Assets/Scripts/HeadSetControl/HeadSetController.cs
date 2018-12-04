using System;
using KetosGames.SceneTransition;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeadSetController : MonoBehaviour
{
    public string mainScene = "mainLevel";
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
}