using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(CameraFog))]
public class FogEditor : Editor {

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        DrawDefaultInspector();
        if (EditorGUI.EndChangeCheck())
        {
            (target as CameraFog).SetFog();
        }
    }
}
