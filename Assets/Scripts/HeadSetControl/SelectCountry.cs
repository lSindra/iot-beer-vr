using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using KetosGames.SceneTransition;

public class SelectCountry : MonoBehaviour
{
    public Toggle toggle;
    public AudioSource audioEffect;
    public string countryName;
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

        DeleteControllers();

        StartCoroutine(NavigateAndFade());
    }

    private IEnumerator NavigateAndFade()
    {
        StartCoroutine(navigation.NavigationIterator(controllerPosition, GetComponentInParent<Transform>()));

        yield return new WaitForSeconds(1);

        SceneLoader.LoadScene(countryName);
    }

    private void DeleteControllers()
    {
        GameObject[] controllers = GameObject.FindGameObjectsWithTag("GameController");
        foreach (GameObject controller in controllers)
        {
            controller.SetActive(false);
        }
    }
}