using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(BuildingBlock))]
public class BuildingBlockEditor : Editor {

    SerializedProperty dimensions;

    void OnEnable()
    {
        dimensions = serializedObject.FindProperty("dimensions");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.HelpBox("Warning! Changing BuildingBlock properties will reset all NodeGrids!", MessageType.Warning);

        EditorGUI.BeginChangeCheck();
        DrawDefaultInspector();
        //EditorGUILayout.PropertyField(dimensions);
        if (EditorGUI.EndChangeCheck())
        {
            ((BuildingBlock)serializedObject.targetObject).UpdateGrid();
        }

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Set All True"))
        {
            ((BuildingBlock)serializedObject.targetObject).SetAllGridsTrue();
        }
        if (GUILayout.Button("Set All False"))
        {
            ((BuildingBlock)serializedObject.targetObject).SetAllGridsFalse();
        }
        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }
}
