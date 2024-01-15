using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButton : MonoBehaviour, ISelectable
{
    [SerializeField] private Renderer buttonRenderer;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color hoverColor;

    public UnityEvent OnPushButton;
    public void OnHoverEnter()// Û±Í–¸Õ£
    {
        buttonRenderer.material.color = hoverColor;
    }

    public void OnHoverExit()
    {
        buttonRenderer.material.color = defaultColor;
    }


    /// <summary>
    /// 
    /// called by playerController, Press L to trigger
    /// </summary>
    public void OnSelect()
    {
        OnPushButton.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
