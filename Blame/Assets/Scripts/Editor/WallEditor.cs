using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(WallGenTester))]
public class WallEditor : Editor {

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        DrawDefaultInspector();
        if (EditorGUI.EndChangeCheck())
        {
            (target as WallGenTester).Generate();
        }
    }
}
