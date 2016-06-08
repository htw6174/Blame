using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(NodeGrid))]
[CanEditMultipleObjects]
public class BuildingNodeEditor : Editor {

    SerializedProperty gridWidth;
    SerializedProperty gridHeight;
    SerializedProperty nodes;

    void OnEnable()
    {
        gridWidth = serializedObject.FindProperty("gridWidth");
        gridHeight = serializedObject.FindProperty("gridHeight");
        nodes = serializedObject.FindProperty("nodes");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUI.BeginChangeCheck();
        DrawDefaultInspector();
        if (EditorGUI.EndChangeCheck())
        {
            foreach (NodeGrid grid in serializedObject.targetObjects)
            {
                grid.InitializeGrid();
            }
        }
        
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Set All True"))
        {
            foreach (NodeGrid grid in serializedObject.targetObjects)
            {
                grid.SetAllTrue();
            }
        }
        if (GUILayout.Button("Set All False"))
        {
            foreach (NodeGrid grid in serializedObject.targetObjects)
            {
                grid.SetAllFalse();
            }
        }
        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }

    private void OnSceneGUI()
    {
        NodeGrid grid = target as NodeGrid;

        if (grid.nodes == null)
        {
            grid.InitializeGrid();
        }

        for (int x = 0; x < grid.gridWidth; x++)
        {
            for (int y = 0; y < grid.gridHeight; y++)
            {
                Vector3 buttonPos = grid.transform.TransformPoint((x * grid.gridSpacing) - grid.LeftToRightHalfDist, (y * grid.gridSpacing) - grid.TopToBottomHalfDist, 0f);
                Handles.color = grid.getNode(x, y) ? Color.green : Color.red;
                if (Handles.Button(buttonPos, grid.transform.rotation, grid.gridSpacing * 0.6f, grid.gridSpacing * 0.6f, Handles.CubeCap))
                {
                    grid.flipNode(x, y);
                }
            }
        }
    }
}
