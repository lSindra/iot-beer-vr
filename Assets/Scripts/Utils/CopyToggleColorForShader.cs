using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CopyToggleColorForShader : MonoBehaviour {

    public Material defaultMaterial;
    public Material highlightMaterial;
    public Toggle toggle;
    public GameObject thisObject;

	void Start () {
        thisObject.GetComponent<MeshRenderer>().material = defaultMaterial;

        EventTrigger eventTrigger = toggle.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry enterEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter
        };
        enterEntry.callback.AddListener((data) => { HightlightMaterial(); });

        EventTrigger.Entry exitEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerExit
        };
        exitEntry.callback.AddListener((data) => { DefaultMaterial(); });

        eventTrigger.triggers.Add(enterEntry);
        eventTrigger.triggers.Add(exitEntry);
    }

    protected void HightlightMaterial() {
        thisObject.GetComponent<MeshRenderer>().material = highlightMaterial;
    }

    protected void DefaultMaterial()
    {
        thisObject.GetComponent<MeshRenderer>().material = defaultMaterial;
    }
}
