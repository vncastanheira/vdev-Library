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
        int optIndex = 0;
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            options = ((LocalizationManager)target).Options.ToArray();
            optIndex = EditorGUILayout.Popup("Default", optIndex, options);
        }
    }
}
