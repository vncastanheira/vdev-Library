using System;
using UnityEditor;
using UnityEngine;

namespace vnc.Experimental.Behaviours
{
	[Serializable]
	public class Node
	{
		public string title;
		public Rect rect;
		public bool isDragged;
		public bool isSelected;

        public Behaviour behaviour;

		public ConnectionPoint inPoint;
		public ConnectionPoint outPoint;

		[NonSerialized]
		public GUIStyle style;

		public Node(Vector2 position, float width, float height)
		{
			this.rect = new Rect(position.x, position.y, width, height);
			this.inPoint = new ConnectionPoint(this, ConnectionPointType.In);
			this.outPoint = new ConnectionPoint(this, ConnectionPointType.Out);
			this.style = NodeBasedEditor.nodeStyle;
		}

		public void Drag(Vector2 delta)
		{
            rect.position += delta;
		}

		public void Draw()
		{
			inPoint.Draw();
			outPoint.Draw();
			style = (!isSelected ? NodeBasedEditor.nodeStyle : NodeBasedEditor.selectedNodeStyle);
            GUI.Box(rect, title, style);
		}

		public bool ProcessEvents(Event e)
		{
            EventType type = e.type;
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0)
                    {
                        if (rect.Contains(e.mousePosition))
                        {
                            isDragged = true;
                            isSelected = true;
                        }
                        else
                        {
                            isSelected = false;
                            NodeBasedEditor.OnClearConnectionSelection();
                        }
                        GUI.changed = true;
                    }
                    if (e.button == 1 && isSelected && rect.Contains(e.mousePosition))
                    {
                        ProcessContextMenu();
                        e.Use();
                    }
                    break;
                case EventType.MouseUp:
                    isDragged = false;
                    break;
                case EventType.MouseDrag:
                    if (e.button == 0 && this.isDragged)
                    {
                        Drag(e.delta);
                        e.Use();
                    }
                    break;
            }
            return true;
		}

		private void ProcessContextMenu()
		{
			GenericMenu genericMenu = new GenericMenu();
			genericMenu.AddItem(new GUIContent("Connect"), false, new GenericMenu.MenuFunction(this.OnClickConnect));
			genericMenu.AddItem(new GUIContent("Remove node"), false, new GenericMenu.MenuFunction(this.OnClickRemoveNode));
			genericMenu.ShowAsContext();
		}

		private void OnClickConnect()
		{
			NodeBasedEditor.OnClickOutPoint(this.outPoint);
		}

		private void OnClickRemoveNode()
		{
			NodeBasedEditor.OnClickRemoveNode(this);
		}
	}
}
