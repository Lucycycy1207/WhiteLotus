using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightableObject : MonoBehaviour, IHighlightable
{

    private GameObject SelectedVisual;
    private bool isActive;
    private bool isHighLight;
    public void Highlight()
    {
        SelectedVisual.SetActive(true);
        isHighLight = true;
    }

    public void UnHighlight()
    {
        SelectedVisual.SetActive(false);
        isHighLight = false;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        SelectedVisual = this.transform.GetChild(1).GetChild(0).gameObject;
        SelectedVisual.SetActive(false);
        isHighLight = false;
        isActive = true;


        //combine event and function in the  start
        //This means that when the OnSelectedDeskChanged event is triggered, the PlayerOnSelectedDeskChanged method will be called.
        PlayerController.Instance.OnSelectedObjectChanged += PlayerOnSelectedObjectChanged;
    }
    private void PlayerOnSelectedObjectChanged(object sender, PlayerController.OnSelectedObjectChangedEventArgs e)
    {
        
        if (SelectedVisual == null || isActive == false) {
            
            //Debug.Log($"none visual {this.gameObject.name}");
            return; }

        if (e.selectedObjectArg == this)
        {
            //Debug.Log($"event recieved for highlightable object{this.gameObject.name}");
            //Debug.Log($"SelectedVisual: {SelectedVisual.name}");
            Highlight();
        }
        else
        {
            //Debug.Log($"unhighlight: {SelectedVisual.name}");
            UnHighlight();
        }
    }

    public void SetHighlightMode(bool b)
    {
        isActive = b;
    }

    public void SetHighlight(bool b)
    {
        SelectedVisual.SetActive(b);
    }

    public bool isHighlight()
    {
        return isHighLight;
    }



}
