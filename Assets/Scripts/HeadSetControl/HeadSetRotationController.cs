using UnityEngine;

public class HeadSetRotationController : MonoBehaviour {

    public Transform hmdOrientation;
    public float minRotation = 0.15f;
    public float rotationSpeed = 3;
	
	void Update ()
    {
        UpdateZRotation();
        UpdateXRotation();
    }

    private void UpdateXRotation()
    {
        float hmdXRotation = hmdOrientation.rotation.x;
        Quaternion rotation = transform.rotation;

        //transform.rotation = Quaternion.Slerp(rotation, new Quaternion(-25f, rotation.y, rotation.z, rotation.w), hmdXRotation);

        //if (hmdXRotation > minRotation)
        //{
        //    transform.rotation = Quaternion.Slerp(rotation, new Quaternion(-25, rotation.y, rotation.z, rotation.w), hmdXRotation/180);
        //}
        //else if (hmdXRotation < -minRotation)
        //{
        //    transform.rotation = Quaternion.Lerp(rotation, new Quaternion(25, rotation.y, rotation.z, rotation.w), -hmdXRotation/180);
        //}
    }

    private void UpdateZRotation()
    {
        float hmdZRotation = hmdOrientation.rotation.z;
        if (hmdZRotation > minRotation)
        {
            this.transform.Rotate(new Vector3(0, rotationSpeed * -(hmdZRotation - minRotation), 0));
        }
        else if (hmdZRotation < -minRotation)
        {
            this.transform.Rotate(new Vector3(0, rotationSpeed * -(hmdZRotation + minRotation), 0));
        }
    }
}
