using UnityEngine;
using UnityEngine.UI;

public class SelectCountry : MonoBehaviour
{
    public Toggle toggle;
    public AudioSource audioEffect;
    public string countryName;
    public FadeInAndOut fader;

    void Start()
    {
        toggle.onValueChanged.AddListener(ToggleEffect);
    }

    void ToggleEffect(bool isOn)
    {
        audioEffect.PlayOneShot(audioEffect.clip);
        fader.FadeOpaque();
        LoadSceneAsyncOperation sceneAsyncOperation = new LoadSceneAsyncOperation();
        StartCoroutine(sceneAsyncOperation.LoadAsync(countryName));
    }
}