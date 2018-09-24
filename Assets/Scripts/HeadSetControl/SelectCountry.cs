using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectCountry : MonoBehaviour
{

    public Toggle toggle;
    public AudioSource audioEffect;
    public string countryName;

    void Start()
    {
        toggle.onValueChanged.AddListener(ToggleEffect);
    }

    void ToggleEffect(bool isOn)
    {
        audioEffect.PlayOneShot(audioEffect.clip);
        SceneManager.LoadSceneAsync(countryName);
    }
}