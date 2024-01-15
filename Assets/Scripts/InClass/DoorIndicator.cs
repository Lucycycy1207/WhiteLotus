using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This should be a customizable component that changes the state based on an event. 
/// Imagine a light which changes the color based on a key being picked up or a cube being put on a pressure pad etc.
/// </summary>
public class DoorIndicator : MonoBehaviour
{

    [SerializeField] private Collider doorCollider;
    [SerializeField] private Renderer IndicatorRenderer;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color ActiveColor;

    private void Awake()
    {
        doorCollider.enabled = false;
        IndicatorRenderer.material.color = defaultColor;
    }

    public void ActiveDoor()
    {
        Debug.Log("active door");
        doorCollider.enabled = true;
        IndicatorRenderer.material.color = ActiveColor;
    }
}
