using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObjectVisual : MonoBehaviour
{
    [SerializeField] private SelectableObject thisObject;
    [SerializeField] private GameObject visualGameObject;
    private void Start()
    {
        //combine event and function in the  start
        //This means that when the OnSelectedDeskChanged event is triggered, the PlayerOnSelectedDeskChanged method will be called.
        //PlayerController.Instance.OnSelectedObjectChanged += PlayerOnSelectedObjectChanged;
    }

    private void PlayerOnSelectedObjectChanged(object sender, PlayerController.OnSelectedObjectChangedEventArgs e)
    {
        if (visualGameObject == null) { return; }

        if (e.selectedObjectArg == thisObject)
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
