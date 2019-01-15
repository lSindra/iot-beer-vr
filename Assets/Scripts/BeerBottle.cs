using UnityEngine;
using Valve.VR.InteractionSystem;

public class BeerBottle : MonoBehaviour
{
    public float minPouringAngle = 45;
    public GameObject cap;
    public ParticleSystem pouringEffect;

    private bool isOpen;
    private Interactable interactable;
    private ParticleSystem.EmissionModule emission;

    void Start()
    {
        isOpen = false;
        interactable = GetComponent<Interactable>();
        emission = pouringEffect.emission;
    }

    void Update()
    {
        if (isOpen)
        {
            UpdateBottleLiquid(GetBottleAngle());
        } else if (interactable.attachedToHand)
        {
            bool isOpeningBottle = false;
            if (isOpeningBottle)
            {
                isOpen = true;
                cap.SetActive(false);
            }
        }
    }

    float GetBottleAngle()
    {
        return Vector3.Angle(-transform.up, Vector3.up);
    }

    void UpdateBottleLiquid(float bottleAngle)
    {
        emission.enabled = bottleAngle > minPouringAngle ? true : false;
    }
}
