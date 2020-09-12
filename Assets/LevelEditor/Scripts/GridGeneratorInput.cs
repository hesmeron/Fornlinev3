using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridGeneratorInput : MonoBehaviour
{
    public void SetX(Text x)
    {
        GridDrawer.gridDrawer.sizeX = int.Parse(x.text) * GridDrawer.gridDrawer.gridSize;
    }
    public void SetY(Text y)
    {
        GridDrawer.gridDrawer.sizeY = int.Parse(y.text) * GridDrawer.gridDrawer.gridSize;
    }
}
