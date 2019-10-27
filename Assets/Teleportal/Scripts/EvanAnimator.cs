using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Teleportal;

public class EvanAnimator : MonoBehaviour
{
    public TPUser tpuser;

    void Start()
    {
        if (this.tpuser.isSelfUser) {
            this.enabled = false;
        }
    }

    void Update()
    {
        transform.position += new Vector3(0.0f, (0.01f * Mathf.Sin(Time.time * 4)), 0.0f);
    }
}
