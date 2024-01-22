using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guest : MonoBehaviour
{
    [SerializeField] private LineController lineController;

    public Transform targetPoint { get ; set; }// line position

    public Transform finishPoint { get; set; }// guest finish task position
    private Vector3 leavePoint;
    public int indexInLine { get; set; } // position in line start from 0

    [SerializeField] private MoodBar moodBar;


    private NavMeshAgent agent;
    private Guest guest;

    public bool taskComplete;

    private bool inLine;
    private bool isLeaving;


    private void Awake()
    {
        guest = this.GetComponent<Guest>();
        moodBar.SetGuest(guest);
    }
    // Start is called before the first frame update
    void Start()
    {
        lineController.OnLineMoveForward += MoveInLine;
        agent = GetComponent<NavMeshAgent>();
        lineController.ArrangeNewGuest(guest);
        taskComplete = false;
        inLine = true;
        isLeaving = false;
        leavePoint = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPoint == null) { return;  }
        
        if (isLeaving)
        {
            if (leavePoint == null) return;
            //Debug.Log("guest should do the leaving action");
            //Debug.Log(leavePoint.position);
            agent.destination = leavePoint;
            //check if guest arrive the finish Point, destroy it.
            if (this.transform.position.x == leavePoint.x
             && this.transform.position.z == leavePoint.z)
            {
                Destroy(this.gameObject);
            }
        }

        else if (taskComplete)
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

    public void Leave()
    {
        isLeaving = true;
        moodBar.PauseMoodBar();
    }

    public void MoveInLine(object sender, System.EventArgs e)
    {
        if (inLine)
        {
            //Debug.Log($"try to move {guest.name} at index {indexInLine}");
            lineController.MoveForwardPosInLine(indexInLine, guest);
        }
        
    }

}
