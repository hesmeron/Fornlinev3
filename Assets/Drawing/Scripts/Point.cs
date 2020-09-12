using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grouping;

public class Point : MonoBehaviour, Groupable
{
    List<Connection> connections = new List<Connection>();
    public List<Point> neighbours = new List<Point>();
    [SerializeField]
    public Group group = null;


    void Update()
    {
        if(group != null)
        {
            Debug.Log("Group says" + group.isStable);
        }
    }
    public void AddNeighbour(Line line,Point neighbour)
    {
        neighbours.Add(neighbour);
    }

    public void OnDestroy() 
    { 
        foreach(var n in neighbours)
        {
            n.neighbours.Remove(this);
        }
        foreach(var c in connections)
        {
            Destroy(c.gameObject);
        }
    }

    public bool AreOnLine(Point a)
    {
        if(neighbours.Count > 1  || neighbours.Count == 0)
        {
            return false;
        }
        Point b = neighbours[0];
        Vector2 directionA = GetDirection(a);
        Vector2 directionB = GetDirection(b);
        bool onLine = (directionA == directionB || directionA == -directionB);
        Debug.Log("Is online: " + onLine);
        return onLine;

    }

    Vector2 GetDirection(Point a)
    {
        Vector2 direction = (Vector2)(a.transform.position - this.transform.position);
        return direction.normalized;
    }

    public void AddConnection(Connection c)
    {
        connections.Add(c);
        if(connections.Count == 1)
        {
            Destroy(this.gameObject.GetComponent<RepulsedBody>());
        }
        if(connections.Count == 2)
        {
            this.gameObject.AddComponent<PointShooter>();
        }
    }
    public void RemoveConnection(Connection c)
    {
        connections.Remove(c);
        if(connections.Count == 0 && this != null)
        {
            this.gameObject.AddComponent<RepulsedBody>();
        }
        if(connections.Count == 1 && this != null)
        {
            Destroy(this.gameObject.GetComponent<PointShooter>());
        }
    }
}
