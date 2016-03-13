using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomPropertyDrawer(typeof(StructureBlock))]
public class StructBlockDrawer : PropertyDrawer {

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Rect doublePosition = position;
        doublePosition.height *= 2f;
        EditorGUI.BeginProperty(position, label, property);

        EditorGUI.LabelField(position, label);
        position.yMin += position.height / 2f;
        //position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        int sliderWidth = (int)(position.width - 5) / 2;
        int prefabRectWidth = (int)(position.width - 5) / 2;

        Rect prefabRect = new Rect(position.x, position.y, prefabRectWidth, position.height);
        Rect spawnChanceRect = new Rect(position.x + prefabRectWidth + 5, position.y, sliderWidth, position.height);

        EditorGUI.PropertyField(prefabRect, property.FindPropertyRelative("prefab"), GUIContent.none);
        //EditorGUI.PropertyField(spawnChanceRect, property.FindPropertyRelative("spawnChance"), GUIContent.none);
        EditorGUI.Slider(spawnChanceRect, property.FindPropertyRelative("spawnChance"), 0f, 1f, GUIContent.none);

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) * 2f;
    }
}
