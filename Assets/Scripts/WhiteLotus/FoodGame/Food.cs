using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : HighlightableObject, IPickable
{
    private Rigidbody foodRb;

    private Collider foodCollider;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        foodRb = GetComponent<Rigidbody>();
        foodCollider = GetComponent<Collider>();
    }

    public void OnDropped()
    {
        foodRb.isKinematic = false;
        foodRb.useGravity = true;
        transform.SetParent(null);
        //make the highlight active
        SetHighlightMode(true);
        foodCollider.enabled = true;
    }

    public void OnPicked(Transform attachTransform)
    {
        Debug.Log("OnPicked with Food");
        //Pickup item

        transform.position = attachTransform.position;
        //transform.localPosition = Vector3.zero;
        transform.rotation = attachTransform.rotation;
        transform.SetParent(attachTransform);

        foodRb.isKinematic = true;
        foodRb.useGravity = false;
        foodCollider.enabled = false;




        //make the highlight inactive
        SetHighlightMode(false);
        SetHighlight(false);

    }
}
