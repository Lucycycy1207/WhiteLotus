using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour, ISelectable
{
    public virtual void OnSelect()
    {
        //Debug.Log($"Interact with {this.tag}");


        //Pick Up
        if (this.tag == "Luggage")
        {
            Transform LuggageVisualOnHand = PlayerController.Instance.transform.Find("PickUpPos").Find("SuitCasePickUp");
            if (!LuggageVisualOnHand.gameObject.activeSelf)
            {
                LuggageVisualOnHand.gameObject.SetActive(true);
                DestroyWithChildrenRecursive(this.gameObject);
            }

        }

        else if (this.tag == "LuggageCar")
        {
            bool hasLuggage = PlayerController.Instance.transform.Find("PickUpPos").Find("SuitCasePickUp").gameObject.activeSelf;
            if (hasLuggage)
            {
                this.GetComponent<LuggageCar>().AddLuggage();
                Transform LuggageVisualOnHand = PlayerController.Instance.transform.Find("PickUpPos").Find("SuitCasePickUp");
                LuggageVisualOnHand.gameObject.SetActive(false);
            }
        }

        else if (this.tag == "desk")
        {

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
