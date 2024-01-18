using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : HighlightableObject, ISelectable
{
    public void OnSelect()
    {
        Debug.Log("OnSelect with Computer");
    }
}
