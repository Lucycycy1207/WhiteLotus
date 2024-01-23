using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pen : MonoBehaviour
{
    public LineRenderer lineRenderer; 
    public float lineDrawSpeed = 6f; 

    [SerializeField]private List<Transform> dots = new List<Transform>(); 
    private bool isDrawing = false; 

    void Start()
    {
        
        foreach (Transform child in transform)
        {
            dots.Add(child);
        }

        lineRenderer.positionCount = 0; 
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDrawing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopDrawing();
        }

        if (isDrawing)
        {
            DrawLine();
        }
    }

    void StartDrawing()
    {
        isDrawing = true;
        lineRenderer.positionCount = 0; 
        lineRenderer.positionCount = 1; 
        lineRenderer.SetPosition(0, dots[0].position); 
    }

    void DrawLine()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Check if the mouse is over any dot
        for (int i = 1; i < dots.Count; i++)
        {
            if (Vector2.Distance(mousePosition, dots[i].position) < 0.5f)
            {
                // Check if the dot is the next one in the sequence
                if (i == lineRenderer.positionCount)
                {
                    lineRenderer.positionCount++;
                    lineRenderer.SetPosition(i, dots[i].position);
                    break;
                }
            }
        }
    }

    void StopDrawing()
    {
        isDrawing = false;

        
        if (lineRenderer.positionCount == dots.Count)
        {
            Debug.Log("Word spelled correctly!");
            
        }
        else
        {
            
            lineRenderer.positionCount = 0;
        }
    }
}
