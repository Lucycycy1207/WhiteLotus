using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A customizable component that unlocks a door when picked up by the player. 
/// Use triggers and destroy the key (the GameObject with the compoent) when picked up. 
/// This component needs to be used for other doors and should not be just limited to unlocking doors.
/// </summary>
public class DoorKey : MonoBehaviour
{
    public UnityEvent ActiveDoor;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("pick up key");
            ActiveDoor.Invoke();
            Destroy(gameObject);
        }
    }
}
