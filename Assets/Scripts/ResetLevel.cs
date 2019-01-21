using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ResetLevel : MonoBehaviour
{
    public ObjectSpawner spawner;

    public void OnButtonDown(Hand fromHand)
    {
        spawner.ReSpawn();
        fromHand.TriggerHapticPulse(1000);
    }
}