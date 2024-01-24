using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickLuggage : MonoBehaviour, IPickable
{
    FixedJoint joint;
    Rigidbody luggageRb;

    private void Start()
    {
        luggageRb = GetComponent<Rigidbody>();
    }
    public void OnDropped()
    {
        luggageRb.isKinematic = false;
        luggageRb.useGravity = true;
        transform.SetParent(null);
    }

    public void OnPicked(Transform attachTransform)
    {
        //Pickup item
        transform.position = attachTransform.position;
        transform.rotation = attachTransform.rotation;
        transform.SetParent(attachTransform);

        luggageRb.isKinematic = true;
        luggageRb.useGravity = false;
    }


}