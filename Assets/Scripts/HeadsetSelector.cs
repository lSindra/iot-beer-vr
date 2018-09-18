using UnityEngine;

public class HeadsetSelector : MonoBehaviour {

    public LayerMask mask = 0;

    void Update () {
        RaycastHit hit;
        Ray ray = new Ray(this.transform.position, this.transform.forward);

        if (Physics.Raycast(ray, out hit, mask))
        {
            hit.collider.gameObject.SendMessage("Selected");
        }
    }
}
