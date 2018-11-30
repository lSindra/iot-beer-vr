using UnityEngine;
using System.Collections.Generic;
using Vuforia;

public class SmoothObjectTracking : MonoBehaviour {

	public int smoothingFrames = 3;

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

        return avgr;
    }

    private Quaternion AverageRotations(Quaternion avrg, Queue<Quaternion> queueRotations)
    {
        foreach (Quaternion singleRotation in queueRotations)
        {
            if (avrg == new Quaternion()) avrg = singleRotation;
            avrg = Quaternion.Lerp(avrg, singleRotation, 0.5f);
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

		VuforiaARController vuforia = VuforiaARController.Instance;

		vuforia.RegisterVuforiaStartedCallback(OnInitialized);
		vuforia.RegisterTrackablesUpdatedCallback (OnTrackablesUpdated);
	}

    private void OnInitialized()
    {}

    void LateUpdate () {
        transform.rotation = smoothedRotation;
        transform.position = smoothedPosition;
    }
}