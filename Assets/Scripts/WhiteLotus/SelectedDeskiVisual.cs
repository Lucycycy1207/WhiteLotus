using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedDeskVisual : MonoBehaviour
{
    [SerializeField] private Desk desk;
    [SerializeField] private GameObject visualGameObject;
    private void Start()
    {
        PlayerController.Instance.OnSelectedDeskChanged += PlayerOnSelectedDeskChanged;
    }

    private void PlayerOnSelectedDeskChanged(object sender, PlayerController.OnSelectedDeskChangedEventArgs e)
    {
        if (e.selectedDesk == desk)
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
