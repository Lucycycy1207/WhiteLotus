using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedLuggageVisual : MonoBehaviour
{
    [SerializeField] private Luggage Luggage;
    [SerializeField] private GameObject visualGameObject;
    private void Start()
    {
        PlayerController.Instance.OnSelectedDeskChanged += PlayerOnSelectedDeskChanged;
    }

    private void PlayerOnSelectedDeskChanged(object sender, PlayerController.OnSelectedDeskChangedEventArgs e)
    {
        if (e.selectedDesk == Luggage)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        visualGameObject.SetActive(true);
    }
    private void Hide()
    {
        visualGameObject.SetActive(false);
    }
}
