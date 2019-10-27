using UnityEngine;
using UnityEngine.UI;

public class Meat : MonoBehaviour
{
    public GameObject canvas;
    public Image Bar;
    public float Fill;

    public Renderer renderer;
    public Material[] myMaterials = new Material[4];
    private bool cooking = false;
    public GameObject Fire, Smoke;
    void Start()
    {
        Fill = 0f;
        Fire.SetActive(false);
        Smoke.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        canvas.SetActive(cooking);
        if (cooking)
        {
            Fill += Time.deltaTime * 0.05f;
            Bar.fillAmount = Fill;
        }

        if (Fill > 1.1f)
        {
            // Burnt
            renderer.material = myMaterials[3];
            // Activate Fires IF in contact with stove
            Fire.SetActive(cooking);
            Smoke.SetActive(cooking);
            gameObject.tag = "Burnt";
        }
        else if (Fill > 0.50f)
        {
            // Cooked
            renderer.material = myMaterials[2];
            gameObject.tag = "Patty";
        }
        else if (Fill > 0.25f)
        {
            // Cooking
            renderer.material = myMaterials[1];
        }
        else
        {
            // Raw
            renderer.material = myMaterials[0];
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Pan")
        {
            cooking = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Pan")
        {
            cooking = false;
        }
    }
}
