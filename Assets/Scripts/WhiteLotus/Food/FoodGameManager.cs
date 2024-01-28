using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class FoodGameManager : MonoBehaviour
{

    public static FoodGameManager Instance { get; private set; }

    private bool finishGame;
    private bool FoodGameStart;
    [SerializeField] TextMeshProUGUI FoodUI;

    public List<GameObject> TargetItemsArray;

    [SerializeField] public GameObject chosenItem { get; private set; }

    private void Awake()
    {
        SetSingleton();
    }

    private void SetSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }

        Instance = this;
    }



    private void Start()
    {
        ResetFoodGame();
        finishGame = false;
        FoodUI.enabled = false;
        PickMeal();
        FoodGameStart = false;
    }


    void Update()
    {
    }

    //public void CheckFinishedCondition()
    //{
    //    if (PlayerController.Instance.GetPickedItem() == chosenItem)
    //    {
    //        //all item is given to guest
    //        FinishGame();
    //    }
    //}

    public void SetGameStatus(bool b)
    {
        Debug.Log("food game is set, pop UI");
        FoodGameStart = b;

        FoodUI.text = $"Bring <color=#FF0000>{chosenItem.name}</color> to customer";
        FoodUI.enabled = true;

        StartCoroutine(DisableTextDelayed());


    }

    IEnumerator DisableTextDelayed()
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(2);

        // Disable the UI Text element
        FoodUI.enabled = false;
    }

    public void FinishGame()
    {
        Debug.Log("finish amenity game");
        finishGame = true;
        ResetFoodGame();
        
        GameManager.GetInstance().finishGame();

    }


    public void ResetFoodGame()
    {
        FoodGameStart = false;
    }

    public void PickMeal()
    {
        if (TargetItemsArray.Count > 0)
        {
            // Choose a random index from the list
            int randomIndex = Random.Range(0, TargetItemsArray.Count);

            // Get the chosen item
            chosenItem = TargetItemsArray[randomIndex];
            //Debug.Log("Chosen Item: " + chosenItem.name);
        }
    }
}