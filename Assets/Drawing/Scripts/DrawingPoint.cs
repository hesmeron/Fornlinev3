using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingPoint : MonoBehaviour
{
    DrawingControler drawingControler;
    public Point currentPoint = null;
    public List<Connection> connections = new List<Connection>();

    private void Start()
    {
        drawingControler = FindObjectOfType<DrawingControler>();
    }
    private void OnMouseOver()
    {
       drawingControler.MouseOverPoint(this);
    }

    public void ErasePoint()
    {
        if(currentPoint != null)
        {
            Destroy(currentPoint.gameObject);
            Points.points++;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Connection connection = collision.gameObject.GetComponent<Connection>();
        if (connection != null)
        {
            if (!connections.Contains(connection))
            {
                connections.Add(connection);
                connection.passingThroughPoints.Add(this);
                if (connections.Count > 1)
                {
                    if(currentPoint == null)
                    {
                        CreatePoint();
                    }

                }
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Point point = collision.gameObject.GetComponent<Point>();
        if(point == currentPoint)
        {
            currentPoint = null;
        }
    }

    public bool IsOneLine()
    {
        return connections.Count == 0;
    }

    void CreateCrosssection()
    {
        Debug.Log("Create Crosssection: " + connections.Count.ToString());
        List<Connection> tmpConnections = connections.GetRange(0, connections.Count);
        Debug.Log("ConnectionsCount: " + tmpConnections.Count.ToString());
        foreach(var c in tmpConnections)
        {
           BreakLine(c, currentPoint);
        }
    }

    void BreakLine(Connection c, Point mid)
    {
        if (c.Contains(currentPoint))
        {
            return;
        }
        Debug.Log("BreakLine");
        Point left, right;
        left = c.from;
        right = c.to;
        Destroy(c.gameObject);
        Drawer.drawer.CreateConnection(left, mid);
        Drawer.drawer.CreateConnection(mid, right);
    }
    public Point CreatePoint()
    {
        if (Points.BuyForPoint())
        {
            Point point = Drawer.drawer.CreatePoint();
            point.gameObject.transform.position = transform.position;
            currentPoint = point;
            if (connections.Count > 0)
            {
                CreateCrosssection();
            }
            return point;
        }
        else
        {
            return null;
        }

    }
}
