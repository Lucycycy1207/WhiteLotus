using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : HighlightableObject, ISelectable
{
    [SerializeField] LineController lineController;
    [SerializeField] SceneChanger sceneChanger;

    [SerializeField] GameManager gameManager;
    private Guest lastGuest;

    Event sceneChange;

    public void OnSelect()
    {
        Debug.Log("Collide with Desk");
        if (!lineController.CheckFirstGuestReady()) { return; }
        Debug.Log("here");
        Guest guest = lineController.firstGuestInLine;
        Debug.Log("There");
        if ((lastGuest != null && lastGuest == guest)) return;
        
        lastGuest = guest;
        

        Debug.Log($"Now is serving guest : {guest.name}");
        GameManager.GetInstance().StartNextGame();
        //sceneChanger.ChangeToScene("SignatureScene2");
        
    }
}
