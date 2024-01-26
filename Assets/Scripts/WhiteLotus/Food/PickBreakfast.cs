using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickBreakfast : MonoBehaviour, IPickable
{
    Rigidbody breakfastRb;

    private void Start()
    {
        breakfastRb = GetComponent<Rigidbody>();
    }
    public void OnDropped()
    {
        breakfastRb.isKinematic = false;
        breakfastRb.useGravity = true;
        transform.SetParent(null);
    }

    public void OnPicked(Transform attachTransform)
    {
        //Pickup item
        transform.position = attachTransform.position;
        transform.rotation = attachTransform.rotation;
        transform.SetParent(attachTransform);

        breakfastRb.isKinematic = true;
        breakfastRb.useGravity = false;
    }


}
