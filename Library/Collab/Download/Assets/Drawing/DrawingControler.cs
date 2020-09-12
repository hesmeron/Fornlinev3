using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingControler : MonoBehaviour
{
    public bool isDrawing = false, isErasing = false;
    public Point previousPoint, ancestorPoint;
    public GameObject pointPref, linePref;
    [SerializeField]
    Vector2 previousDirection = Vector2.zero;
    public Color lineColor;
    public Line currentLine;
    public Line tempLine;

    public void Start()
    {
        Debug.Log("Start");
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
        Debug.Log("MoseOver");
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
            Point newPoint = CreatePoint(drawingPoint);
            if (previousPoint != null)
            {
                Extend(newPoint);
            }
            previousPoint = newPoint;
        }

    }

    public void Extend(Point point)
    {
        if (IsOnLine(point))
        {
            Destroy(previousPoint.gameObject);
            if (currentLine != null)
            {
                Destroy(currentLine.gameObject);
            }
            CreateConnection(ancestorPoint, point);
        }
        else
        {
            CreateConnection(previousPoint, point);
            ancestorPoint = previousPoint;
        }
        
    }

    bool IsOnLine(Point point)
    {
        Vector2 newDirection = (Vector2)(point.transform.position - previousPoint.transform.position);
        float angle = Vector2.Angle(newDirection, previousDirection);
        bool onLine = (angle == 0 || angle == 180) && ancestorPoint != null && ancestorPoint != previousPoint && ancestorPoint != point && previousPoint.lines.Count < 2;
        previousDirection = newDirection;
        Debug.Log(onLine);
        return onLine;

    }
    public Point CreatePoint(DrawingPoint drawingPoint)
    {
        GameObject newPoint = Instantiate(pointPref);
        newPoint.transform.position = drawingPoint.transform.position;
        Point newPointScript = newPoint.GetComponent<Point>();
        drawingPoint.currentPoint = newPointScript;
        return newPointScript;
    }

    void CreateConnection(Point from, Point to)
    {
        Line line = Instantiate(linePref, this.transform).AddComponent<Line>();
        line.gameObject.AddComponent<LineCollider>().line = line;
        line.IninitateLine(from.transform.position, to.transform.position, lineColor, 0.045f);
        from.AddNeighbour(line, to);
        to.AddNeighbour(line, from);
        currentLine = line;
    }

    void StartDrawing()
    {
        isDrawing = true;
        isErasing = false;
    }

    void EndDrawing()
    {
        isDrawing = false;
        previousPoint = null;
        previousDirection = Vector2.zero;
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
