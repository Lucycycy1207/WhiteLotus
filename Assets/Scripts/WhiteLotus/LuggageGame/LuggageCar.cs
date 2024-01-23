using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuggageCar : HighlightableObject, ISelectable
{
    [SerializeField] LuggageGameManager luggageGameManager;
    private int maxLuggage;
    private int currLuggage;
    [SerializeField] AudioSource putLuggageAudio;
    [SerializeField] AudioSource TaskFinishAudio;


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
        putLuggageAudio.enabled = false;
        TaskFinishAudio.enabled = false;
        maxLuggage = 3;
        currLuggage = 0;
        
        for (int i = 0; i < Luggage.Length; i++)
        {
            Luggage[i].SetActive(false);
        }

    }

    public void AddLuggage()
    {
        //Debug.Log("add luggage sound");
        putLuggageAudio.enabled = true;
        putLuggageAudio.time = 0;
        putLuggageAudio.Play();
        float playTime = 0.3f;
        putLuggageAudio.SetScheduledEndTime(AudioSettings.dspTime + playTime);

        currLuggage++;
        Luggage[currLuggage - 1].SetActive(true);

        CheckFull();

    }

    private void CheckFull()
    {
        if (currLuggage == maxLuggage)
        {
            //Debug.Log("Luggage Car is full, do something");
            float delayTime = 0.5f;
            Invoke(nameof(CompleteGame), delayTime);
        }
    }


    private void CompleteGame()
    {
        PlayFinishAudio();

        ResetCar();
        luggageGameManager.FinishGame();

    }

    private void PlayFinishAudio()
    {
        //Debug.Log("finish");
        TaskFinishAudio.enabled = true;

        TaskFinishAudio.Play();
        float playTime = 0.5f;
        TaskFinishAudio.SetScheduledEndTime(AudioSettings.dspTime + playTime);
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
