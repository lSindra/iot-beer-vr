using UnityEngine;

public class HeadSetRotationController : MonoBehaviour
{

    public Transform hmdOrientation;
    public Transform worldTransform;
    public Transform desiredDirection;
    public float minRotation = 0.10f;
    public float maxRotation = 0.45f;
    public float rotationSpeed = 3;

    private readonly float EPSILON = Mathf.Epsilon;

    void LateUpdate()
    {
        MoveSatelite();
    }

    void MoveSatelite()
    {
        if (Mathf.Abs(GetMoveUp()) > EPSILON)
        {
            int height = 0;
            if (GetMoveUp() > 0.05) height = 1;
            if (GetMoveUp() < 0.05) height = -1;
            transform.position = Vector3.Slerp(transform.position, new Vector3(transform.position.x, worldTransform.position.y + 450 * height, transform.position.z), Mathf.Abs(GetMoveUp()) * rotationSpeed * Time.deltaTime);
        }
        transform.RotateAround(worldTransform.position, worldTransform.up, rotationSpeed/5 * GetMoveRight());
    }

    private float GetMoveUp()
    {
        float moveUp = EPSILON;

        if (hmdOrientation.localRotation.x < -minRotation) moveUp = -hmdOrientation.localRotation.x+minRotation;
        if (hmdOrientation.localRotation.x > minRotation) moveUp = -hmdOrientation.localRotation.x-minRotation;
        if (hmdOrientation.localRotation.x > maxRotation || hmdOrientation.localRotation.x < -maxRotation) moveUp = EPSILON;
        return moveUp;
    }

    private float GetMoveRight()
    {
        float moveRight = 0;

        if (hmdOrientation.localRotation.y < -minRotation) moveRight = -hmdOrientation.localRotation.y+minRotation;
        if (hmdOrientation.localRotation.y > minRotation) moveRight = -hmdOrientation.localRotation.y-minRotation;
        if (hmdOrientation.localRotation.y > maxRotation || hmdOrientation.localRotation.y < -maxRotation) moveRight = 0;
        return moveRight;
    }
}
