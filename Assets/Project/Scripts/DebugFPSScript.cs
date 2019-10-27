using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugFPSScript : MonoBehaviour
{
    public float speed = 10;
    public bool holding = false;
    public GameObject target;
    public Transform guide;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (holding)
                Release();
            else
                Pickup();
        }

        if (holding && target)
            target.transform.position = guide.position;
    }

    //We can use trigger or Collision
    void OnTriggerEnter(Collider col)
    {
        if (!holding) // if we don't have anything holding
            target = col.gameObject;
    }

    //We can use trigger or Collision
    void OnTriggerExit(Collider col)
    {
        if (!holding)
            target = null;
    }


    private void Pickup()
    {
        if (!target)
            return;

        //We set the object parent to our guide empty object.
        target.transform.SetParent(guide);

        //Set gravity to false while holding it
        target.GetComponent<Rigidbody>().isKinematic = true;

        //we apply the same rotation our main object (Camera) has.
        target.transform.localRotation = transform.rotation;
        //We re-position the ball on our guide object 
        target.transform.position = guide.position;

        holding = true;
    }

    private void Release()
    {
        if (!target)
            return;

        //Set our Gravity to true again.
        target.GetComponent<Rigidbody>().isKinematic = false;
        // we don't have anything to do with our ball field anymore
        target = null;
        //Apply velocity on throwing
        guide.GetChild(0).gameObject.GetComponent<Rigidbody>().velocity = transform.forward * speed;

        //Unparent our ball
        guide.GetChild(0).parent = null;
        holding = false;
    }
}
