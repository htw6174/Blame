using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(NodeGrid))]
//[CanEditMultipleObjects] //Can't support multi-object editing because reasons
public class BuildingNodeEditor : Editor {

    SerializedProperty gridWidth;
    SerializedProperty gridHeight;
    SerializedProperty nodes;

    NodeGrid grid;

    void OnEnable()
    {
        gridWidth = serializedObject.FindProperty("gridWidth");
        gridHeight = serializedObject.FindProperty("gridHeight");
        nodes = serializedObject.FindProperty("nodes");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        EditorGUILayout.LabelField(string.Format("Width: {0}", gridWidth.intValue), string.Format("Height: {0}", gridHeight.intValue));
        EditorGUILayout.HelpBox("Click on nodes to toggle on or off", MessageType.Info);

        //EditorGUI.BeginChangeCheck();
        //DrawDefaultInspector();
        //if (EditorGUI.EndChangeCheck())
        //{
        //    foreach (NodeGrid grid in serializedObject.targetObjects)
        //    {
        //        grid.InitializeGrid();
        //    }
        //}

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Set All True"))
        {
            //foreach (NodeGrid grid in serializedObject.targetObjects)
            //{
            //    grid.SetAllTrue();
            //}
            ((NodeGrid)serializedObject.targetObject).SetAllTrue();
        }
        if (GUILayout.Button("Set All False"))
        {
            //foreach (NodeGrid grid in serializedObject.targetObjects)
            //{
            //    grid.SetAllFalse();
            //}
            ((NodeGrid)serializedObject.targetObject).SetAllFalse();
        }
        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }

    private void OnSceneGUI()
    {
        grid = target as NodeGrid;

        if (grid.NodesDefined() == false)
        {
            grid.InitializeGrid();
        }

        for (int x = 0; x < grid.gridWidth; x++)
        {
            for (int y = 0; y < grid.gridHeight; y++)
            {
                Vector3 buttonPos = grid.GetNodePosition(x, y);
                Handles.color = grid.GetNode(x, y) ? Color.green : Color.red;
                if (Handles.Button(buttonPos, grid.transform.rotation, grid.nodeSpacing * 0.6f, grid.nodeSpacing * 0.6f, Handles.CubeHandleCap))
                {
                    grid.FlipNode(x, y);
                }
            }
        }
    }
}
