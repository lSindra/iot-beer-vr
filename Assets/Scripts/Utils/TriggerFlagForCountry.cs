using UnityEngine;
using VRStandardAssets.Utils;

public class TriggerFlagForCountry : MonoBehaviour {

    public Material defaultMaterial;
    public Material highlightMaterial;

    private VRInteractiveItem trigger;

	void Start () {
        GetComponent<MeshRenderer>().material = defaultMaterial;

        trigger = GetComponent<VRInteractiveItem>();
        trigger.OnOver += HightlightMaterial;
        trigger.OnOut += DefaultMaterial;
    }

    protected void HightlightMaterial() {
        GetComponent<MeshRenderer>().material = highlightMaterial;
    }

    protected void DefaultMaterial()
    {
        GetComponent<MeshRenderer>().material = defaultMaterial;
    }
}
