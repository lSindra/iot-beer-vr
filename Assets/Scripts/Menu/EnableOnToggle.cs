using UnityEngine;
using VRStandardAssets.Utils;

public class EnableOnToggle : MonoBehaviour {

    public Renderer component;
    public VRInteractiveItem trigger;

	void Start () {
        trigger.OnOver += Enable;
        trigger.OnOut += Disable;
    }

    protected void Enable() {
        component.enabled = true;
    }

    protected void Disable()
    {
        component.enabled = false;
    }
}
