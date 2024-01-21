using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Rendering.InspectorCurveEditor;


public class LineController : MonoBehaviour
{
    [SerializeField] private Transform[] targetPoint;
    [SerializeField] private Transform finishPoint;
    //[SerializeField] private Transform leavePoint;

    Queue<Guest> guestQueue;
    public Guest firstGuestInLine { get; private set; }

    private int currGuestNum;

    public event EventHandler OnLineMoveForward;
    public class OnSelectedObjectChangedEventArgs : EventArgs
    {
        public HighlightableObject selectedObjectArg;
    }

    // Start is called before the first frame update
    private void Awake()
    {
        ResetLine();
        guestQueue = new Queue<Guest>();
        firstGuestInLine = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetLine()
    {
        currGuestNum = 0;
    }


    public bool CheckFirstGuestReady()
    {
        //check if it reach the target point
        if (firstGuestInLine.transform.position.x == targetPoint[0].position.x
                && firstGuestInLine.transform.position.z == targetPoint[0].position.z)
        {
            //position ready
            return true;
        }
        return false;

    }

    public void ArrangeNewGuest(Guest _newGuest)
    {
        Debug.Log($"arrange new guest, queueCount: {guestQueue}");
        if (guestQueue.Count < targetPoint.Length)
        {
            
            _newGuest.targetPoint = targetPoint[guestQueue.Count];
            //Debug.Log($"leave point: {leavePoint.position}");

            _newGuest.indexInLine = guestQueue.Count;
            guestQueue.Enqueue(_newGuest);
            Debug.Log($"arrange new guest, queueCount: {guestQueue.Count}");
            
        }
        else
        {
            Debug.Log("exceed guests");
        }
        
        if (guestQueue.Count == 1)
        {
            firstGuestInLine = _newGuest;
        }
        
    }

    public void LeaveLine() // first In First out
    {
        Guest guest = guestQueue.Dequeue();
        guest.finishPoint = finishPoint;

        firstGuestInLine = guestQueue.Peek();

        LineMoveForward();
    }

    private void LineMoveForward()
    {
        Debug.Log("the line need to move forward");
        OnLineMoveForward?.Invoke(this, EventArgs.Empty);
    }

    public void MoveForwardPosInLine(int _indexInLine, Guest _guest)
    {

        int ForwardIndex = _indexInLine - 1;

        //Debug.Log($"the index {ForwardIndex}");
        if (ForwardIndex < 0) { return;  }
        _guest.targetPoint = targetPoint[ForwardIndex];
        _guest.indexInLine--;

    }

    public bool HasLine()
    {
        if (guestQueue != null)
        {
            return true;
        }
        return false;
    }
}
