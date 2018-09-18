using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            SceneManager.LoadSceneAsync("mainLevel");
        }
    }
}
