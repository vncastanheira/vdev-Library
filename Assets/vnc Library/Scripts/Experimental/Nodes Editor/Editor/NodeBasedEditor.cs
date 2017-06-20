using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace vnc.Experimental.Behaviours
{
    public partial class NodeBasedEditor : EditorWindow
    {
        private static NodeBasedEditor singleton;

        private Blackboard selectedBlackboard = null;
        ReorderableList parametersList;
        
        public static GUIStyle nodeStyle { get; private set; }
        public static GUIStyle selectedNodeStyle { get; private set; }
        public static GUIStyle inPointStyle { get; private set; }
        public static GUIStyle outPointStyle { get; private set; }
        public static Texture2D backgroundStyle { get; private set; }

        private ConnectionPoint selectedInPoint;
        private ConnectionPoint selectedOutPoint;

        private Vector2 drag;
        private Vector2 offset;

        private const float PANEL_WIDTH = 250f;

        private Rect nodeArea { get { return new Rect(PANEL_WIDTH, 0, position.width - PANEL_WIDTH, position.height); } }

        [MenuItem("Window/vnc Library/Node Based Editor")]
        public static void OpenWindow()
        {
            NodeBasedEditor window = GetWindow<NodeBasedEditor>();
            window.titleContent = new GUIContent("Node Editor");
        }

        private void OnEnable()
        {
            singleton = this;

            nodeStyle = new GUIStyle();
            nodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
            nodeStyle.border = new RectOffset(12, 12, 12, 12);

            selectedNodeStyle = new GUIStyle();
            selectedNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1 on.png") as Texture2D;
            selectedNodeStyle.border = new RectOffset(12, 12, 12, 12);

            inPointStyle = new GUIStyle();
            inPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
            inPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
            inPointStyle.border = new RectOffset(4, 4, 12, 12);

            outPointStyle = new GUIStyle();
            outPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D;
            outPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D;
            outPointStyle.border = new RectOffset(4, 4, 12, 12);

            backgroundStyle = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            backgroundStyle.SetPixel(0, 0, new Color(0.15f, 0.15f, 0.15f));
            backgroundStyle.Apply();

            Selection.selectionChanged = ProcessBlackboard;
            ProcessBlackboard();
        }

        private void OnGUI()
        {
            GUI.DrawTexture(new Rect(0, 0, position.width, position.height), backgroundStyle, ScaleMode.StretchToFill);
            DrawGrid(20, 0.2f, Color.grey);
            DrawGrid(100, 0.4f, Color.black);

            DrawNodes();
            DrawConnections();
            DrawConnectionLine(Event.current);
            DrawPanel();

            ProcessNodeEvents(Event.current);
            ProcessEvents(Event.current);

            if (GUI.changed)
                Repaint();
        }

        private void ProcessBlackboard()
        {
            Blackboard blackboard = Selection.activeObject as Blackboard;
            if (blackboard != null)
            {
                selectedBlackboard = blackboard;
                parametersList = new ReorderableList(selectedBlackboard.Parameters, typeof(Parameter), true, true, true, true);
                parametersList.drawHeaderCallback = (Rect rect) =>
                {
                    EditorGUI.LabelField(rect, "Parameters");
                };
                parametersList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    var param = (Parameter)parametersList.list[index];
                    rect.y += 2;

                    param.Name = EditorGUI.TextField(new Rect(rect.x, rect.y, rect.width / 2, EditorGUIUtility.singleLineHeight), param.Name);
                    param.Type = (ParameterType)EditorGUI.EnumPopup(new Rect(rect.x + (rect.width / 2), rect.y, rect.width / 2, EditorGUIUtility.singleLineHeight), param.Type);
                };
                parametersList.displayRemove = true;
                Repaint();
            }
        }

        private void ProcessContextMenu(Vector2 mousePosition)
        {
            GenericMenu genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Add node"), false, () => OnClickAddNode(mousePosition));
            genericMenu.ShowAsContext();
        }

        private void OnDrag(Vector2 delta)
        {
            if (selectedBlackboard != null)
            {
                drag = delta;

                if (selectedBlackboard.nodes != null)
                {
                    for (int i = 0; i < selectedBlackboard.nodes.Count; i++)
                    {
                        selectedBlackboard.nodes[i].Drag(delta);
                    }
                }

                GUI.changed = true;
            }
        }

        #region Static Actions

        public static void OnClickAddNode(Vector2 mousePosition)
        {
            if (singleton.selectedBlackboard != null)
            {
                singleton.selectedBlackboard.nodes.Add(new Node(mousePosition, 200, 50));
            }
        }

        public static void OnClickInPoint(ConnectionPoint inPoint)
        {
            if (singleton.selectedOutPoint != null)
            {
                singleton.selectedInPoint = inPoint;
                if (singleton.selectedOutPoint.node != singleton.selectedInPoint.node)
                {
                    OnCreateConnection();
                    OnClearConnectionSelection();
                }
                else
                {
                    OnClearConnectionSelection();
                }
            }
        }

        public static void OnClickOutPoint(ConnectionPoint outPoint)
        {
            singleton.selectedOutPoint = outPoint;

            if (singleton.selectedInPoint != null)
            {
                if (singleton.selectedOutPoint.node != singleton.selectedInPoint.node)
                {
                    OnCreateConnection();
                    OnClearConnectionSelection();
                }
                else
                {
                    OnClearConnectionSelection();
                }
            }
        }

        public static void OnClickRemoveConnection(Connection connection)
        {
            singleton.selectedBlackboard.connections.Remove(connection);
        }

        public static void OnClickRemoveNode(Node node)
        {
            if (singleton.selectedBlackboard != null)
            {
                if (singleton.selectedBlackboard.connections != null)
                {
                    List<Connection> connectionsToRemove = new List<Connection>();

                    for (int i = 0; i < singleton.selectedBlackboard.connections.Count; i++)
                    {
                        if (singleton.selectedBlackboard.connections[i].inPoint == node.inPoint || singleton.selectedBlackboard.connections[i].outPoint == node.outPoint)
                        {
                            connectionsToRemove.Add(singleton.selectedBlackboard.connections[i]);
                        }
                    }

                    for (int i = 0; i < connectionsToRemove.Count; i++)
                    {
                        singleton.selectedBlackboard.connections.Remove(connectionsToRemove[i]);
                    }

                    connectionsToRemove = null;
                }

                singleton.selectedBlackboard.nodes.Remove(node);
            }
        }

        public static void OnCreateConnection()
        {
            singleton.selectedBlackboard.connections.Add(new Connection(singleton.selectedInPoint, singleton.selectedOutPoint));
        }

        public static void OnClearConnectionSelection()
        {
            singleton.selectedInPoint = null;
            singleton.selectedOutPoint = null;
        }
        #endregion

        [UnityEditor.Callbacks.OnOpenAsset(1)]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            if (Selection.activeObject as Blackboard != null)
            {
                NodeBasedEditor.OpenWindow();
                return true;
            }
            return false;
        }
    }
}
