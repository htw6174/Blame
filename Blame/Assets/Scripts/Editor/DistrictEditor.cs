using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(DistrictGenerator))]
public class DistrictEditor : Editor {

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        DrawDefaultInspector();
        if (EditorGUI.EndChangeCheck())
        {
            (target as DistrictGenerator).UpdateDimensions();
            if (Application.isPlaying)
            {
                (target as DistrictGenerator).Generate();
            }
        }
    }
}
