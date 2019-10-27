using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public GameObject template;
    public Transform reparent;
    public int numObjects = 10;
    public Vector3 randomSpawn;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numObjects; i++)
        {
            CloneTemplate();
        }
    }

    void Update()
    {
        Transform parent = template.transform.parent;
        int objectCount = parent.childCount - 1;
        for (; objectCount < numObjects; objectCount++)
        {
            CloneTemplate();
        }
        for (; objectCount > numObjects; objectCount--)
        {
            Destroy(parent.GetChild(objectCount - numObjects).gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent == reparent)
        {
            other.transform.parent = template.transform.parent;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.parent == template.transform.parent)
        {
            other.transform.parent = reparent;
        }
    }

    private void CloneTemplate()
    {
        GameObject cloned = Instantiate(
            template,
            template.transform.parent,
            true
        );

        cloned.transform.Translate(
            Random.Range(-randomSpawn.x, randomSpawn.x),
            Random.Range(-randomSpawn.y, randomSpawn.y),
            Random.Range(-randomSpawn.z, randomSpawn.z)
        );

        cloned.SetActive(true);
    }
}
