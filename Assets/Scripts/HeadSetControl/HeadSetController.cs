using UnityEngine;

public class HeadSetController : MonoBehaviour {
	
	void Update () {
        //Touch
        if(OVRInput.Get(OVRInput.Button.One))
        {
            SendMessage("SelectCountry");
        }
        //Back button
        if (OVRInput.Get(OVRInput.Button.Two))
        {
            LoadSceneAsyncOperation sceneAsyncOperation = new LoadSceneAsyncOperation();
            sceneAsyncOperation.LoadScene("mainLevel");
        }
    }
}
