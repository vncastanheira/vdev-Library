using UnityEditor;
using UnityEngine;
using vnc.Tools.Localization;

namespace vnc.Editor
{
    [CustomEditor(typeof(Language))]
    public class LanguageEditor : UnityEditor.Editor
    {
        UnityEditorInternal.ReorderableList list;
        SerializedProperty registries;
        SerializedProperty langName;

        private void OnEnable()
        {
            registries = serializedObject.FindProperty("Registries");
            langName = serializedObject.FindProperty("Name");

            list = new UnityEditorInternal.ReorderableList(serializedObject, registries, true, true, true, true);
            // draw the header
            list.drawHeaderCallback = (Rect rect) => {
                EditorGUI.LabelField(rect, "Language Registries");
            };
        }

        public override void OnInspectorGUI()
        {
            langName.stringValue = EditorGUILayout.TextField("Language Name", langName.stringValue);
            EditorGUILayout.Space();

            // callback draw each element
            list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = list.serializedProperty.GetArrayElementAtIndex(index);
                var keyProp = element.FindPropertyRelative("Key");
                var textProp = element.FindPropertyRelative("Text");

                rect.y += 2;
                EditorGUI.PropertyField(
                    new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight),
                    keyProp, GUIContent.none);
                EditorGUI.PropertyField(
                    new Rect(rect.x + 65, rect.y, rect.width - 60 - 30, EditorGUIUtility.singleLineHeight),
                    textProp, GUIContent.none);
            };
            // draw the list
            list.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
    }
}

