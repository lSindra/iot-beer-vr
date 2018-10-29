using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CopyToggleColor : MonoBehaviour {

    public MaskableGraphic child;
    public Toggle toggle;

	void Start () {
        EventTrigger eventTrigger = toggle.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry enterEntry = new EventTrigger.Entry();
        enterEntry.eventID = EventTriggerType.PointerEnter;
        enterEntry.callback.AddListener((data) => { EnterColor((PointerEventData)data); });

        EventTrigger.Entry exitEntry = new EventTrigger.Entry();
        exitEntry.eventID = EventTriggerType.PointerExit;
        exitEntry.callback.AddListener((data) => { ExitColor((PointerEventData)data); });

        eventTrigger.triggers.Add(enterEntry);
        eventTrigger.triggers.Add(exitEntry);

    }

    protected void EnterColor(PointerEventData data) {
        child.color = toggle.colors.highlightedColor;
    }

    protected void ExitColor(PointerEventData data)
    {
        child.color = toggle.colors.normalColor;
    }
}
