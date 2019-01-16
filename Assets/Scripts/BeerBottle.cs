using UnityEngine;
using Valve.VR.InteractionSystem;

public class BeerBottle : MonoBehaviour
{
    public float beerAmount = 140;
    public float minPouringAngle = 45;
    public GameObject cap;
    public ParticleSystem pouringEffect;

    private bool isOpen;
    private Interactable interactable;
    private ParticleSystem.EmissionModule emission;
    private float beerLeft;

    void Start()
    {
        isOpen = false;
        interactable = GetComponent<Interactable>();
        emission = pouringEffect.emission;
        beerLeft = beerAmount;

        InvokeRepeating("UpdateBottleAmount", 0, 0.2f);
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
        emission.enabled = bottleAngle > minPouringAngle + beerLeft - beerAmount ? true : false;
    }

    void UpdateBeerAmount()
    {
        if (emission.enabled && beerLeft > 0)
        {
            beerLeft -= beerAmount / 50;
        }
    }
}
