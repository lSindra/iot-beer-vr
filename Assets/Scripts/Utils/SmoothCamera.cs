using UnityEngine;
using System.Collections.Generic;
using Vuforia;
using InputTracking = UnityEngine.XR.InputTracking;
using Node = UnityEngine.XR.XRNode;
using System;

[RequireComponent(typeof (VuforiaBehaviour))]
public class SmoothCamera : MonoBehaviour {

	public int smoothingFrames = 3;
    public bool smoothPosition = true;
    public bool smoothRotation = true;
	private VuforiaBehaviour qcarBehavior;

    private Quaternion smoothedRotation;
	private Vector3 smoothedPosition;

	private Queue<Quaternion> rotations;
	private Queue<Vector3> positions;

	public void OnTrackablesUpdated() {
        if (rotations.Count >= smoothingFrames) {
			rotations.Dequeue();
			positions.Dequeue();
		}

		rotations.Enqueue(transform.rotation);
		positions.Enqueue(transform.position);

		Vector4 avgr = Vector4.zero;
		foreach (Quaternion singleRotation in rotations) {
            Math3d.AverageQuaternion(ref avgr, singleRotation, rotations.Peek(), rotations.Count);
		}
        Math3d.AverageQuaternion(ref avgr, InputTracking.GetLocalRotation(Node.CenterEye), rotations.Peek(), rotations.Count);

        Vector3 avgp = Vector3.zero;
		foreach (Vector3 singlePosition in positions) {
			avgp += singlePosition;
		}
		avgp /= positions.Count;

		smoothedRotation = new Quaternion(avgr.x, avgr.y, avgr.z, avgr.w);
		smoothedPosition = avgp;

        if (rotations.Count >= smoothingFrames && smoothRotation)
        {
            transform.rotation = smoothedRotation;
        }
    }

    public void Reset()
    {
        rotations.Dequeue();
        positions.Dequeue();
        rotations.Clear();
        positions.Clear();
        transform.position = new Vector3(0, 0, 0);
        transform.rotation = new Quaternion(0, 0, 0, 0);
        smoothedPosition = new Vector3(0, 0, 0);
        smoothedRotation = new Quaternion(0, 0, 0, 0);
        rotations.Enqueue(transform.rotation);
        positions.Enqueue(transform.position);
    }

    // Use this for initialization
    void Start () {

		rotations = new Queue<Quaternion>(smoothingFrames);
		positions = new Queue<Vector3>(smoothingFrames);
        qcarBehavior = GetComponent<VuforiaBehaviour>();

		VuforiaARController vuforia = VuforiaARController.Instance;
//		qcarBehavior.RegisterVuforiaStartedCallback(OnInitialized);
//		qcarBehavior.RegisterTrackablesUpdatedCallback(OnTrackablesUpdated);

		vuforia.RegisterVuforiaStartedCallback(OnInitialized);
		vuforia.RegisterTrackablesUpdatedCallback (OnTrackablesUpdated);
	}

    private void OnInitialized()
    {}

    // Update is called once per frame
    void LateUpdate () {
        transform.rotation = InputTracking.GetLocalRotation(Node.CenterEye);
        if (smoothPosition) {
            transform.position = smoothedPosition;
        }
	}

}