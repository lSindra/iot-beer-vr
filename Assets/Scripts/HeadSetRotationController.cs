using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadSetRotationController : MonoBehaviour {

    public Transform hmdOrientation;
    public float minRotation = 0.15f;
    public float rotationSpeed = 3;
	
	void Update () {
        float hmdZRotation = hmdOrientation.rotation.z;
        if (hmdZRotation > minRotation)
        {
            this.transform.Rotate(new Vector3(0, rotationSpeed * -(hmdZRotation - minRotation), 0));
        } else if (hmdZRotation < -minRotation)
        {
            this.transform.Rotate(new Vector3(0, rotationSpeed * -(hmdZRotation + minRotation), 0));
        }
    }
}
