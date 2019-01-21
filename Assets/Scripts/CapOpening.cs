using UnityEngine;

public class CapOpening : MonoBehaviour
{
    public FixedJoint joint;
    public BeerBottle beerBottle;

    void Update()
    {
        if (joint.currentForce.magnitude > joint.breakForce)
        {
            beerBottle.OpenCap();
        }
    }
}
