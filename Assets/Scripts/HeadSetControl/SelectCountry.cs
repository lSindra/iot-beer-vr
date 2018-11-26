using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectCountry : MonoBehaviour
{
    public Toggle toggle;
    public AudioSource audioEffect;
    public string countryName;
    public FadeInAndOut fader;
    public Transform controllerPosition;

    private CountryNavigation navigation;

    void Start()
    {
        navigation = new CountryNavigation();
        toggle.onValueChanged.AddListener(ToggleEffect);
    }

    void ToggleEffect(bool isOn)
    {
        audioEffect.PlayOneShot(audioEffect.clip);

        LoadSceneAsyncOperation sceneAsyncOperation = new LoadSceneAsyncOperation();
        StartCoroutine(sceneAsyncOperation.LoadAsync(countryName));

        StartCoroutine(NavigateAndFade());
    }

    private IEnumerator NavigateAndFade()
    {
        StartCoroutine(navigation.NavigationIterator(controllerPosition, GetComponentInParent<Transform>()));

        yield return new WaitForSeconds(1);

        fader.FadeOpaque();
    }
}