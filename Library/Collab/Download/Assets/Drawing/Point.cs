using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public List<Line> lines = new List<Line>();
    public List<Point> neighbours = new List<Point>();

    public void AddNeighbour(Line line,Point neighbour)
    {
        lines.Add(line);
        neighbours.Add(neighbour);
    }

    public void OnDestroy() 
    { 
        foreach(var l in lines)
        {
            neighbours[lines.IndexOf(l)].lines.Remove(l);
            if(l!= null)
            {
                Destroy(l.gameObject);
            }
        }

        foreach(var n in neighbours)
        {
            n.neighbours.Remove(this);
        }
    }
}
