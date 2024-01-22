using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;


    [SerializeField] SignatureGame signatureGame;
    [SerializeField] LuggageGameManager luggageGameManager;
    [SerializeField] LineController lineController;

    private int currGame;
    

    //1. sign game.
    //2. luggage game;

    // Start is called before the first frame update
    void Start()
    {
        currGame = 0;
    }

// Update is called once per frame
    void Update()
    {
    }

    public void finishGame()
    {
        Debug.Log("finish game detected");
        lineController.SetFirstGuestComplete();
    }

    public void StartNextGame()
    {
        if (currGame == 0)
        {
            Debug.Log("start signature game");
            signatureGame.SetGameStatus(true);
            currGame++;
        }
        else if (currGame == 1)
        {
            Debug.Log("start luggage game");
            luggageGameManager.SetGameStatus(true);
            currGame++;
        }
        else if (currGame == 2)
        {
            Debug.Log("continue");
        }

    }
    private void Awake()
    {
        SetSingleton();
    }

    public static GameManager GetInstance()
    {
        return instance;
    }
    private void SetSingleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
    }
}
