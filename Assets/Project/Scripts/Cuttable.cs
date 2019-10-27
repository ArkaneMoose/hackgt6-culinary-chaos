using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cuttable : MonoBehaviour
{
    public int numCuts;
    public Image progress;
    public GameObject replaceWith;
    private int numCutsDone = 0;


    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit " + other.gameObject.name);
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter " + other.gameObject.name);
        if (other.tag == "Knife")
        {
            numCutsDone++;
            progress.fillAmount = (float)numCutsDone / numCuts;

            if (numCutsDone >= numCuts)
            {
                GameObject created = Instantiate(replaceWith, transform.parent);
                created.transform.position = GameObject.Find("Board").transform.position + new Vector3(0, 0.5f, 0);
                created.SetActiveRecursively(true);
                Destroy(gameObject);
            }
        }
    }
}
