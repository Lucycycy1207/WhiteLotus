using UnityEngine;

public class DrawingManager : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject penPrefab;
    [SerializeField] private LayerMask drawBoardLayer;

    LineRenderer currentLineRenderer;
    Vector3 lastPosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDrawing();
        }
        else if (Input.GetMouseButton(0))
        {
            ContinueDrawing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopDrawing();
        }
    }


    void StartDrawing()
    {
        GameObject penInstance = Instantiate(penPrefab, GetMousePosition(), Quaternion.identity);
        currentLineRenderer = penInstance.GetComponent<LineRenderer>();
        currentLineRenderer.positionCount = 1;
        currentLineRenderer.SetPosition(0, GetMousePosition());
        lastPosition = GetMousePosition();
    }

    void ContinueDrawing()
    {
        if (currentLineRenderer != null)
        {
            Vector3 mousePos = GetMousePosition();
            if (Vector3.Distance(lastPosition, mousePos) > 0.1f) // Optional: Check if the mouse has moved significantly
            {
                currentLineRenderer.positionCount++;
                int positionIndex = currentLineRenderer.positionCount - 1;
                currentLineRenderer.SetPosition(positionIndex, mousePos);
                lastPosition = mousePos;
            }
        }
    }

    void StopDrawing()
    {
        currentLineRenderer = null;
    }

    Vector3 GetMousePosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //Debug.Log(drawBoardLayer.ToString());
        //if (Physics.Raycast(ray,out hit, Mathf.Infinity, drawBoardLayer))

        if (Physics.Raycast(ray, out hit))
        {
           
            return hit.point;
        }

        return Vector3.zero;
    }

    public void ClearDrawing()
    {
        Debug.Log("clear drawing");
        // Destroy all existing drawn lines
        GameObject[] existingLines = GameObject.FindGameObjectsWithTag("Pen");
        foreach (var line in existingLines)
        {
            Destroy(line);
        }
    }
}
