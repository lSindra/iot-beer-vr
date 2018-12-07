using UnityEngine;
using System.Collections;
using KetosGames.SceneTransition;
using VRStandardAssets.Utils;

public class SelectCountry : MonoBehaviour
{
    public AudioSource audioEffect;
    public string countryName;
    public Transform controllerPosition;

    private CountryNavigation navigation;
    private VRInteractiveItem trigger;

    void Start()
    {
        trigger = GetComponent<VRInteractiveItem>();
        navigation = new CountryNavigation();

        trigger.OnClick += ToggleEffect;
    }

    void ToggleEffect()
    {
        audioEffect.PlayOneShot(audioEffect.clip);

        HeadSetController.SetActiveControllers(false);

        StartCoroutine(NavigateAndFade());
    }

    private IEnumerator NavigateAndFade()
    {
        StartCoroutine(navigation.NavigationIterator(controllerPosition, GetComponentInParent<Transform>()));

        yield return new WaitForSeconds(1);

        SceneLoader.LoadScene(countryName);
    }
}