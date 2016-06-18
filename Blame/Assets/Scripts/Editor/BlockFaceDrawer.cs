using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomPropertyDrawer(typeof(BlockFace))]
public class BlockFaceDrawer : PropertyDrawer {

    int screenCollapseWidth = 333;
    float fieldHeight = 16f;
    float fieldSpacing = 18f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty grid = property.FindPropertyRelative("grid");
        SerializedProperty relativePosition = property.FindPropertyRelative("relativePosition");
        SerializedProperty facingDirection = property.FindPropertyRelative("facingDirection");

        label = EditorGUI.BeginProperty(position, label, property);
        Rect currentPosition;
        float heightOffset = Screen.width < screenCollapseWidth ? fieldSpacing * 2f: fieldSpacing;
        int indent = EditorGUI.indentLevel;
        EditorGUI.PrefixLabel(position, label);
        currentPosition = position;
        EditorGUI.indentLevel = indent + 1;
        currentPosition.height = fieldHeight;
        currentPosition.y += fieldSpacing;
        EditorGUI.PropertyField(currentPosition, grid);
        currentPosition.y += fieldSpacing;
        EditorGUI.PropertyField(currentPosition, relativePosition);
        currentPosition.y += heightOffset;
        EditorGUI.PropertyField(currentPosition, facingDirection);

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return fieldHeight + (fieldSpacing * (Screen.width < screenCollapseWidth ? 5f : 3f));
    }
}
