using UnityEngine;

public class HeadsetSelector : MonoBehaviour {

    public LayerMask mask = 0;

    public void SelectCountry() {
        RaycastHit hit;
        Ray ray = new Ray(this.transform.position, this.transform.forward);

        if (Physics.Raycast(ray, out hit, mask))
        {
            hit.collider.gameObject.SendMessage("Selected");
        }
    }
}
