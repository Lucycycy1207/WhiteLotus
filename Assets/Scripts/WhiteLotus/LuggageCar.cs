using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuggageCar : MonoBehaviour
{
    private int maxLuggage;
    private int currLuggage;

    [SerializeField] private GameObject suitcasePickUp;

    [SerializeField] GameObject[] Luggage;

    private void Start()
    {
        maxLuggage = 3;
        currLuggage = 0;
    }

    public void AddLuggage()
    {
        currLuggage ++;
        Luggage[currLuggage - 1].SetActive(true);

    }

    private void Update()
    {
        if (currLuggage == maxLuggage)
        {
            Debug.Log("Luggage Car is full, do something");
           
            //ResetCar();
            
        }
    }

    private void ResetCar()
    {
        currLuggage = 0;
        for (int i = 0; i < Luggage.Length; i++)
        {
            Luggage[i].SetActive(false);
        }
    }

}
