using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public float startingSpawnDelay = 0;
    public List<GameObject> spawningObjects;
    public List<GameObject> initialObjects;

    private List<GameObject> instances;
    private List<Transform> initialTransforms;

    void Start()
    {
        instances = new List<GameObject>();
        initialTransforms = new List<Transform>();

        StartCoroutine("DelayedStart");
    }

    public IEnumerator DelayedStart()
    {
        foreach (GameObject initial in initialObjects)
        {
            GameObject emptyGO = new GameObject();
            Transform copiedTransform = emptyGO.transform;
            copiedTransform.SetPositionAndRotation(initial.transform.position, initial.transform.rotation);
            initialTransforms.Add(copiedTransform);
            Destroy(initial);
        }

        yield return new WaitForSeconds(startingSpawnDelay);

        SpawnAll();
    }

    public void ReSpawn()
    {
        DespawnAll();
        SpawnAll();
    }

    void DespawnAll()
    {
        foreach(GameObject instance in instances)
        {
            Destroy(instance);
        }
        instances.Clear();
    }

    void SpawnAll()
    {
        if(spawningObjects.Count == initialTransforms.Count)
        {
            for (int i = 0; i < spawningObjects.Count; i++)
            {
                instances.Add(Instantiate(spawningObjects[i], initialTransforms[i].position, initialTransforms[i].rotation));
            }
        }
    }

}
