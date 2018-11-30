using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

    public GameObject Object;
    public bool destroy;
    public float destroyAfter;

    private GameObject instance;

	void Start () {
        instance = Instantiate(Object);

        if (destroy)
        {
            Destroy(instance, destroyAfter);
        }
    }
}
