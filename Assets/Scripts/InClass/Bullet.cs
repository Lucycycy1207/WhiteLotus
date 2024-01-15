using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        IDestroyable destroyable = collision.gameObject.GetComponent<IDestroyable>();
        if (destroyable != null)
        {
            Debug.Log(destroyable.ToString() + " by " + this.gameObject.ToString());
            destroyable.OnCollided();
            Destroy(gameObject);
        }
        
    }
}
