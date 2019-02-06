using UnityEngine;

public class LiquidPhysics : MonoBehaviour
{
    public int sloshSpeed = 65;
    public int difference = 25;

    public void UpdateLayerRotation(GameObject layer)
    {
        Slosh(layer);
    }

    void Slosh(GameObject layer)
    {
        Quaternion inverseRotation = Quaternion.Inverse(transform.rotation);
        Vector3 finalRotation = Quaternion.RotateTowards(layer.transform.localRotation, inverseRotation, sloshSpeed * Time.deltaTime).eulerAngles;

        finalRotation.x = ClampRotationValue(finalRotation.x);
        finalRotation.z = ClampRotationValue(finalRotation.z);

        layer.transform.localEulerAngles = finalRotation;
    }

    float ClampRotationValue(float value)
    {
        if (value > 180)
        {
            return Mathf.Clamp(value, 360 - difference, 360);
        }
        else
        {
            return Mathf.Clamp(value, 0, difference);
        }
    }
}
