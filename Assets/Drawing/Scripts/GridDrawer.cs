using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridDrawer : MonoBehaviour
{
    public static GridDrawer gridDrawer = null;
    public float sizeY, sizeX;
    [SerializeField]
    GameObject linePref;
    public float gridSize = 1;
    [SerializeField]
    Color color;
    [SerializeField]
    GameObject drawingPointPref;
    // Start is called before the first frame update


    public void Start()
    {
        if(gridDrawer == null)
        {
            gridDrawer = this;
        }
        else
        {
            Debug.LogError("Multiple grid drawers n scene, that is not allowed");
        }
    }
    public void Draw()
    {
        Clear();
        DrawGrid();
        CreateDrawingPoint();
    }

    void DrawLine(Vector2 from, Vector2 to)
    {
        Line line =Instantiate(linePref, this.transform).AddComponent<Line>();
        line.IninitateLine(from, to, color, 0.04f);
    }

    void DrawGrid()
    {
        for (float i = -sizeY / 2; i <= sizeY / 2; i += gridSize)
        {
            DrawLine(new Vector2(-sizeX / 2, i), new Vector2(sizeX / 2, i));
        }
        for (float i = -sizeX / 2; i <= sizeX / 2; i += gridSize)
        {
            DrawLine(new Vector2(i, -sizeY / 2), new Vector2(i, sizeY / 2));
        }
    }

    void CreateDrawingPoint()
    {
        for (float i = -sizeY / 2 + gridSize; i <= sizeY / 2; i += gridSize)
        {
            for (float j = -sizeX / 2 + gridSize; j <= sizeX / 2; j += gridSize)
            {
                Instantiate(drawingPointPref, this.transform).transform.position = new Vector3(j, i, 0);
            }
        }
    }

    public void Clear()
    {
        while(this.transform.childCount > 0) {
            DestroyImmediate(this.transform.GetChild(0).gameObject);
        }
    }
}
