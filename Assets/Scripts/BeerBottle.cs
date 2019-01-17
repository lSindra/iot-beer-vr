using UnityEngine;
using Valve.VR.InteractionSystem;

public class BeerBottle : MonoBehaviour
{
    public float beerAmount = 100;
    public float beerLeft = 100;
    public float minPouringAngle = 100;
    public GameObject cap;
    public ParticleSystem pouringEffect;
    public bool isOpen = false;

    private Interactable interactable;
    private bool pouringBeer = false;

    void Start()
    {
        interactable = GetComponent<Interactable>();

        InvokeRepeating("UpdateBeerAmount", 0, 0.2f);
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

    bool AddBeer(float amount)
    {
        if (beerLeft < beerAmount)
        {
            beerLeft += amount;
            return true;
        }
        return false;
    }

    float GetBottleAngle()
    {
        return Vector3.Angle(-transform.up, Vector3.up);
    }

    void UpdateBottleLiquid(float bottleAngle)
    {
        if (bottleAngle < minPouringAngle - beerAmount + beerLeft)
        {
            pouringEffect.Emit(1);
            pouringBeer = true;
        } else
        {
            pouringBeer = false;
        }
    }

    void UpdateBeerAmount()
    {
        if (pouringBeer && beerLeft > 0)
        {
            beerLeft -= beerAmount/30;
        }
    }

    void OnParticleCollision(GameObject particle)
    {
        FillCupWithBeer(particle);
    }

    void FillCupWithBeer(GameObject particle)
    {
        if (!AddBeer(0.5f))
        {
            //Destroy(particle);
        }
    }

    public float PercentageFilled()
    {
        return beerLeft * 100 / beerAmount;
    }
}
