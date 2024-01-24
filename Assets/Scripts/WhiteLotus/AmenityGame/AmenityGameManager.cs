using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

public class AmenityGameManager : MonoBehaviour
{
    public static AmenityGameManager Instance { get; private set; }

    private bool finishGame;
    private bool AmenityGameStart;
    [SerializeField] TextMeshProUGUI AmenityUI;



    [SerializeField] private string[] TargetItemsArray;
    public List<string> TargetItemList { get; private set; }

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
        ResetAmenityGame();
        finishGame = false;
        AmenityUI.enabled = false;
        TargetItemList = TargetItemsArray.ToList();
}


    void Update()
    {
        
    }
        
    public void CheckFinishedCondition()
    {
        if (TargetItemList.Count() == 0)
        {
            //all item is given to guest
            FinishGame();
        }
    }

    public void SetGameStatus(bool b)
    {
        AmenityGameStart = b;

        AmenityUI.enabled = true;

        StartCoroutine(DisableTextDelayed());

        
    }

    IEnumerator DisableTextDelayed()
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(2);

        // Disable the UI Text element
        AmenityUI.enabled = false;
    }

    public void FinishGame()
    {
        finishGame = true;
        ResetAmenityGame();
        Debug.Log("finish amenity game");
        GameManager.GetInstance().finishGame();
    }

    public void ResetAmenityGame()
    {
        AmenityGameStart = false;
    }
}
