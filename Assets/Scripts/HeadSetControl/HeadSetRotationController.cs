using UnityEngine;

public class HeadSetRotationController : MonoBehaviour
{

    public Transform hmdOrientation;
    public Transform worldTransform;
    public float minRotation = 0.10f;
    public float maxRotation = 0.45f;
    public float rotationSpeed = 3;

    float adjustedXRotation;
    float adjustedYRotation;

    void Update()
    {
        UpdateXRotation();
        UpdateYRotation();

        MoveSatelite();
    }

    void MoveSatelite()
    {
        print(transform.rotation.y);
        if (transform.rotation.y < -0.75 || transform.rotation.y > 0.75) {
            adjustedXRotation = -adjustedXRotation;
        }
        if (transform.rotation.x < 0.3 && adjustedXRotation < 0)
        {
            transform.RotateAround(worldTransform.position, worldTransform.right, -adjustedXRotation);
        }
        else if (transform.rotation.x > -0.3 && adjustedXRotation > 0)
        {
            transform.RotateAround(worldTransform.position, worldTransform.right, -adjustedXRotation);
        }
        transform.RotateAround(worldTransform.position, worldTransform.up, -adjustedYRotation);
    }

    void UpdateXRotation()
    {
        float hmdXRotation = hmdOrientation.localRotation.x;
        adjustedXRotation = 0;
        if (hmdXRotation > minRotation && hmdXRotation < maxRotation)
        {
            adjustedXRotation = rotationSpeed * (hmdXRotation - minRotation);
        }
        else if (hmdXRotation < -minRotation && hmdXRotation > -maxRotation)
        {
            adjustedXRotation = rotationSpeed * (hmdXRotation + minRotation);
        }
        adjustedXRotation *= 100 * Time.deltaTime;
    }

    void UpdateYRotation()
    {
        float hmdYRotation = hmdOrientation.localRotation.y;
        adjustedYRotation = 0;
        if (hmdYRotation > minRotation && hmdYRotation < maxRotation)
        {
            adjustedYRotation = rotationSpeed * (hmdYRotation - minRotation);
        }
        else if (hmdYRotation < -minRotation && hmdYRotation > -maxRotation)
        {
            adjustedYRotation = rotationSpeed * (hmdYRotation + minRotation);
        }
        adjustedYRotation *= 100 * Time.deltaTime;
    }
}
