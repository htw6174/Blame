using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(BuildingGenTester))]
public class BuildingEditor : Editor {

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        DrawDefaultInspector();
        if (EditorGUI.EndChangeCheck())
        {
            (target as BuildingGenTester).UpdateDimensions();
            if (Application.isPlaying)
            {
                (target as BuildingGenTester).Generate();
            }
        }
    }
}
