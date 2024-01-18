using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuggageCar : HighlightableObject, ISelectable
{

    private int maxLuggage;
    private int currLuggage;
    [SerializeField] AudioSource putLuggageAudio;
    


    //[SerializeField] private GameObject suitcasePickUp;

    [SerializeField] GameObject[] Luggage;

    public void OnSelect()
    {
        Debug.Log("OnSelect with LuggageCar");
        if (PlayerController.Instance.isPickingSomething)
        {
            //assume the pickedItem is luggage
            GameObject item = PlayerController.Instance.GetPickedItem();
            if (item.TryGetComponent<Luggage>(out Luggage l))
            {
                Destroy(item);
                PlayerController.Instance.SetPickedItem(null);
                AddLuggage();
            }
        }
    }


    protected override void Start()
    {
        base.Start();
        maxLuggage = 3;
        currLuggage = 0;
        
        for (int i = 0; i < Luggage.Length; i++)
        {
            Luggage[i].SetActive(false);
        }

    }

    public void AddLuggage()
    {
        Debug.Log("add luggage sound");
        putLuggageAudio.time = 0;
        putLuggageAudio.Play();
        currLuggage ++;
        Luggage[currLuggage - 1].SetActive(true);

    }

    private void Update()
    {
        if (putLuggageAudio.time > 1)
        {
            putLuggageAudio.Stop();
            
        }
        if (currLuggage == maxLuggage)
        {
            Debug.Log("Luggage Car is full, do something");
           
            ResetCar();
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
