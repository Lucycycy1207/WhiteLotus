using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sushi : HighlightableObject, IPickable
{
    private Rigidbody sushiRb;

    private Collider sushiCollider;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        sushiRb = GetComponent<Rigidbody>();
        sushiCollider = GetComponent<Collider>();
    }

    public void OnDropped()
    {
        sushiRb.isKinematic = false;
        sushiRb.useGravity = true;
        transform.SetParent(null);
        //make the highlight active
        SetHighlightMode(true);
        sushiCollider.enabled = true;
    }

    public void OnPicked(Transform attachTransform)
    {
        Debug.Log("OnPicked with Sushi");
        //Pickup item

        transform.position = attachTransform.position;
        //transform.localPosition = Vector3.zero;
        transform.rotation = attachTransform.rotation;
        transform.SetParent(attachTransform);

        sushiRb.isKinematic = true;
        sushiRb.useGravity = false;
        sushiCollider.enabled = false;




        //make the highlight inactive
        SetHighlightMode(false);
        SetHighlight(false);

    }
}
