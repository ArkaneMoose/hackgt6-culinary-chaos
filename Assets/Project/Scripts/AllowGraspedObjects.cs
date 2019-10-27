using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;

public class AllowGraspedObjects : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        InteractionBehaviour interaction
            = collision.gameObject.GetComponent<InteractionBehaviour>();
        if (interaction && interaction.isGrasped)
        {
            Physics.IgnoreCollision(
                collision.collider,
                GetComponent<Collider>()
            );
        }
    }
}
