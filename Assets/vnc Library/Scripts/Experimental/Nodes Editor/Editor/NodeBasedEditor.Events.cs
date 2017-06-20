using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace vnc.Experimental.Behaviours
{
    public partial class NodeBasedEditor {

        private void ProcessEvents(Event e)
        {
            if (nodeArea.Contains(e.mousePosition))
            {
                drag = Vector2.zero;

                switch (e.type)
                {
                    case EventType.MouseDown:
                        if (e.button == 1)
                        {
                            ProcessContextMenu(e.mousePosition);
                        }
                        break;
                    case EventType.MouseDrag:
                        if (e.button == 0)
                        {
                            OnDrag(e.delta);
                        }
                        break;
                }
            }
        }

        private void ProcessNodeEvents(Event e)
        {
            if (selectedBlackboard != null)
            {
                if (selectedBlackboard.nodes != null && nodeArea.Contains(e.mousePosition))
                {
                    for (int i = selectedBlackboard.nodes.Count - 1; i >= 0; i--)
                    {
                        bool guiChanged = selectedBlackboard.nodes[i].ProcessEvents(e);
                        if (guiChanged)
                        {
                            GUI.changed = true;
                        }
                    }
                }

            }
        }
    }
}
