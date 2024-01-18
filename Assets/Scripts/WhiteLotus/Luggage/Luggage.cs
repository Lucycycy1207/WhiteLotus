using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luggage : HighlightableObject, IPickable
{
    FixedJoint joint;
    Rigidbody luggageRb;

    protected override void Start()
    {
        base.Start();
        luggageRb = GetComponent<Rigidbody>();
    }

    public void OnDropped()
    {
        luggageRb.isKinematic = false;
        luggageRb.useGravity = true;
        transform.SetParent(null);
        //make the highlight active
        SetHighlightMode(true);
    }

    public void OnPicked(Transform attachTransform)
    {
        Debug.Log("OnPicked with luggage");
        //Pickup item
        transform.position = attachTransform.position;
        transform.rotation = attachTransform.rotation;
        transform.Rotate(0, 90, 0);
        transform.SetParent(attachTransform);

        //make the highlight inactive
        SetHighlightMode(false);
        SetHighlight(false);
        luggageRb.isKinematic = true;
        luggageRb.useGravity = false;
    }


}
