using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickSushi : MonoBehaviour, IPickable
{
    Rigidbody sushiRb;

    private void Start()
    {
        sushiRb = GetComponent<Rigidbody>();
    }
    public void OnDropped()
    {
        sushiRb.isKinematic = false;
        sushiRb.useGravity = true;
        transform.SetParent(null);
    }

    public void OnPicked(Transform attachTransform)
    {
        //Pickup item
        transform.position = attachTransform.position;
        transform.rotation = attachTransform.rotation;
        transform.SetParent(attachTransform);

        sushiRb.isKinematic = true;
        sushiRb.useGravity = false;
    }
}
