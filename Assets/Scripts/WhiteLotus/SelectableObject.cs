using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    private string tag;

    public void Interact(string _tag)
    {
        tag = _tag;
        Debug.Log($"Interact with {tag}");


        //Pick Up
        if (tag == "Luggage")
        {
            Transform LuggageVisualOnHand = PlayerController.Instance.transform.Find("PickUpPos").Find("SuitCasePickUp");
            if (!LuggageVisualOnHand.gameObject.activeSelf)
            {
                LuggageVisualOnHand.gameObject.SetActive(true);
                DestroyWithChildrenRecursive(this.gameObject);
            }

        }

        else if (tag == "LuggageCar")
        {
            bool hasLuggage = PlayerController.Instance.transform.Find("PickUpPos").Find("SuitCasePickUp").gameObject.activeSelf;
            if (hasLuggage)
            {
                this.GetComponent<LuggageCar>().AddLuggage();
                Transform LuggageVisualOnHand = PlayerController.Instance.transform.Find("PickUpPos").Find("SuitCasePickUp");
                LuggageVisualOnHand.gameObject.SetActive(false);
            }
        }
    }
    void DestroyWithChildrenRecursive(GameObject obj)
    {
        // Iterate through all child objects
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Destroy the current game object
        Destroy(obj);
    }




}
