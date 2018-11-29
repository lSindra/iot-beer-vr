using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftTextOnLook : MonoBehaviour {

    public Component child;
    public Transform text;
    public Transform playerCamera;
    public float textShift = 10;

    private Vector3 initialLocalTextPosition;
    private Vector3 maxLocalTextPosition;
    private readonly float maxAngle = 40;

	void LateUpdate () {
        //Quaternion rotation = Quaternion.LookRotation(playerCamera.transform.position - transform.position);
        //float angle = rotation.x + rotation.y + rotation.z;
        float angle = Vector3.Angle(transform.up, playerCamera.transform.position - child.GetComponent<Renderer>().bounds.center);

        if (angle < maxAngle)
        {
            ShiftTextForward(angle);
        }
    }

    private void ShiftTextForward(float angle)
    {
        float percentage = (maxAngle - angle) / maxAngle;
        text.localPosition = Vector3.Slerp(initialLocalTextPosition, maxLocalTextPosition, percentage);
    }

    void Start()
    {
        initialLocalTextPosition = text.localPosition;
        maxLocalTextPosition = new Vector3(initialLocalTextPosition.x, initialLocalTextPosition.y + textShift, initialLocalTextPosition.z);
    }

}
