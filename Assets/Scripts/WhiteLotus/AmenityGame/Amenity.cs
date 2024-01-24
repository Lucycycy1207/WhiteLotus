using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amenity : HighlightableObject, IPickable
{
    FixedJoint joint;
    private Rigidbody amenityRb;
    private Collider amenityCollider;

    protected override void Start()
    {
        base.Start();
        amenityRb = GetComponent<Rigidbody>();
        amenityCollider = GetComponent<Collider>();
    }

    public void OnDropped()
    {
        if (LineController.Instance.firstGuestInLine.isHighlight())
        {
            Debug.Log("lock droping amenity item");
            return;
        }

        Debug.Log($"OnDropped with {this.gameObject.name}");
        amenityRb.isKinematic = false;
        amenityRb.useGravity = true;
        transform.SetParent(null);

        amenityCollider.enabled = true;

        //make the highlight active
        SetHighlightMode(true);
    }


    public void OnPicked(Transform attachTransform)
    {
        Debug.Log($"OnPicked with {this.gameObject.name}");
        //Pickup item

        transform.position = attachTransform.position;
        //transform.localPosition = Vector3.zero;
        transform.rotation = attachTransform.rotation;
        transform.SetParent(attachTransform);

        amenityRb.isKinematic = true;
        amenityRb.useGravity = false;

        amenityCollider.enabled = false;
        //transform.Rotate(0, 90, 0);



        //make the highlight inactive
        SetHighlightMode(false);
        SetHighlight(false);

    }
}
