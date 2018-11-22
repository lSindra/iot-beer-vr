using UnityEngine;

public class HeadSetController : MonoBehaviour {
    public FadeInAndOut fader;

	void Update () {
        //Touch
        if(OVRInput.Get(OVRInput.Button.One))
        {
            SendMessage("SelectCountry");
        }
        //Back button
        if (OVRInput.Get(OVRInput.Button.Two))
        {
            fader.FadeOpaque();
            LoadSceneAsyncOperation sceneAsyncOperation = new LoadSceneAsyncOperation();
            StartCoroutine(sceneAsyncOperation.LoadAsync("mainLevel"));
        }
    }
}
