using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlidingDoor : MonoBehaviour
{
    public string tagToOpenFor;
    public GameObject door;
    public Image image;
    public float step = 0.01f;

    private int triggered = 0;
    private float progress = 1f;

    // Start is called before the first frame update
    void Start()
    {
    }

    void FixedUpdate()
    {
        progress += (triggered != 0 ? -1 : 1) * step;
        if (progress < 0f)
        {
            progress = 0f;
        }
        else if (progress > 1f)
        {
            progress = 1f;
        }

        Vector3 translation = door.transform.localPosition;
        translation.x = -0.5f + 0.5f * progress;
        door.transform.localPosition = translation;
        Vector3 scale = door.transform.localScale;
        scale.x = progress;
        door.transform.localScale = scale;
        image.fillAmount = progress;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == tagToOpenFor)
        {
            triggered++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == tagToOpenFor)
        {
            triggered--;
        }
    }
}
