using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(BlockGenerator))]
public class BlockEditor : Editor {

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        DrawDefaultInspector();
        if(EditorGUI.EndChangeCheck() && Application.isPlaying)
        {
            (target as BlockGenerator).Generate();
        }
    }
}
