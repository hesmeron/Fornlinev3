using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridDrawer))]
public class GridInspector : Editor
{
    private GridDrawer _myTarget;

    private void OnEnable()
    {
        _myTarget = (GridDrawer) target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        bool generateGrid = GUILayout.Button("Generate", GUILayout.Height(2 * EditorGUIUtility.singleLineHeight));
        bool clearGrid = GUILayout.Button("Clear", GUILayout.Height(2 * EditorGUIUtility.singleLineHeight));

        if(generateGrid)
        {
            _myTarget.Draw();
        }
        if (clearGrid)
        {
            _myTarget.Clear();
        }
    }
}
