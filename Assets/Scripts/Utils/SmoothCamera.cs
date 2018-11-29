using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Vuforia;
using InputTracking = UnityEngine.XR.InputTracking;
using Node = UnityEngine.XR.XRNode;

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

	public void OnTrackablesUpdated()
    {
        RemoveOldestFrame();
        AddNewestFrame();

        smoothedRotation = GetAverageFromRotations();
        smoothedPosition = GetAverageFromPositions();
    }

    private Vector3 GetAverageFromPositions()
    {
        Vector3 avgp = Vector3.zero;
        foreach (Vector3 singlePosition in positions)
        {
            avgp += singlePosition;
        }
        avgp /= positions.Count;
        return avgp;
    }

    private Quaternion GetAverageFromRotations()
    {
        Quaternion avgr = new Quaternion();

        avgr = AverageRotations(avgr, rotations);
        avgr = AverageRotations(avgr, new Queue<Quaternion>(new[] { InputTracking.GetLocalRotation(Node.CenterEye) }));

        return avgr;
    }

    private Quaternion AverageRotations(Quaternion avrg, Queue<Quaternion> queueRotations)
    {
        Quaternion lastRotation = new Quaternion();
        //Quaternion diffRotation;

        if (avrg == new Quaternion()) avrg = queueRotations.Peek();

        foreach (Quaternion singleRotation in queueRotations)
        {
            avrg = Quaternion.Lerp(avrg, singleRotation, 0.5f);

            //if (lastRotation != new Quaternion())
            //{
            //    Quaternion nextAvrg = new Quaternion();
            //    Queue<Quaternion> nextRotations = queueRotations;
            //    nextRotations.Dequeue();

            //    nextAvrg = AverageRotations(nextAvrg, nextRotations);
            //    diffRotation = nextAvrg * Quaternion.Inverse(lastRotation);

            //    Quaternion predictedRotation = diffRotation * singleRotation;
            //    avrg = Quaternion.Lerp(avrg, predictedRotation, 0.5f);
            //}


            lastRotation = singleRotation;
        }
        return avrg;
    }

    private void AddNewestFrame()
    {
        rotations.Enqueue(transform.rotation);
        positions.Enqueue(transform.position);
    }

    private void RemoveOldestFrame()
    {
        if (rotations.Count >= smoothingFrames)
        {
            rotations.Dequeue();
            positions.Dequeue();
        }

    }

    void Start () {

		rotations = new Queue<Quaternion>(smoothingFrames);
		positions = new Queue<Vector3>(smoothingFrames);
        qcarBehavior = GetComponent<VuforiaBehaviour>();

		VuforiaARController vuforia = VuforiaARController.Instance;

		vuforia.RegisterVuforiaStartedCallback(OnInitialized);
		vuforia.RegisterTrackablesUpdatedCallback (OnTrackablesUpdated);
	}

    private void OnInitialized()
    {}

    void LateUpdate () {
        if (smoothRotation)
        {
            transform.rotation = smoothedRotation;
        }
        else
        {
            transform.rotation = InputTracking.GetLocalRotation(Node.CenterEye);
        }

        if (smoothPosition) {
            transform.position = smoothedPosition;
        }
	}
}