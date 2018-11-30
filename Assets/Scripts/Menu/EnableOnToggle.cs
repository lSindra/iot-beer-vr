using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EnableOnToggle : MonoBehaviour {

    public Renderer component;
    public Toggle toggle;

	void Start () {
        EventTrigger eventTrigger = toggle.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry enterEntry = new EventTrigger.Entry();
        enterEntry.eventID = EventTriggerType.PointerEnter;
        enterEntry.callback.AddListener((data) => { Enable(); });

        EventTrigger.Entry exitEntry = new EventTrigger.Entry();
        exitEntry.eventID = EventTriggerType.PointerExit;
        exitEntry.callback.AddListener((data) => { Disable(); });

        eventTrigger.triggers.Add(enterEntry);
        eventTrigger.triggers.Add(exitEntry);
    }

    protected void Enable() {
        component.enabled = true;
    }

    protected void Disable()
    {
        component.enabled = false;
    }
}
