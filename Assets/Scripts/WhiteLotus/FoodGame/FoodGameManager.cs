using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class FoodGameManager : MonoBehaviour
{
    public static FoodGameManager Instance {  get; private set; }

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
    }


    void Update()
    {

    }

    public void CheckFinishedCondition()
    {
        if (PlayerController.Instance.GetPickedItem() is GameObject chosenItem)
        {
            //all item is given to guest
            FinishGame();
        }
        else return;
    }

    public void SetGameStatus(bool b)
    {
        FoodGameStart = b;

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
        finishGame = true;
        ResetFoodGame();
        Debug.Log("finish amenity game");
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
            int randomIndex = UnityEngine.Random.Range(0, TargetItemsArray.Count);

            // Get the chosen item
            GameObject chosenItem = TargetItemsArray[randomIndex];
            Debug.Log("Chosen Item: " + chosenItem.name);
        }
    }
}
