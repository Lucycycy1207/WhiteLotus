using UnityEngine;
using UnityEngine.Events;

public class SelectObject : MonoBehaviour, ISelectable
{
    [SerializeField] private Renderer objectRenderer;
    [SerializeField] private Renderer OnHoverRenderer;

    //select current task to a minigame
    public UnityEvent OnSelectObject;


    public void OnHoverEnter()
    {
        //change color by enable child renderer
        Debug.Log("OnHoverEnter");
        OnHoverRenderer.enabled = true;
    }

    public void OnHoverExit()
    {
        Debug.Log("OnHoverExit");
        OnHoverRenderer.enabled = false;
    }

    public void OnSelect()
    {
        OnSelectObject?.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (OnHoverRenderer != null)
            OnHoverRenderer.enabled = true;
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }
}
