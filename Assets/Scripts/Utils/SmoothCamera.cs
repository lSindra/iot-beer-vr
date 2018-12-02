using UnityEngine;
using System.Collections.Generic;
using Vuforia;
using InputTracking = UnityEngine.XR.InputTracking;
using Node = UnityEngine.XR.XRNode;

[RequireComponent(typeof(VuforiaBehaviour))]
public class SmoothCamera : MonoBehaviour
{
    public int smoothingFrames = 10;
    public int vuforiaDelay = 7;
    public int bufferSize = 1;
    public bool smoothPosition = true;
    public bool smoothRotation = true;

    VuforiaBehaviour qcarBehavior;
    Queue<Quaternion> vuforiaRotations;
    Queue<Vector3> vuforiaPositions;
    Queue<Quaternion> buffer;

    Quaternion smoothedVuforiaRotation;
    Vector3 smoothedVuforiaPosition;

    Queue<Quaternion> gearRotations;
    Quaternion lastGearFrame;
    Quaternion smoothedGearRotation;

    void Start()
    {
        vuforiaRotations = new Queue<Quaternion>(smoothingFrames);
        vuforiaPositions = new Queue<Vector3>(smoothingFrames);
        gearRotations = new Queue<Quaternion>(vuforiaDelay + bufferSize);
        buffer = new Queue<Quaternion>(bufferSize);

        qcarBehavior = GetComponent<VuforiaBehaviour>();
        VuforiaARController vuforia = VuforiaARController.Instance;

        vuforia.RegisterVuforiaStartedCallback(OnInitialized);
        vuforia.RegisterTrackablesUpdatedCallback(OnTrackablesUpdated);
    }

    void LateUpdate()
    {
        if (smoothRotation)
        {
            transform.rotation = smoothedVuforiaRotation * smoothedGearRotation;
        }
        else
        {
            transform.rotation = InputTracking.GetLocalRotation(Node.CenterEye);
        }

        if (smoothPosition)
        {
            transform.position = smoothedVuforiaPosition;
        }
    }

    public void OnTrackablesUpdated()
    {
        UpdateVuforiaTrackable();
        UpdateVuforiaAverage();

        UpdateGearTrackableDiff();
        UpdateGearAverage();
    }

    void UpdateVuforiaAverage()
    {
        smoothedVuforiaRotation = GetAverageFromRotations();
        smoothedVuforiaPosition = GetAverageFromPositions();
    }

    void UpdateGearAverage()
    {
        smoothedGearRotation = GetSumFromRotations(gearRotations);
    }

    Vector3 GetAverageFromPositions()
    {
        Vector3 avgp = Vector3.zero;
        foreach (Vector3 singlePosition in vuforiaPositions)
        {
            avgp += singlePosition;
        }
        avgp /= vuforiaPositions.Count;
        return avgp;
    }

    Quaternion GetAverageFromRotations()
    {
        Quaternion avgr = new Quaternion();

        avgr = AverageRotations(avgr, vuforiaRotations);

        return avgr;
    }

    private Quaternion AverageRotations(Quaternion avrg, Queue<Quaternion> queueRotations)
    {
        if (avrg == new Quaternion()) avrg = queueRotations.Peek();

        foreach (Quaternion singleRotation in queueRotations)
        {
            avrg = Quaternion.Lerp(avrg, singleRotation, 0.5f);
        }
        return avrg;
    }

    Quaternion GetSumFromRotations(Queue<Quaternion> queueRotations)
    {
        Quaternion sum = new Quaternion();
        bool first = true;

        foreach (Quaternion singleRotation in queueRotations)
        {
            if (first)
            {
                sum = singleRotation;
                first = false;
            }
            else
            {
                sum *= singleRotation;
            }
        }
        return sum;
    }

    void UpdateVuforiaTrackable()
    {
        RemoveOldestVuforiaFrame();
        AddNewestVuforiaFrame();
    }

    void AddNewestVuforiaFrame()
    {
        buffer.Enqueue(transform.rotation);

        if (buffer.Count >= bufferSize)
        {
            vuforiaRotations.Enqueue(buffer.Peek());
            buffer.Dequeue();
        } else
        {
            vuforiaRotations.Enqueue(transform.rotation);
        }

        vuforiaPositions.Enqueue(transform.position);
    }

    void RemoveOldestVuforiaFrame()
    {
        if (vuforiaRotations.Count >= smoothingFrames)
        {
            vuforiaRotations.Dequeue();
            vuforiaPositions.Dequeue();
        }
    }

    void UpdateGearTrackableDiff()
    {
        RemoveOldestGearDiff();
        AddNewestGearDiff();
    }

    void AddNewestGearDiff()
    {
        Quaternion gearDiffMovement = InputTracking.GetLocalRotation(Node.CenterEye) * Quaternion.Inverse(lastGearFrame);

        gearRotations.Enqueue(gearDiffMovement);

        lastGearFrame = InputTracking.GetLocalRotation(Node.CenterEye);
    }

    void RemoveOldestGearDiff()
    {
        if (gearRotations.Count >= vuforiaDelay + bufferSize)
        {
            gearRotations.Dequeue();
        }
    }

    public void OnInitialized()
    {}
}