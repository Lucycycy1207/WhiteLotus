using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : HighlightableObject, ISelectable
{
    [SerializeField] LineController lineController;
    [SerializeField] SceneChanger sceneChanger;

    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject[] gameList;
    private Guest lastGuest;

    Event sceneChange;

    public void OnSelect()
    {
        if (!lineController.CheckFirstGuestReady()) { return; }
        Debug.Log("Collide with Desk");
        Guest guest = lineController.firstGuestInLine;
        if ((lastGuest != null && lastGuest == guest)) return;
        
        lastGuest = guest;
        

        Debug.Log($"Now is serving guest : {guest.name}");
        GameManager.GetInstance().StartNextGame();
        //sceneChanger.ChangeToScene("SignatureScene2");
        
    }
}
