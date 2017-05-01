using System.Collections;
using System.Collections.Generic;
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

        private void OnEnable()
        {
            registries = serializedObject.FindProperty("Registries");

            list = new UnityEditorInternal.ReorderableList(serializedObject, registries, true, true, true, true);
            list.drawHeaderCallback = (Rect rect) => {
                EditorGUI.LabelField(rect, "Language Registries");
            };
        }

        public override void OnInspectorGUI()
        {
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

                //keyProp.stringValue = EditorGUILayout.TextField("Key", keyProp.stringValue);
                //textProp.objectReferenceValue = EditorGUILayout.ObjectField("Text", textProp.objectReferenceValue, typeof(TextAsset), allowSceneObjects: true);
            };
            list.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
    }
}

