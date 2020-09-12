using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingControler : MonoBehaviour
{
    [SerializeField]
    bool isDrawing = false, isErasing = false;
    [SerializeField]
    Point previousPoint, ancestorPoint;
    public GameObject pointPref, linePref;
    public Color lineColor;
    [SerializeField]
    public Line currentLine, tempLine;

    public void Start()
    {
        tempLine = Instantiate(linePref).AddComponent<Line>();
        tempLine.IninitateLine(Vector2.zero, Vector2.zero, lineColor, 0.045f);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDrawing();
        }
        if (Input.GetMouseButtonUp(0))
        {
            EndDrawing();
        }

        if (Input.GetMouseButtonDown(1))
        {
            StartErasing();
        }
        if (Input.GetMouseButtonUp(1))
        {
            StopErasing();
        }

        SetTmpLine();
    }

    public void SetTmpLine()
    {
        tempLine.gameObject.SetActive(isDrawing);
        if (isDrawing && previousPoint != null)
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tempLine.SetNewPosition(previousPoint.transform.position, position);
        }
    }
    public void MouseOverPoint(DrawingPoint drawingPoint)
    {
        if (isErasing)
        {
            drawingPoint.ErasePoint();
        }
        if(isDrawing == true && (drawingPoint.currentPoint != previousPoint|| previousPoint == null))
        {
            AddPoint(drawingPoint);
        }
    }
    void AddPoint(DrawingPoint drawingPoint)
    {
        if (previousPoint != null && ancestorPoint != null)
        {
            if (Drawer.AreOnLine(ancestorPoint.transform, previousPoint.transform, drawingPoint.transform))
            {
                Points.points++;
            }
        }

        if (drawingPoint.currentPoint != null)
        {
            if (previousPoint != null)
            {
                Extend(drawingPoint.currentPoint);
            }
            previousPoint = drawingPoint.currentPoint;
        }
        else
        {
            Point newPoint = drawingPoint.CreatePoint();
            if (previousPoint != null)
            {
                Extend(newPoint);
            }
            previousPoint = newPoint;
        }

    }

    public void Extend(Point point)
    {     
        if (previousPoint.AreOnLine(point))
        {
            Destroy(previousPoint.gameObject);
            if (currentLine != null)
            {
                Destroy(currentLine.gameObject);
            }
            currentLine = Drawer.drawer.CreateConnection(ancestorPoint, point);
        }
        else
        {
            currentLine = Drawer.drawer.CreateConnection(previousPoint, point);
            ancestorPoint = previousPoint;
        }
        
    }

    public Point CreatePoint(DrawingPoint drawingPoint)
    {
        GameObject newPoint = Instantiate(pointPref);
        newPoint.transform.position = drawingPoint.transform.position;
        Point newPointScript = newPoint.GetComponent<Point>();
        drawingPoint.currentPoint = newPointScript;
        return newPointScript;
    }

    void StartDrawing()
    {
        isDrawing = true;
        isErasing = false;
    }

    void EndDrawing()
    {
        isDrawing = false;

        if(previousPoint != null && ancestorPoint == null && previousPoint.neighbours.Count == 0)
        {
            previousPoint.gameObject.AddComponent<RepulsedBody>();
        }
        previousPoint = null;
        ancestorPoint = null;
        currentLine = null;
    }

    void StartErasing()
    {
        isErasing = true;
        isDrawing = false;
    }

    void StopErasing()
    {
        isErasing = false;
    }
}
