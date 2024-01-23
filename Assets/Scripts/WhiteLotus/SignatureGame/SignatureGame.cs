using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignatureGame : MonoBehaviour
{
    private Transform content;
    private bool finishGame;
    // Start is called before the first frame update
    void Start()
    {
        content = this.gameObject.transform.GetChild(0);
        content.gameObject.SetActive(false);
        finishGame = false;

    }


    public void FinishSignatureGame()
    {
        finishGame = true;
        SetGameStatus(false);
        GameManager.GetInstance().finishGame();

    }


    public void SetGameStatus(bool b)
    {
        if (b)
        {
            content.gameObject.SetActive(true);
        }
        else
        {
            content.gameObject.SetActive(false);
        }
    }
}
