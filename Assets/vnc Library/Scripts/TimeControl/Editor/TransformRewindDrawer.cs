using UnityEditor;
using UnityEngine;

namespace vnc.TimeSystem
{
    [CustomPropertyDrawer(typeof(TransformRewind))]
    public class TransformRewindDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var size = property.FindPropertyRelative("recordedPos").arraySize;
            EditorGUI.BeginProperty(position, label, property);

            var posRect = new Rect(position.x, position.y, EditorGUIUtility.currentViewWidth, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(posRect, "Transform Count: " + size);

            EditorGUI.EndProperty();
        }
    }
}
