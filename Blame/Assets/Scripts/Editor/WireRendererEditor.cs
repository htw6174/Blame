using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(WireRenderer))]
public class WireRendererEditor : Editor {

    private WireRenderer _wireRenderer;

    private MaterialEditor _materialEditor;

    void OnEnable()
    {
        _wireRenderer = (WireRenderer)target;

        if (_wireRenderer.material != null)
        {
            _materialEditor = (MaterialEditor)CreateEditor(_wireRenderer.material);
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUI.BeginChangeCheck();

        DrawDefaultInspector();

        if (EditorGUI.EndChangeCheck() || (Event.current.type == EventType.ValidateCommand && Event.current.commandName == "UndoRedoPerformed"))
        {
            _wireRenderer.CreateWireMesh();

            if (_materialEditor != null)
            {
                DestroyImmediate(_materialEditor);
            }

            if (_wireRenderer.material != null)
            {
                _materialEditor = (MaterialEditor)CreateEditor(_wireRenderer.material);
            }
        }

        if (_materialEditor != null)
        {
            _materialEditor.DrawHeader();
            _materialEditor.OnInspectorGUI();
        }

        serializedObject.ApplyModifiedProperties();
    }

    void OnSceneGUI()
    {
        Transform wireTransform = _wireRenderer.transform;
        Quaternion handleRotation = Tools.pivotRotation == PivotRotation.Local ? wireTransform.rotation : Quaternion.identity;
        Vector3 startPoint = wireTransform.TransformPoint(_wireRenderer.startPoint);
        Vector3 endPoint = wireTransform.TransformPoint(_wireRenderer.endPoint);

        EditorGUI.BeginChangeCheck();
        startPoint = Handles.DoPositionHandle(startPoint, handleRotation);
        endPoint = Handles.DoPositionHandle(endPoint, handleRotation);
        if (EditorGUI.EndChangeCheck())
        {
            //Undo.RecordObject(_wireRenderer, "Move Endpoint");
            EditorUtility.SetDirty(_wireRenderer);
            _wireRenderer.startPoint = startPoint;
            _wireRenderer.endPoint = endPoint;
            _wireRenderer.CreateWireMesh();
        }
    }
}
