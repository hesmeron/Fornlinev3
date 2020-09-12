using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingPoint : MonoBehaviour
{
    DrawingControler drawingControler;
    public Point currentPoint = null;
    public List<Line> lines = new List<Line>();

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
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < lines.Count; i++)
        {
            if(lines[i] == null)
            {
                lines.RemoveAt(i);
            }
        }
        Line line = collision.gameObject.GetComponent<Line>();
        if(line!= null)
        {
            if (!lines.Contains(line))
            {
                lines.Add(line);
            }
        }
    }
}
