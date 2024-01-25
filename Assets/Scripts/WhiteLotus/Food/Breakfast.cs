using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakfast : HighlightableObject, IPickable
{
    Rigidbody breakfastRb;

    private Collider breakfastCollider;

    protected override void Start()
    {
        base.Start();
        breakfastRb = GetComponent<Rigidbody>();
    }

    public void OnDropped()
    {
        breakfastRb.isKinematic = false;
        breakfastRb.useGravity = true;
        transform.SetParent(null);
        //make the highlight active
        SetHighlightMode(true);
    }

    public void OnPicked(Transform attachTransform)
    {
        Debug.Log("OnPicked with Breakfast");
        //Pickup item

        transform.position = attachTransform.position;
        //transform.localPosition = Vector3.zero;
        transform.rotation = attachTransform.rotation;
        transform.SetParent(attachTransform);

        breakfastRb.isKinematic = true;
        breakfastRb.useGravity = false;
        
        //make the highlight inactive
        SetHighlightMode(false);
        SetHighlight(false);

    }
}
