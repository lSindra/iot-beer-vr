using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CopyToggleColorForShader : MonoBehaviour {

    public Material defaultMaterial;
    public Toggle toggle;
    public GameObject thisObject;

    Material material;

	void Start () {
        material = new Material(defaultMaterial);
        thisObject.GetComponent<MeshRenderer>().material = material;

        EventTrigger eventTrigger = toggle.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry enterEntry = new EventTrigger.Entry();
        enterEntry.eventID = EventTriggerType.PointerEnter;
        enterEntry.callback.AddListener((data) => { EnterColor(); });

        EventTrigger.Entry exitEntry = new EventTrigger.Entry();
        exitEntry.eventID = EventTriggerType.PointerExit;
        exitEntry.callback.AddListener((data) => { ExitColor(); });

        eventTrigger.triggers.Add(enterEntry);
        eventTrigger.triggers.Add(exitEntry);
    }

    protected void EnterColor() {
        material.color = toggle.colors.highlightedColor;
    }

    protected void ExitColor()
    {
        material.color = defaultMaterial.color;
    }
}
