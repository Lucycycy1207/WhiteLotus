using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator doorAnim;

    private float timer = 0;
    [SerializeField] private Renderer doorRenderer;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color activeColor;

    [SerializeField] private float waitTime = 1.0f;

    private void Awake()
    {
        //get Door animator at the start
        doorAnim = GetComponent<Animator> ();  

        //set door to default color
        if (doorRenderer != null)
        {
            doorRenderer.material.color = defaultColor;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //detect player,set color to active
        if (other.CompareTag("Player"))
        {
            timer = 0;
            doorRenderer.material.color = activeColor;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) { return; } //Guard clause to exit if not player
        timer += Time.deltaTime;

        if (timer >= waitTime)
        {
            timer = waitTime;//increase timer value
            doorAnim.SetBool("isDoorOpen", true);
            waitTime *= 1.5f;//increase waitTime value
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnim.SetBool("isDoorOpen", false);
            doorRenderer.material.color = defaultColor;
        }
    }

}
