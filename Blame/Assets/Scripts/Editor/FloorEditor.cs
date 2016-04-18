using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(FloorGeneratorTester))]
public class FloorEditor : Editor {

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        DrawDefaultInspector();
        if (EditorGUI.EndChangeCheck())
        {
            (target as FloorGeneratorTester).UpdateDimensions();
            if (Application.isPlaying)
            {
                (target as FloorGeneratorTester).Generate();
            }
        }
    }
}
