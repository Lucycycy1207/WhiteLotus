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
    [SerializeField] AmenityGameManager amenityGameManager;
    [SerializeField] FoodGameManager foodGameManager;


    public Game currGame { get; private set; }


    //1. sign game.
    //2. luggage game;

    // Start is called before the first frame update
    void Start()
    {
        currGame = Game.NoGame;
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
        if (currGame == Game.NoGame)
        {
            Debug.Log("start signature game");
            signatureGame.SetGameStatus(true);
            currGame++;
        }
        else if (currGame == Game.Signature)
        {
            Debug.Log("start luggage game");
            luggageGameManager.SetGameStatus(true);
            currGame++;
        }
        else if (currGame == Game.Luggage)
        {
            Debug.Log("start Amenity game");
            amenityGameManager.SetGameStatus(true);
            currGame++;
        }
        else if (currGame == Game.Amenity)
        {
            Debug.Log("start Food game");
            foodGameManager.SetGameStatus(true);
            currGame++;
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