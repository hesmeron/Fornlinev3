using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Drawer : MonoBehaviour
{
    public static Drawer drawer;
    [SerializeField]
    GameObject pointPref, linePref;
    [SerializeField]
    Color lineColor;
    // Start is called before the first frame update
    void Start()
    {
        if (drawer != null)
        {
            Destroy(this);
        }
        else
        {
            drawer = this;
        }
    }

    public Line CreateConnection(Point from, Point to)
    {
        Debug.Log("CreateConnection");
        Line line = Instantiate(linePref, this.transform).AddComponent<Line>();
        line.gameObject.AddComponent<LineCollider>().line = line;
        Connection c = line.gameObject.AddComponent<Connection>();
        line.gameObject.AddComponent<Repulsor>().Initialaize(from, to);
        c.Initiate(from, to);
        line.IninitateLine(from.transform.position, to.transform.position, lineColor, 0.045f);
        from.AddNeighbour(line, to);
        to.AddNeighbour(line, from);
        return line;
    }
    public Point CreatePoint()
    {
        GameObject newPoint = Instantiate(pointPref);
        Point newPointScript = newPoint.GetComponent<Point>();
        return newPointScript;
    }

    public static bool AreOnLine(Transform a, Transform b, Transform c)
    {
        Vector2 directionA = b.position - a.position;
        Vector2 directionB = c.position - b.position;
        bool onLine = (directionA == directionB || directionA == -directionB);
        Debug.Log("Is online: " + onLine);
        return onLine;
    }
}
