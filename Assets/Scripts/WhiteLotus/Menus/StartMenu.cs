using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private GameObject StartPanel;
    [SerializeField] private GameObject ControlPanel;

    private void Start()
    {
        SwitchToStart();
    }
    public void SwitchToStart()
    {
        ControlPanel.SetActive(false);
        StartPanel.SetActive(true);
    }

    public void SwitchToControl()
    {
        StartPanel.SetActive(false);
        ControlPanel.SetActive(true);
    }

    public void SwitchToExit()
    {
        Debug.Log("exit the game");
        ExitGame();
    }

    // Method to exit the game
    public void ExitGame()
    {
        //this is for exit play mode in unity
        UnityEditor.EditorApplication.isPlaying = false;

        //this one is for published game
        Application.Quit();
    }


}
