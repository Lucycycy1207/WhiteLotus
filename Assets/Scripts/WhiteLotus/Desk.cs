using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : HighlightableObject, ISelectable
{
    [SerializeField] LineController lineController;
    
    public void OnSelect()
    {
        if (!lineController.CheckFirstGuestReady()) { return; }
        //Debug.Log("Collide with Desk");
        Guest guest = lineController.firstGuestInLine;

        Debug.Log($"Now is serving guest : {guest.name}");
    }
}
