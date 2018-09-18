using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadSetRotationController : MonoBehaviour {

    public Transform hmdOrientation;
    public float minRotation = 0;
    public float rotationSpeed = 5;

    void Start () {}
	
	void Update () {
        if (hmdOrientation.rotation.z > minRotation)
        {
            this.transform.Rotate(new Vector3(0, 0, -rotationSpeed));
        } else if (hmdOrientation.rotation.z < -minRotation)
        {
            this.transform.Rotate(new Vector3(0, 0, rotationSpeed));
        }
    }
}
