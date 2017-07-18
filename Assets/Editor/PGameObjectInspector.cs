using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PWorldObject)), CanEditMultipleObjects]
public class TextAreaEditor : Editor
{

    public SerializedProperty description;
    void OnEnable()
    {
        description = serializedObject.FindProperty("description");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        description.stringValue = EditorGUILayout.TextArea(description.stringValue, GUILayout.MaxHeight(75));
        serializedObject.ApplyModifiedProperties();
    }
}