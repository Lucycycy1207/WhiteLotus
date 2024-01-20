using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LineController : MonoBehaviour
{
    [SerializeField] private Transform[] targetPoint;
    [SerializeField] private Transform finishPoint;

    Queue<Guest> guestQueue;
    private int currGuestNum;

    public event EventHandler OnLineMoveForward;
    public class OnSelectedObjectChangedEventArgs : EventArgs
    {
        public HighlightableObject selectedObjectArg;
    }


    // Start is called before the first frame update
    void Start()
    {
        ResetLine();
        guestQueue = new Queue<Guest>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetLine()
    {
        currGuestNum = 0;
    }

    public void ArrangeNewGuest(Guest _newGuest)
    {
        
        if (guestQueue.Count < targetPoint.Length)
        {
            
            _newGuest.targetPoint = targetPoint[guestQueue.Count];
            _newGuest.indexInLine = guestQueue.Count;
            guestQueue.Enqueue(_newGuest);
            Debug.Log($"arrange new guest, queueCount: {guestQueue.Count}, pointCount: {targetPoint.Length}");
        }
        else
        {

        }
        
    }

    public void LeaveLine() // first In First out
    {

        Guest guest = guestQueue.Dequeue();
        guest.finishPoint = finishPoint;
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

        Debug.Log($"the index {ForwardIndex}");
        if (ForwardIndex < 0) { return;  }
        _guest.targetPoint = targetPoint[ForwardIndex];
        _guest.indexInLine--;

    }
}
