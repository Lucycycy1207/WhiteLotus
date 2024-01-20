using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guest : MonoBehaviour
{
    [SerializeField] private LineController lineController;

    public Transform targetPoint { get ; set; }

    public Transform finishPoint { get; set; }

    public int indexInLine { get; set; }

    private NavMeshAgent agent;
    private Guest guest;

    public bool taskComplete;

    private bool inLine;


    private void Awake()
    {
        guest = this.GetComponent<Guest>();
    }
    // Start is called before the first frame update
    void Start()
    {
        lineController.OnLineMoveForward += MoveInLine;
        agent = GetComponent<NavMeshAgent>();
        lineController.ArrangeNewGuest(guest);
        taskComplete = false;
        inLine = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPoint == null) { return;  }
        

        if (taskComplete)
        {
            if (inLine)
            {
                lineController.LeaveLine();
                inLine = false;
            }
            agent.destination = finishPoint.position;


            //check if guest arrive the finish Point, destroy it.
            if (this.transform.position.x == finishPoint.position.x
             && this.transform.position.z == finishPoint.position.z)
            {
                Destroy(this.gameObject);
            }
            
        }
        else
        {
            
            agent.destination = targetPoint.position;

        }




    }

    public void MoveInLine(object sender, System.EventArgs e)
    {
        if (inLine)
        {
            Debug.Log($"try to move {guest.name} at index {indexInLine}");
            lineController.MoveForwardPosInLine(indexInLine, guest);
        }
        
    }

}
