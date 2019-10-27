using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayArea : MonoBehaviour
{
    public GameObject template;

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Knife")
        {
            Invoke("CloneTemplate", 3);
        }
    }

    private void CloneTemplate()
    {
        GameObject cloned = Instantiate(
            template,
            template.transform.parent,
            true
        );
        cloned.SetActive(true);
    }
}
