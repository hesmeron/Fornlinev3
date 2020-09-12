using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[ExecuteAlways]
public class Line : MonoBehaviour
{
    public Color color = new Color(0,0,0);
    public Vector2 from, to;
    public float width = 0.04f;
    void Start()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        var t = Time.time;
        lineRenderer.widthMultiplier = width;
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
    }
    void Update()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        SetPointsPositions(lineRenderer);
    }


    void SetPointsPositions(LineRenderer lineRenderer)
    {
        Vector2 start = from;
        Vector2 end = to;
        float lengthOfLineRenderer = Vector2.Distance(start, end);
        lineRenderer.positionCount = 2;
        Vector2 heading = (end - start);
        lineRenderer.SetPosition(0, from);
        lineRenderer.SetPosition(1, to);

        Vector2 delta = (heading / 2);
        Vector2[] points = { start, end };
    }

    public Line IninitateLine(Vector2 from, Vector2 to, Color color, float width)
    {
        this.from = from;
        this.to = to;
        this.color = color;
        this.width = width;
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        SetPointsPositions(lineRenderer);
        return this;
    }

    public void SetNewPosition(Vector2 from, Vector2 to)
    {
        this.from = from;
        this.to = to;
    }
}
