using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace vnc.Experimental.Behaviours
{
    public partial class NodeBasedEditor
    {
        private void DrawPanel()
        {
            GUILayout.BeginArea(new Rect(0, 0, PANEL_WIDTH, position.height), GUI.skin.box);
            if(selectedBlackboard != null)
            {
                selectedBlackboard.Title = EditorGUILayout.TextField("Name", selectedBlackboard.Title, EditorStyles.textField);
                selectedBlackboard.Update = (AnimatorUpdateMode) EditorGUILayout.EnumPopup("Update", selectedBlackboard.Update);
                DrawParameters();
            }
            GUILayout.EndArea();
        }

        private void DrawParameters()
        {
            if (selectedBlackboard != null)
            {
                parametersList.DoLayoutList();
            }
        }

        private void DrawNodes()
        {
            if (selectedBlackboard != null)
            {
                foreach (var node in selectedBlackboard.nodes)
                {
                    node.Draw();
                }
            }
        }

        private void DrawConnections()
        {
            if (singleton.selectedBlackboard.connections != null)
            {
                for (int i = 0; i < singleton.selectedBlackboard.connections.Count; i++)
                {
                    singleton.selectedBlackboard.connections[i].Draw();
                }
            }
        }

        private void DrawConnectionLine(Event e)
        {
            if (selectedInPoint != null && selectedOutPoint == null)
            {
                Handles.DrawBezier(
                    selectedInPoint.rect.center,
                    e.mousePosition,
                    selectedInPoint.rect.center + Vector2.left * 50f,
                    e.mousePosition - Vector2.left * 50f,
                    Color.white,
                    null,
                    2f
                );

                GUI.changed = true;
            }

            if (selectedOutPoint != null && selectedInPoint == null)
            {
                Handles.DrawBezier(
                    selectedOutPoint.rect.center,
                    e.mousePosition,
                    selectedOutPoint.rect.center - Vector2.left * 50f,
                    e.mousePosition + Vector2.left * 50f,
                    Color.white,
                    null,
                    2f
                );

                GUI.changed = true;
            }
        }

        private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
        {
            int widthDivs = Mathf.CeilToInt(position.width / gridSpacing);
            int heightDivs = Mathf.CeilToInt(position.height / gridSpacing);

            Handles.BeginGUI();
            Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

            offset += drag * 0.5f;
            Vector3 newOffset = new Vector3(offset.x % gridSpacing, offset.y % gridSpacing, 0);

            for (int i = 0; i < widthDivs; i++)
            {
                Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, position.height, 0f) + newOffset);
            }

            for (int j = 0; j < heightDivs; j++)
            {
                Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, new Vector3(position.width, gridSpacing * j, 0f) + newOffset);
            }

            Handles.color = Color.white;
            Handles.EndGUI();
        }
    }
}
