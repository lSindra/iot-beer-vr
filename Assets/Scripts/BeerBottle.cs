using UnityEngine;
using Valve.VR.InteractionSystem;

public class BeerBottle : MonoBehaviour
{
    public float beerAmount = 100;
    public float beerLeft = 100;
    public float minPouringAngle = 100;
    public GameObject cap;
    public ParticleSystem pouringEffect;
    public ParticleSystem openingEffect;
    public AudioSource audioSource;
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
            cap.GetComponent<CapOpening>().enabled = true;
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
            if (!audioSource.isPlaying) audioSource.Play();
        }
        else audioSource.Stop();
    }

    void OnParticleCollision(GameObject particle)
    {
        AddBeer(0.5f);
    }

    public float PercentageFilled()
    {
        return beerLeft * 100 / beerAmount;
    }

    public void OpenCap()
    {
        isOpen = true;
        cap.SetActive(false);
        pouringEffect.Emit(1);
        openingEffect.Emit(1);
    }
}
