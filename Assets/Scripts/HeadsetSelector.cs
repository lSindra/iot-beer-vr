using UnityEngine;

public class HeadsetSelector : MonoBehaviour {

    public LayerMask mask = 0;
    public Transform centerEye;

    //just for testing
    void Update()
    {
        Ray ray = new Ray(centerEye.position, centerEye.forward);

        Debug.DrawLine(centerEye.position, centerEye.forward, Color.green);
    }

    public void SelectCountry() {
        RaycastHit hit;
        Ray ray = new Ray(centerEye.position, centerEye.forward);

        if (Physics.Raycast(ray, out hit, mask))
        {
            Destroy(hit.collider.gameObject);//TEMP
            hit.collider.gameObject.SendMessage("Selected");
        }
    }
}
