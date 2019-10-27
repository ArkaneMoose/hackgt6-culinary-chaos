using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;

public class CuttingBoard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Tomato" && other.tag != "Lettuce")
        {
            return;
        }

        Transform canvas = other.transform.Find("Canvas");
        if (!canvas) return;

        canvas.gameObject.SetActive(true);
        Destroy(other.gameObject.GetComponent<InteractionBehaviour>());
        Destroy(other.attachedRigidbody);
        Bounds bounds = other.gameObject.GetComponent<Renderer>().bounds;
        float yOffset = other.transform.position.y - bounds.center.y;
        other.transform.position = transform.position
            + new Vector3(0f, bounds.size.y / 2 + yOffset, 0f);

        Collider[] colliders = other.gameObject.GetComponents<Collider>();
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].isTrigger)
            {
                // Only used for interaction by hand
                colliders[i].enabled = false;
            }
            else
            {
                // The one representing physical object
                colliders[i].isTrigger = true;
            }
        }
    }
}
