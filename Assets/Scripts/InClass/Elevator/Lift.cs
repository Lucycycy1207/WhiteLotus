using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField] private float moveDistance;
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool isUp;
    [SerializeField] private Door door;

    
    private bool isMoving;
    private Vector3 targetPosition;

    private PlayerController player;


    /// <summary>
    /// return 1: is Moving; 0: Not moving.
    /// </summary>
    /// <returns></returns>
    public bool GetLiftMoveMovement()
    {
        return isMoving;
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();

    }
    public void ToggleLift()
    {
        //Can't toggle when lift is moving
        if (isMoving) { return; }

        //These two lines try to make player transform follow the lift, otherwise player is stretchy
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.SetParent(this.transform); 

        if (isUp)//can go up status
        {
            targetPosition = transform.localPosition - new Vector3(0, moveDistance, 0);
            isUp = false;
        }
        else//can go down status
        {
            targetPosition = transform.localPosition + new Vector3(0, moveDistance, 0);
            isUp = true;
        }

        isMoving = true;
        
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        //Player press L to toggle lift
        if (Input.GetKeyDown(KeyCode.L))
        {
            ToggleLift();
        }

        if (isMoving)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, 
                targetPosition, moveSpeed * Time.fixedDeltaTime);
        }
        if (Vector3.Distance(transform.localPosition, targetPosition) < 0.02f)
        {
            isMoving = false;
            player.GetComponent<CharacterController>().enabled = true;
            player.transform.SetParent(null);
        }
    }
}
