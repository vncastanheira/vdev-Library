using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using vnc.Utilities;

namespace vnc.Editor
{
    public class UtilitiesWindow : EditorWindow
    {
        #region Custom Quad
        Rect quadRect;
        Material quadMaterial;
        #endregion

        #region Transform
        int xAxis = 0;
        int yAxis = 0;
        int zAxis = 0;
        Vector3 size;
        bool showError = false;

        #endregion

        [MenuItem("Window/vnc Library/Utilities")]
        public static void Open()
        {
            var window = GetWindow<UtilitiesWindow>();
            window.titleContent = new GUIContent("vnc Utilities", VNCStyles.Icon);
            window.Show();
        }

        private void OnEnable()
        {
            var icon = AssetDatabase.LoadAssetAtPath<Texture>("Assets/vnc Library/Icons/favicon.png");
            titleContent = new GUIContent("Utilities", icon);
            size = Vector3.zero;
        }

        private void OnGUI()
        {
            #region Custom Quad
            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical(EditorStyles.inspectorDefaultMargins);
            EditorGUILayout.LabelField("Custom Quad", VNCStyles.CenteredLabelBold);
            quadRect = EditorGUILayout.RectField("Rect", quadRect);
            quadMaterial = (Material)EditorGUILayout.ObjectField("Custom Material", quadMaterial, typeof(Material), false);
            if (GUILayout.Button("Create", EditorStyles.miniButtonRight))
            {
                CustomQuad.Create(quadRect, quadMaterial);
            }

            EditorGUILayout.EndVertical();
            #endregion

            #region Transform Utility
            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical(EditorStyles.inspectorDefaultMargins);
            EditorGUILayout.LabelField("Local Scale", VNCStyles.CenteredLabelBold);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Direction", EditorStyles.miniLabel);
            xAxis = EditorGUILayout.IntSlider("X Axis", xAxis, -1, 1);
            yAxis = EditorGUILayout.IntSlider("Y Axis", yAxis, -1, 1);
            zAxis = EditorGUILayout.IntSlider("Z Axis", zAxis, -1, 1);
            EditorGUILayout.Space();
            size = EditorGUILayout.Vector3Field("Size", size);

            if (GUILayout.Button("Scale"))
            {
                var active = Selection.activeGameObject;
                if (active == null)
                {
                    showError = true;
                }
                else
                {
                    showError = false;
                    active.transform.LocalScale(size, new Vector3(xAxis, yAxis, zAxis));
                }
            }
            if(showError)
                EditorGUILayout.HelpBox("No GameObject selected in Scene", MessageType.Error);

            EditorGUILayout.EndVertical();
            #endregion

        }
    }
}
