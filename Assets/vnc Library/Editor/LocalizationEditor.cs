using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using vnc.Tools.Localization;

namespace vnc.Editor
{
    [CustomEditor(typeof(LocalizationManager))]
    public class LocalizationEditor : UnityEditor.Editor
    {
        string[] options;
        SerializedProperty optIndex;

        private void OnEnable()
        {
            optIndex = serializedObject.FindProperty("OptionIndex");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            options = ((LocalizationManager)target).DisplayOptions.ToArray();
            if (options.Length > 0)
            {
                optIndex.intValue = EditorGUILayout.Popup("Default", optIndex.intValue, options);
                EditorGUILayout.HelpBox(@"The Default language will be the starting language if no other is selected.", MessageType.Info);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
