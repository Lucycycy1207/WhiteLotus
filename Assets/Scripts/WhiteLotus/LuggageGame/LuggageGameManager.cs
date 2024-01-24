using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LuggageGameManager : MonoBehaviour
{
    [SerializeField] private LuggageSpawner luggageSpawner;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private int maxLuggageCount = 3;
    [SerializeField] private float spawnSpeedIncreaseInterval = 20f;
    [SerializeField] private float spawnSpeedIncreaseAmount = 0.1f;
    [SerializeField] private TextMeshProUGUI LuggageUI;

    private float timeSinceLastSpawn;
    private int currentLuggageCount;
    public bool LuggageGameStart;

    private bool finishGame;

    private void Start()
    {
        ResetLuggageGame();
        finishGame = false;
        LuggageUI.enabled = false;
    }

    public void SetGameStatus(bool b)
    {
        LuggageGameStart = b;
        if (LuggageGameStart)
        {
            ShowLuggageUI();
        }
    }
    
    public void ShowLuggageUI()
    {
        LuggageUI.enabled = true;
        StartCoroutine(DisableTextDelayed());


    }

    IEnumerator DisableTextDelayed()
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(2);
        // Disable the UI Text element
        LuggageUI.enabled = false;
    }

    public void FinishGame()
    {
        finishGame = true;
        ResetLuggageGame();
        GameManager.GetInstance().finishGame();
    }



    void Update()
    {
        if (LuggageGameStart)
        {

            // Check if we can spawn more luggage
            if (currentLuggageCount < maxLuggageCount)
            {
                // Update the timer
                timeSinceLastSpawn += Time.deltaTime;

                // Check if enough time has passed to spawn another luggage
                if (timeSinceLastSpawn >= spawnInterval)
                {
                    // Spawn luggage
                    Debug.Log("spawnLuggage");
                    luggageSpawner.SpawnLuggage();

                    // Reset the timer
                    timeSinceLastSpawn = 0f;

                    // Increment luggage count
                    currentLuggageCount++;
                }

            }
        }
    }

    public void ResetLuggageGame()
    {
        //Debug.Log("reset Luggage game");
        LuggageGameStart = false;
        timeSinceLastSpawn = 0;
        currentLuggageCount = 0;
    }

    public void ActivateLuggageGame()
    {
        LuggageGameStart = true;
    }

}
