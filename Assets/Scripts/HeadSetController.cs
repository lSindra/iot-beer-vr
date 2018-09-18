using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeadSetController : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        //Touch
        if(OVRInput.Get(OVRInput.Button.One))
        {

        }
        //Back button
        if (OVRInput.Get(OVRInput.Button.Two))
        {
            SceneManager.LoadSceneAsync("mainLevel");
        }
    }
}
