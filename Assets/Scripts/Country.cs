using UnityEngine;
using UnityEngine.SceneManagement;

public class Country : MonoBehaviour {

    public string countryName;

    public void Selected()
    {
        SceneManager.LoadSceneAsync(countryName);
    }
}
