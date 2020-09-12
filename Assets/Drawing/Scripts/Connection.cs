using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection : MonoBehaviour
{
    public Point from, to;
    public List<DrawingPoint> passingThroughPoints = new List<DrawingPoint>();

    // Start is called before the first frame update
    public void Initiate(Point from, Point to)
    {

        this.from = from;
        from.AddConnection(this);
        to.AddConnection(this);
        this.to = to;
    }

    public bool Contains(Point point)
    {
        return from == point || to == point;
    }

    private void OnDestroy()
    {
        if (from)
        {
            from.neighbours.Remove(to);
        }
        if (to)
        {
            to.neighbours.Remove(from);
        }
        foreach(var p in passingThroughPoints)
        {
            p.connections.Remove(this);
        }
        from.RemoveConnection(this);
        to.RemoveConnection(this);
    }

}
