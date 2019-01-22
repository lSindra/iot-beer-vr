using System;
using System.Collections;
using KetosGames.SceneTransition;
using Valve.VR;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

public class HeadSetController : MonoBehaviour
{
    public string mainScene = "mainLevel";
    public string ibsoScene = "IBSO";
    public int gameTime = 120;

    private bool active = false;

    void Start()
    {
        //StartCoroutine(StartActivateControllers());
        StartCoroutine(WaitThenLoadIBSO());

        GlobalCountDown.StartCountDown(TimeSpan.FromSeconds(gameTime));
    }

    void Update()
    {
       
    }

    public static void SetActiveControllers(bool active)
    {
        GameObject[] controllers = GameObject.FindGameObjectsWithTag("GameController");
        foreach (GameObject controller in controllers)
        {
            controller.SetActive(active);
        }
    }

    private IEnumerator StartActivateControllers()
    {
        SetActiveControllers(false);

        yield return new WaitForSeconds(2);

        SetActiveControllers(true);

        yield return null;
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
    }
}