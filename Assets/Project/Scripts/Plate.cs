using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;

public class Plate : MonoBehaviour
{
    private IngredientFlag[] ingredientFlags;
    private IDictionary<string, int> ingredientIndexMap;
    private Vector3 platePos;
    public GameObject plate;

    private class IngredientFlag
    {
        public GameObject gameObject;
        public bool isPlaced;
        public float height;
        public float thiccness;
        public IngredientFlag()
        {
            this.isPlaced = false;
            this.thiccness = 0;
            this.height = 0;
        }
        public void setHeight(Vector3 platePos)
        {
            isPlaced = true;
            GameObject template = getTemplate(gameObject);
            Vector3 offset = template.GetComponent<Renderer>().bounds.center;
            gameObject.transform.position = new Vector3(platePos.x - offset.x, platePos.y + height + thiccness - offset.y, platePos.z - offset.z);
            gameObject.transform.rotation = template.transform.rotation;
        }
        public void addHeight(float heightToAdd, Vector3 platePos)
        {
            height += heightToAdd;
            if (isPlaced)
                setHeight(platePos);
        }
    }

    private static GameObject getTemplate(GameObject gameObject)
    {
        return GameObject.Find("Burger Models").transform.Find(gameObject.tag).gameObject;
    }
    private void updateCollisionHeight(float heightToAdd)
    {
        transform.position += new Vector3(0, heightToAdd, 0);
        transform.localScale += new Vector3(0, heightToAdd, 0);
    }

    private void createIngredientFlags(int ingredientCount)
    {
        ingredientFlags = new IngredientFlag[ingredientCount];
        for (int i = 0; i < ingredientFlags.Length; i++)
        {
            ingredientFlags[i] = new IngredientFlag();
        }
    }

    void Start()
    {
        platePos = plate.transform.position;

        ingredientIndexMap = new Dictionary<string, int>();
        ingredientIndexMap.Add("BottomBun", 0);
        ingredientIndexMap.Add("Patty", 1);
        ingredientIndexMap.Add("Cheese", 2);
        ingredientIndexMap.Add("LettuceSlice", 3);
        ingredientIndexMap.Add("TomatoSlice", 4);
        ingredientIndexMap.Add("TopBun", 5);
        createIngredientFlags(ingredientIndexMap.Count);
    }

    private void updateIngredientHeights(GameObject ingredObject)
    {
        int ingredIndex = ingredientIndexMap[ingredObject.tag];
        IngredientFlag ingredient = ingredientFlags[ingredIndex];
        ingredient.gameObject = ingredObject;

        GameObject template = getTemplate(ingredObject);
        if (ingredObject.tag == "TomatoSlice")
        {
            ingredient.thiccness = 0.2f;
        }
        else
        {
            ingredient.thiccness = template.transform.lossyScale.y
                * template.GetComponent<Renderer>().bounds.size.y;
        }

        ingredient.setHeight(platePos);
        updateCollisionHeight(ingredient.thiccness);
        for (int i = ingredIndex + 1; i < ingredientFlags.Length; i++)
        {
            ingredientFlags[i].addHeight(ingredient.thiccness * 2.1f, platePos);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject otherGameObject = other.gameObject;
        string tag = other.tag;
        if (tag == "TopBun" && !ingredientFlags[ingredientIndexMap["BottomBun"]].isPlaced)
        {
            tag = "BottomBun";
            GameObject template = GameObject.Find("Burger Models")
                .transform.Find(tag).gameObject;
            GameObject templated = Instantiate(template, other.transform.parent);
            templated.SetActive(true);
            templated.tag = tag;
            templated.transform.position = other.transform.position;
            templated.transform.rotation = other.transform.rotation;
            Destroy(otherGameObject);
            otherGameObject = templated;
        }

        if (tag != null && ingredientIndexMap.ContainsKey(tag))
        {
            updateIngredientHeights(otherGameObject);
            Destroy(otherGameObject.GetComponent<InteractionBehaviour>());
            Destroy(otherGameObject.GetComponent<Rigidbody>());
        }
        else
        {
            return;
        }

        // returns true if incredient passes check, false otherwise
        if (!Scoring.CurTicket.CheckIngredient(otherGameObject.tag) || Scoring.CurTicket.HasWon)
        {
            if (Scoring.CurTicket.HasWon)
            {
                Scoring.NextTicket();
            }
            else
            {
                Scoring.WrongTicket();
            }

            for (int i = 0; i < ingredientFlags.Length; i++)
            {
                if (ingredientFlags[i].isPlaced)
                {
                    Destroy(ingredientFlags[i].gameObject);
                }
                ingredientFlags[i] = new IngredientFlag();
            }

            transform.localPosition = new Vector3(transform.localPosition.x, 1f, transform.localPosition.z);
            transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
        }
    }
}
