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
        SerializedProperty selectedLangProp;
        SerializedProperty languagesProp;
        string[] options;
        int optIndex = 0;

        private void OnEnable()
        {
            selectedLangProp = serializedObject.FindProperty("selectedLanguage");
            languagesProp = serializedObject.FindProperty("languages");
        }
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            options = ((LocalizationManager)target).Options.ToArray();
            optIndex = EditorGUILayout.Popup("Default", optIndex, options);
        }
    }
}
