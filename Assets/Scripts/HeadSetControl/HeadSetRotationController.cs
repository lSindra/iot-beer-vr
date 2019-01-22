using UnityEngine;
using Valve.VR.InteractionSystem;
using System.Collections;

public class HeadSetRotationController : MonoBehaviour
{
    public Player player;
    public float speed = 20;
    public float delay = 0;

    private Transform hmdOrientation;
    private Rigidbody thisBody;
    private bool ready = false;

    void Start()
    {
        hmdOrientation = player.hmdTransform;
        thisBody = GetComponent<Rigidbody>();
        StartCoroutine("GetReady");
    }

    void Update()
    {
        if (ready)
        {
            MoveSatelite();
        }
    }

    IEnumerator GetReady()
    {
        yield return new WaitForSeconds(delay);
        ready = true;
    }

    void MoveSatelite()
    {
        thisBody.AddForce(-Vector3.up * speed / 200 * GetRotationX() * Time.deltaTime);

        thisBody.AddRelativeForce(-Vector3.right * speed / 100 * GetRotationY() * Time.deltaTime);
    }

    private float GetRotationY()
    {
        float y = hmdOrientation.localRotation.y;
        return y < 0.5 && y > -0.5 ? -y : 0;
    }

    private float GetRotationX()
    {
        float x = hmdOrientation.localRotation.x;
        return x < 0.5 && x > -0.5 ? x : 0;
    }
}
